using UnityEngine;
using System;

public class InputDetector : MonoBehaviour
{
    public event Action<GameObject, Vector3> OnCubeClicked;
    
    private void Update()
    {
        CheckForCubeClick();
    }
    
    private void CheckForCubeClick()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Cube"))
                {
                    Vector3 cubeScale = hit.transform.localScale;
                    OnCubeClicked?.Invoke(hit.collider.gameObject, cubeScale);
                }
            }
        }
    }
}
