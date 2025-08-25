using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class GameCoordinator : MonoBehaviour  
{
    [Header("Настройки игры")]
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _initialSplitChance = 1f;
    
    [Header("Системы игры")]
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private CubeExploder _cubeExploder;
    [SerializeField] private InputDetector _inputDetector;
    
    [Header("Настройки вероятности")]
    [SerializeField] private float _chanceMultiplier = 0.5f;
    [SerializeField] private float _minSplitChance = 0.01f;
    [SerializeField] private float _minRandomValue = 0f;
    [SerializeField] private float _maxRandomValue = 1f;
    
    private List<GameObject> _allCubes = new List<GameObject>();
    
    private void Start()
    {
        _inputDetector.OnCubeClicked += HandleCubeClick;_inputDetector.OnCubeClicked += HandleCubeClick;
    }
    
    private void HandleCubeClick(GameObject clickedCube, Vector3 cubeScale)
    {
        bool shouldSplit = CheckSplitChance(clickedCube);

        if (shouldSplit)
        {
            Vector3 cubePosition = clickedCube.transform.position;
            List<GameObject> newCubes = _cubeSpawner.CreateChildCubes(cubePosition, cubeScale, _initialSplitChance * 0.5f);
            _cubeExploder.ExplodeCubes(newCubes, cubePosition, _explosionForce, _explosionRadius);
        }
        
        _cubeSpawner.DestroyCube(clickedCube);
        _allCubes.Remove(clickedCube);
    }
    
    private bool CheckSplitChance(GameObject cube)
    {
        CubeProperties cubeProps = cube.GetComponent<CubeProperties>();

        if (cubeProps == null)
        {
            Debug.LogError("Объект не содержит компонент CubeProperties!");
            
            return false;
        }
        
        float currentChance = cubeProps.GetCurrentSplitChance();
        float newChance = currentChance * _chanceMultiplier;
        bool canSplit = newChance > _minSplitChance;
        bool shouldSplit = Random.Range(_minRandomValue, _maxRandomValue) <= newChance;

        return shouldSplit && canSplit;
    }
    
    public void RegisterCube(GameObject cube)
    {
        if (!_allCubes.Contains(cube))
        {
            _allCubes.Add(cube);
        }
    }
    
    public void UnregisterCube(GameObject cube)
    {
        _allCubes.Remove(cube);
    }
}
