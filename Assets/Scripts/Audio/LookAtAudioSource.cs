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
        audioPoint = new GameObject("AudioPoint");
        audioPoint.transform.parent = transform;

        // Attach the audio source to the audio point
        audioSource.transform.parent = audioPoint.transform;
        audioSource.transform.localPosition = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerCamera == null) return;

        playerCameraTransform = playerCamera.transform;
        Ray ray = new Ray(playerCameraTransform.position, playerCameraTransform.forward);
        RaycastHit hit;

        // Turn off sound to make sure it doesn't play when not looking at object
        audioSource.volume = 0f;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // If the object hit by the ray is the same as the object this script is attached to
            if (hit.collider.gameObject == gameObject)
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green, 2f);
                Debug.Log($"Ray hit {hit.collider.gameObject.name} at {hit.point}");

                // Convert the hit point to local space

                Vector3 localHitPoint = transform.InverseTransformPoint(hit.point);

                // Set the audio point to the hit point

                audioPoint.transform.localPosition = localHitPoint;

                // Turn on sound, louder if closer

                float distance = hit.distance;
                float volume = 0;//Mathf.Clamp01(1 / (distance));

                if (distance < maxDistance)
                {
                    float e = (float) System.Math.E;
                    volume = Mathf.Pow(e, maxDistance/ distance) - 1; 
                }
                audioSource.volume = volume;
            }
        }
    }
}
