using UnityEngine;
using System.Collections;

public class LevelSpawner : MonoBehaviour
{

    public string levelName;
    public AudioSource loadLevelAudio;
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

