using UnityEngine;
using System.Collections;

public class LevelSpawner : MonoBehaviour
{

    public string levelName;
    public AudioSource loadLevelAudio;

    private static bool inputBlocked = false;
    private static float blockDuration = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(UnblockAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inputBlocked)
        {
            return;
        }
        if (other.CompareTag("PlayerCollider"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();


            if (loadLevelAudio != null)
            {
                loadLevelAudio.Play();
                // Load the scene after the sound finishes
                StartCoroutine(LoadSceneAfterSound(levelName));
            }
            else
            {
                // Return to start scene
                UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
            }

            inputBlocked = true;
        }
    }

    private IEnumerator LoadSceneAfterSound(string sceneName)
    {
        // Wait until the audio clip has finished playing
        yield return new WaitForSeconds(1);

        // Load the scene after the sound finishes
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private IEnumerator UnblockAfterDelay()
    {
        yield return new WaitForSeconds(blockDuration);
        inputBlocked = false;
    }
}

