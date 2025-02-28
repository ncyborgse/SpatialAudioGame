using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour
{
    public AudioSource audioSource;
    private TextToSpeech textToSpeech;
    private string sceneName;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

            // Ensure played does not keep key
            if (playerController != null) {
                playerController.SetHasKey(false);
                PlayerPrefs.SetInt($"{sceneName}KeyObtained", 0);
                PlayerPrefs.Save();
            }
            if (audioSource != null)
            {
                audioSource.Play();
                RequestTextToSpeech("Collided. Returning to lobby.");

                // Load the scene after the sound finishes
                StartCoroutine(LoadSceneAfterSound("SelectionScene"));
            }
            else
            {
                // Return to start scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("SelectionScene");
            }
        }
    }

    private IEnumerator LoadSceneAfterSound(string sceneName)
    {
        // Wait until the audio clip has finished playing
        yield return new WaitForSeconds(3);

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
