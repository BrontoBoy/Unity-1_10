using UnityEngine;
using System.Collections.Generic;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 6;
    [SerializeField] private float _spawnOffset = 0.5f;
    [SerializeField] private float _sizeMultiplier = 0.5f;
    [SerializeField] private float _chanceMultiplier = 0.5f;

    public List<Cube> CreateCubes(Vector3 position, Cube parentCube)
    {
        List<Cube> newCubes = new List<Cube>();
        float newSize = parentCube.Size.x * _sizeMultiplier;
        float newSplitChance = parentCube.SplitChance * _chanceMultiplier;
        int cubeCount = Random.Range(_minCubes, _maxCubes + 1);

        for (int i = 0; i < cubeCount; i++)
        {
            Cube newCube = CreateCube(position, newSize, newSplitChance);
            newCubes.Add(newCube);
        }

        return newCubes;
    }

    private Cube CreateCube(Vector3 position, float size, float splitChance)
    {
        Vector3 spawnPosition = position + Random.insideUnitSphere * _spawnOffset;
        Cube newCube = Instantiate(_cubePrefab, spawnPosition, Random.rotation);
        newCube.Initialize(size, splitChance);
        return newCube;
    }

    public void DestroyCube(Cube cube)
    {
        Destroy(cube.gameObject);
    }
}