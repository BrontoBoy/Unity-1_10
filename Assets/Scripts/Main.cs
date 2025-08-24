using UnityEngine;

public class Main : MonoBehaviour  
{
    private static Main instance;
    
    private void Awake()  
    {
        if (instance != null && instance != this)  
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}
