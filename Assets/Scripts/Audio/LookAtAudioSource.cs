using UnityEngine;

public class LookAtAudioSource : MonoBehaviour

{

    public float maxDistance = 5f;
    public AudioSource audioSource;

    private Camera playerCamera;
    private Transform playerCameraTransform;
    private GameObject audioPoint; // The object that the audio source is attached to
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCamera = Camera.main;
        // Log the tag to ensure this is the correct camera

        audioPoint = new GameObject("AudioPoint");
        audioPoint.transform.parent = transform;

        // Attach the audio source to the audio point
        audioSource.transform.parent = audioPoint.transform;
        audioSource.transform.localPosition = Vector3.zero;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerCamera == null) return;

        playerCameraTransform = playerCamera.transform;
        Ray ray = new Ray(playerCameraTransform.position, playerCameraTransform.forward);
        //Debug.DrawLine(playerCameraTransform.forward)
        RaycastHit hit;

        // Turn off sound to make sure it doesn't play when not looking at object
        audioSource.volume = 0f;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // If the object hit by the ray is the same as the object this script is attached to
            if (hit.collider.gameObject == gameObject)
            {
                // Move the audio point to the hit point
                audioPoint.transform.position = hit.point;

                // Turn on sound, louder if closer

                float distance = hit.distance;
                float volume = 0;//Mathf.Clamp01(1 / (distance));

                if (distance < maxDistance)
                {
                    float k = -Mathf.Log(0.01f)/ maxDistance;
                    volume = Mathf.Exp(-k * distance);
                }
                audioSource.volume = volume;
            }
        }
    }
}
