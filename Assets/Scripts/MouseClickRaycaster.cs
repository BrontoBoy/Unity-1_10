using UnityEngine;

public class MouseClickRaycaster : MonoBehaviour
{
    [Header("Зависимости")]
    [SerializeField] private InputReader _inputReader;
    
    private Camera _mainCamera;
    public event System.Action<Cube> CubeClicked;

    private void Awake()
    {
        _mainCamera = Camera.main;
        
        if (_inputReader == null)
        {
            Debug.LogError("InputReader не назначен в MouseClickRaycaster!");
            return;
        }
    }

    private void OnEnable()
    {
        _inputReader.Clicked += HandleClick;
    }

    private void OnDisable()
    {
        _inputReader.Clicked -= HandleClick;
    }

    private void HandleClick(Vector3 mousePosition)
    {
        CastRayFromMousePosition(mousePosition);
    }

    public void CastRayFromMousePosition(Vector3 mousePosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out Cube cube))
            {
                CubeClicked?.Invoke(cube);
            }
        }
    }
}