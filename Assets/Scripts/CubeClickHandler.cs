using UnityEngine;

public class CubeClickHandler : MonoBehaviour  
{
    private float currentSplitChance = 1f; 
    private CubeSplitter cubeSplitter;
    
    private void Start()  
    {
        cubeSplitter = GetComponent<CubeSplitter>();

        if (cubeSplitter == null)
        {
            cubeSplitter = gameObject.AddComponent<CubeSplitter>();
        }  
    }
    
    private void OnMouseDown()  
    {
        HandleCubeClick();
    }
    
    private void HandleCubeClick()  
    {
        bool shouldSplit = cubeSplitter.CheckShouldSplit(currentSplitChance);  
        Vector3 cubePosition = transform.position;  
        CubeProperties props = GetComponent<CubeProperties>();  
        Vector3 cubeScale = props.GetCubeSize();  
        cubeSplitter.ProcessSplit(shouldSplit, cubePosition, cubeScale, currentSplitChance);  
        Destroy(gameObject);
    }
    
    public void SetSplitChance(float newChance)  
    {
        currentSplitChance = newChance;
    }
}