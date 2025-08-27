using UnityEngine;

public class RaycastController : MonoBehaviour
{
    private Camera _mainCamera;
    public event System.Action<Cube> CubeFound;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void CastRayFromMousePosition(Vector3 mousePosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out Cube cube))
            {
                CubeFound?.Invoke(cube);
            }
        }
    }
}