using UnityEngine;
using UnityEngine.InputSystem;


public class CameraMovement : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float lookSpeed = 2f;
    private float rotationX = 0;
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.EnableDevice(Mouse.current);
        Debug.Log("Input System Enabled!");
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center
        Cursor.visible = false; // Hide cursor


    }

    void Update()
    {
        // Check if the Keyboard is available
        if (Keyboard.current != null)
        {
            // Move with WASD
            float moveX = 0;
            float moveZ = 0;

            if (Keyboard.current.wKey.isPressed) moveZ += 1;
            if (Keyboard.current.sKey.isPressed) moveZ -= 1;
            if (Keyboard.current.aKey.isPressed) moveX -= 1;
            if (Keyboard.current.dKey.isPressed) moveX += 1;

            Vector3 move = new Vector3(moveX, 0, moveZ).normalized * moveSpeed * Time.deltaTime;
            //Debug.Log($"Move Vector: {move}"); // Print movement vector to check
            transform.Translate(move, Space.Self);

            // Look around with Mouse
            if (Mouse.current != null)
            {
                Vector2 lookDelta = Mouse.current.delta.ReadValue();

                rotationX -= lookDelta.y * lookSpeed * Time.deltaTime;
                float rotationY = lookDelta.x * lookSpeed * Time.deltaTime;
                rotationX = Mathf.Clamp(rotationX, -90, 90); // Prevent looking too far up/down

                transform.localRotation = Quaternion.Euler(rotationX, transform.localRotation.eulerAngles.y + rotationY, 0);
            }

        }
    }
}
