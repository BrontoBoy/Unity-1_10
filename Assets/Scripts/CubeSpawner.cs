using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 6;
    [SerializeField] private float _spawnOffset = 0.5f;
    [SerializeField] private float _scaleMultiplier = 0.5f;
    
    public List<GameObject> CreateChildCubes(Vector3 position, Vector3 parentScale, float splitChance)
    {
        List<GameObject> newCubes = new List<GameObject>();
        int cubeCount = Random.Range(_minCubes, _maxCubes + 1);
        Vector3 newScale = parentScale * _scaleMultiplier;

        for (int i = 0; i < cubeCount; i++)
        {
            GameObject newCube = CreateSingleCube(position, newScale, splitChance);
            newCubes.Add(newCube);
        }

        return newCubes;
    }
    
    public GameObject CreateSingleCube(Vector3 position, Vector3 scale, float splitChance)
    {
        Vector3 spawnPosition = position + Random.insideUnitSphere * _spawnOffset;
        GameObject newCube = Instantiate(_cubePrefab, spawnPosition, Random.rotation);
        CubeProperties props = newCube.GetComponent<CubeProperties>();
        props.SetCubeSize(scale.x);
        props.SetSplitChance(splitChance);

        return newCube;
    }

    public void DestroyCube(GameObject cube)
    {
        Destroy(cube);
    }
}