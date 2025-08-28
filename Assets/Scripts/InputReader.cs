using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InputReader : MonoBehaviour
{
    private const int LeftMouseButton = 0;
    public event System.Action<Vector3> Clicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Clicked?.Invoke(Input.mousePosition);
        }
    }
}