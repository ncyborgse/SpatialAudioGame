using UnityEngine;

public class KeyPickup : MonoBehaviour
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
            playerController.SetHasKey(true);

            // Play collection sound
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Destroy the key after the sound has played

            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
