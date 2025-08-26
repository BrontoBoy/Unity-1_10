using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 6;
    [SerializeField] private float _spawnOffset = 0.5f;
    [SerializeField] private float _scaleMultiplier = 0.5f;
    
    public List<Cube> CreateChildCubes(Vector3 position, Vector3 parentScale, float splitChance)
    {
        List<Cube> newCubes = new List<Cube>();
        int cubeCount = Random.Range(_minCubes, _maxCubes + 1);
        Vector3 newScale = parentScale * _scaleMultiplier;

        for (int i = 0; i < cubeCount; i++)
        {
            Cube newCube = CreateSingleCube(position, newScale, splitChance);
            newCubes.Add(newCube);
        }

        return newCubes;
    }
    
    public Cube CreateSingleCube(Vector3 position, Vector3 scale, float splitChance)
    {
        Vector3 spawnPosition = position + Random.insideUnitSphere * _spawnOffset;
        Cube newCube = Instantiate(_cubePrefab, spawnPosition, Random.rotation);
        newCube.SetCubeSize(scale.x);
        newCube.SetSplitChance(splitChance);

        return newCube;
    }
    
    public void DestroyCube(GameObject cube)
    {
        Destroy(cube);
    }
}