using UnityEngine;

public class KeyPickup : MonoBehaviour
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
            playerController.SetHasKey(true);
            PlayerPrefs.SetInt($"{sceneName}KeyObtained", 1);
            PlayerPrefs.Save();

            // Play collection sound
            if (audioSource != null)
            {
                audioSource.Play();
                RequestTextToSpeech("Key collected!Find the door.");
            }

            // Destroy the key after the sound has played

            Destroy(gameObject, audioSource.clip.length);
        }
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
