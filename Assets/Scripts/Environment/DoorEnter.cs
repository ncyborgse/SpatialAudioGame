using UnityEngine;
using System.Collections;

public class DoorEnter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource audioSource;
    public AudioSource closedAudio;
    private TextToSpeech textToSpeech;
    private string sceneName;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (playerController.GetHasKey())
            {

                // Remove the key from the player

                playerController.SetHasKey(false);
                PlayerPrefs.SetInt($"{sceneName}KeyObtained", 0);
                PlayerPrefs.Save();
                if (audioSource != null)
                {
                    audioSource.Play();
                    RequestTextToSpeech("Map Cleared!");
                    // Load the scene after the sound finishes
                    StartCoroutine(LoadSceneAfterSound("SelectionScene"));
                }
                else { 
                    // Return to start scene
                    UnityEngine.SceneManagement.SceneManager.LoadScene("SelectionScene");
                }
            } else
            {                 // Play a sound to indicate the player does not have the key
                if (closedAudio != null)
                {
                    closedAudio.Play();
                    RequestTextToSpeech("The door cannot be opened. Find the key.");
                }
            }
        }
    }

    private IEnumerator LoadSceneAfterSound(string sceneName)
    {
        // Wait until the audio clip has finished playing
        yield return new WaitForSeconds(audioSource.clip.length);

        // Load the scene after the sound finishes
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    [System.Obsolete]
    private void RequestTextToSpeech(string text)
    {
        textToSpeech = FindObjectOfType<TextToSpeech>();

        // Make sure it exists before calling SendRequest()
        if (textToSpeech != null)
        {
            textToSpeech.SendRequest(text);
        }
        else
        {
            Debug.LogError("TextToSpeech component not found in the scene.");
        }
    }
}
