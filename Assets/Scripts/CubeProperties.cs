using UnityEngine;

// Этот скрипт отвечает за все начальные свойства куба
public class CubeProperties : MonoBehaviour  
{
    [SerializeField] private Vector3 cubeSize = Vector3.one;
    
    private void Start()  
    {
        ApplyCubeSize();
        ApplyCubeColor();
    }
    
    private void ApplyCubeSize()  
    {
        transform.localScale = cubeSize;
    }
    
    private void ApplyCubeColor()  
    {
        Renderer cubeRenderer = GetComponent<Renderer>(); 
        
        if (cubeRenderer != null) 
        {
            cubeRenderer.material.color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            1f
            );
        }
    }
    
    private void OnValidate()  
    {
        SynchronizeCubeDimensions();
    }
    
    private void SynchronizeCubeDimensions()  
    {
        if (cubeSize.x != cubeSize.y || cubeSize.x != cubeSize.z)  
        {
            cubeSize = new Vector3(cubeSize.x, cubeSize.x, cubeSize.x);  
        }
    }
    
    public void SetCubeSize(float newSize)  
    {
        cubeSize = new Vector3(newSize, newSize, newSize);
        ApplyCubeSize();
    }
    
    public Vector3 GetCubeSize()  
    {
        return cubeSize;
    }
}