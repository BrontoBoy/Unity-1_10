using UnityEngine;
using System.Collections.Generic;

public class CubeExploder : MonoBehaviour
{
    [Header("Настройки взрыва")]
    [SerializeField] private float _torqueMultiplier = 0.1f;
    [SerializeField] private float _fullForceMultiplier = 1f;
    
    public void ExplodeCubes(List<GameObject> cubes, Vector3 explosionCenter, float force, float radius)
    {
        foreach (GameObject cube in cubes)
        {
            ApplyExplosionToCube(cube, explosionCenter, force, radius);
        }
    }
    
    private void ApplyExplosionToCube(GameObject cube, Vector3 explosionCenter, float force, float radius)
    {
        Rigidbody cubeRb = cube.GetComponent<Rigidbody>();
        
        if (cubeRb != null)
        {
            Vector3 direction = (cube.transform.position - explosionCenter);
            float distanceSqr = direction.sqrMagnitude;
            float radiusSqr = radius * radius;
            
            if (distanceSqr < radiusSqr)
            {
                direction.Normalize();
                float distance = Mathf.Sqrt(distanceSqr);
                float explosionForce = force * (_fullForceMultiplier - distance / radius);
                cubeRb.AddForce(direction * explosionForce, ForceMode.Impulse);
                cubeRb.AddTorque(Random.insideUnitSphere * explosionForce * _torqueMultiplier, ForceMode.Impulse);
            }
        }
    }
}