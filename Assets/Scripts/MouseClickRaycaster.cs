using UnityEngine;

public class MouseClickRaycaster : MonoBehaviour
{
    private Camera _mainCamera;
    
    public event System.Action<Cube> CubeClicked;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
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