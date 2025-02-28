using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool hasKey = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHasKey(bool value)
    {
        hasKey = value;
    }

    public bool GetHasKey()
    {
        return hasKey;
    }
}
