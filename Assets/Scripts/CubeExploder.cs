using UnityEngine;
using System.Collections.Generic;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private float _torqueMultiplier = 0.1f;
    
    public void ExplodeCubes(List<Cube> cubes, Vector3 explosionCenter, float force, float radius)
    {
        foreach (Cube cube in cubes)
        {
            if (cube.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(force, explosionCenter, radius);
                rigidbody.AddTorque(Random.insideUnitSphere * force * _torqueMultiplier, ForceMode.Impulse);
            }
        }
    }
}