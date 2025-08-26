using UnityEngine;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Vector3 _cubeSize = Vector3.one;
    [SerializeField] private float _minColorValue = 0f;
    [SerializeField] private float _maxColorValue = 1f;
    
    private float _currentSplitChance = 1f;
    private Renderer _cubeRenderer;
    
    private void Awake()
    {
        _cubeRenderer = GetComponent<Renderer>();
    }

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
        if (_cubeRenderer != null)
        {
            _cubeRenderer.material.color = new Color(
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
        _currentSplitChance = chance;
    }

    public float GetCurrentSplitChance()
    {
        return _currentSplitChance;
    }
}
