using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            // Ensure played does not keep key
            if (playerController != null) {
                playerController.SetHasKey(false);
            }
            if (audioSource != null)
            {
                audioSource.Play();
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
        yield return new WaitForSeconds(1);

        // Load the scene after the sound finishes
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }


}
