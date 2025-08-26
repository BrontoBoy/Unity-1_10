using UnityEngine;
using System.Collections.Generic;

public class CubeExploder : MonoBehaviour
{
    [Header("Настройки взрыва")]
    [SerializeField] private float _torqueMultiplier = 0.1f;
    
    public void ExplodeCubes(List<Cube> cubes, Vector3 explosionCenter, float force, float radius)
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();
        
        foreach (Cube cube in cubes)
        {
            Rigidbody rb = cube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rigidbodies.Add(rb);
            }
        }
        
        ApplyExplosion(rigidbodies, explosionCenter, force, radius);
    }
    
    private void ApplyExplosion(List<Rigidbody> rigidbodies, Vector3 explosionCenter, float force, float radius)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddExplosionForce(force, explosionCenter, radius);
            rb.AddTorque(Random.insideUnitSphere * force * _torqueMultiplier, ForceMode.Impulse);
        }
    }
}