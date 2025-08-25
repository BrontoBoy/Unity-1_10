using UnityEngine;
using UnityEngine.Serialization;

public class CubeProperties : MonoBehaviour
{
    [SerializeField] private Vector3 _cubeSize = Vector3.one;
    private float currentSplitChance = 1f;
    
    [Header("Настройки цвета")]
    [SerializeField] private float _minColorValue = 0f;
    [SerializeField] private float _maxColorValue = 1f;
    
    private void Start()
    {
        ApplyCubeSize();
        ApplyRandomColor();
    }
    
    private void ApplyCubeSize()
    {
        transform.localScale = _cubeSize;
    }
    
    private void ApplyRandomColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = new Color(
                Random.Range(_minColorValue, _maxColorValue),
                Random.Range(_minColorValue, _maxColorValue), 
                Random.Range(_minColorValue, _maxColorValue)
            );
        }
    }
    
    private void OnValidate()
    {
        _cubeSize = new Vector3(_cubeSize.x, _cubeSize.x, _cubeSize.x);
    }

    public void SetCubeSize(float size)
    {
        _cubeSize = new Vector3(size, size, size);
        ApplyCubeSize();
    }

    public Vector3 GetCubeSize()
    {
        return _cubeSize;
    }

    public void SetSplitChance(float chance)
    {
        currentSplitChance = chance;
    }

    public float GetCurrentSplitChance()
    {
        return currentSplitChance;
    }
}
