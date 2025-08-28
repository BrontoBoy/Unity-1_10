using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Vector3 _size = Vector3.one;
    
    public Vector3 Size => _size;
    public float SplitChance { get; private set; } = 1f;
    
    private void OnValidate()
    {
        _size = new Vector3(_size.x, _size.x, _size.x);
    }

    private void Start()
    {
        transform.localScale = _size;
    }

    public void Initialize(float size, float splitChance)
    {
        _size = new Vector3(size, size, size);
        SplitChance = splitChance;
        transform.localScale = _size;
    }
}