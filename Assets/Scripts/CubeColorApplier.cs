using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeColorApplier : MonoBehaviour
{
    [SerializeField] private float _minColorValue = 0f;
    [SerializeField] private float _maxColorValue = 1f;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        ApplyRandomColor();
    }

    private void ApplyRandomColor()
    {
        if (_renderer != null)
        {
            _renderer.material.color = new Color(
                Random.Range(_minColorValue, _maxColorValue),
                Random.Range(_minColorValue, _maxColorValue), 
                Random.Range(_minColorValue, _maxColorValue)
            );
        }
    }
}
