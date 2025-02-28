using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        // If there is no instance of this object
        if (instance == null)
        {
            // Set the instance to this object
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // If there is an instance of this object, destroy this object
            Destroy(this.gameObject);
        }
    }
}
