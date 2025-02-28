using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class TextToSpeech : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public enum InstructionType
    {
        PlayOnce,
        Repeated
    }

    [SerializeField] private InstructionType instructionType;
    [SerializeField][TextArea] private string text;

    private string sceneName;
    private string accessKey = "";
    private string secretKey = "";

    private string audioDirectory;

    private void Start()
    {
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        Debug.Log($"{sceneName}PlayOnce {PlayerPrefs.GetInt($"{sceneName}PlayOnce", 0)}");

        if (sceneName == "SelectionScene")
        {
            if (PlayerPrefs.GetInt($"{sceneName}PlayOnce") == 0)
            {
                PlayerPrefs.SetInt($"{sceneName}PlayOnce", 1);
                PlayerPrefs.Save();
                SendRequest(text);
            }
            else
            {
                SendRequest("Select a map by stepping on the coloured circle!");
            }
        }
        else
        {
            SendRequest(text);
        }
    }

    private string GetAudioFileName(string text)
    {
        return $"audio_{text.GetHashCode()}.mp3";
    }

    public void SendRequest(string text)
    {
        audioDirectory = Path.Combine(Application.dataPath, "Sounds");

        if (!Directory.Exists(audioDirectory))
        {
            Directory.CreateDirectory(audioDirectory);
        }

        string audioFileName = GetAudioFileName(text);
        string audioFilePath = Path.Combine(audioDirectory, audioFileName);

        if (File.Exists(audioFilePath))
        {
            PlayAudio(audioFilePath);
            Debug.Log("audio playing from file path");
        }
        else
        {
            SendRequest(text, audioFilePath);
        }
    }

    public async void SendRequest(string text, string filePath)
    {
        try
        {
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var client = new AmazonPollyClient(credentials, RegionEndpoint.EUCentral1);

            var request = new SynthesizeSpeechRequest
            {
                Text = text,
                Engine = Engine.Neural,
                VoiceId = VoiceId.Danielle,
                OutputFormat = OutputFormat.Mp3
            };

            var response = await client.SynthesizeSpeechAsync(request);

            WriteAudioFile(response.AudioStream, filePath);
            PlayAudio(filePath);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception encountered: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private void WriteAudioFile(Stream stream, string filePath)
    {
        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                byte[] buffer = new byte[8 * 1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error writing audio file: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private async void PlayAudio(string filePath)
    {
        using (var www = UnityWebRequestMultimedia.GetAudioClip($"file://{filePath}", AudioType.MPEG))
        {
            var op = www.SendWebRequest();

            while (!op.isDone)
            {
                await Task.Yield();
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error loading audio: {www.error}");
                return;
            }

            var clip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
