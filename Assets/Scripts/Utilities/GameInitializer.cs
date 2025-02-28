using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private static bool hasInitialized = false;

    private void Awake()
    {
        if (!hasInitialized)
        {
            Debug.Log("Resetting PlayerPrefs for a new session.");
            PlayerPrefs.DeleteAll(); // Clears all saved values on game start
            PlayerPrefs.Save();

            hasInitialized = true; // Ensures this only runs once per play session
        }
    }
}
