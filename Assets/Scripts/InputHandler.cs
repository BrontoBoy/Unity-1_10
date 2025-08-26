using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const int LEFT_MOUSE_BUTTON = 0;
    
    public event System.Action<Vector3> MouseButtonPressed;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
        {
            Vector3 mousePosition = Input.mousePosition;
            MouseButtonPressed?.Invoke(mousePosition);
        }
    }
}