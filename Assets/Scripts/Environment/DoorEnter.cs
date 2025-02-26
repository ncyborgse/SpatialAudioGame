using UnityEngine;
using System.Collections;

public class DoorEnter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource audioSource;
    public AudioSource closedAudio;
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
            if (playerController.GetHasKey())
            {

                // Remove the key from the player

                playerController.SetHasKey(false);
                if (audioSource != null)
                {
                    audioSource.Play();
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
}
