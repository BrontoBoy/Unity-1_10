using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Vector3 _cubeSize = Vector3.one;
    private float _splitChance = 1f;
    private Renderer _cubeRenderer;
    
    public Vector3 CubeSize => _cubeSize;
    public float CurrentSplitChance => _splitChance;

    private void Awake()
    {
        _cubeRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        ApplyCubeSize();
    }

    private void ApplyCubeSize()
    {
        transform.localScale = _cubeSize;
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

    public void SetSplitChance(float chance)
    {
        _splitChance = chance;
    }
}
