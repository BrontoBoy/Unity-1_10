using UnityEngine;
using System.Collections.Generic;

public class GameCoordinator : MonoBehaviour
{
    [Header("Настройки игры")]
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _initialSplitChance = 1f;

    [Header("Настройки вероятности")]
    [SerializeField] private float _chanceMultiplier = 0.5f;

    [Header("Системы игры")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private RaycastController _raycastController;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private CubeExploder _cubeExploder;
    
    private void Awake()
    {
        if (_inputReader == null)
        {
            _inputReader = FindObjectOfType<InputReader>();
        }

        if (_raycastController == null)
        {
            _raycastController = FindObjectOfType<RaycastController>();
        }

        if (_cubeSpawner == null)
        {
            _cubeSpawner = FindObjectOfType<CubeSpawner>();
        }

        if (_cubeExploder == null)
        {
            _cubeExploder = FindObjectOfType<CubeExploder>();
        }
    }

    private void OnEnable()
    {
        _inputReader.Clicked += HandleClick;
        _raycastController.CubeFound += HandleCubeFound;
    }

    private void OnDisable()
    {
        _inputReader.Clicked -= HandleClick;
        _raycastController.CubeFound -= HandleCubeFound;
    }

    private void HandleClick(Vector3 mousePosition)
    {
        _raycastController.CastRayFromMousePosition(mousePosition);
    }

    private void HandleCubeFound(Cube cube)
    {
        bool canSplit = CanCubeSplit(cube);

        if (canSplit)
        {
            Vector3 cubePosition = cube.transform.position;
            Vector3 cubeScale = cube.CubeSize;
            
            float newSplitChance = _initialSplitChance * _chanceMultiplier;
            
            List<Cube> newCubes = _cubeSpawner.CreateChildCubes(cubePosition, cubeScale, newSplitChance);
            _cubeExploder.ExplodeCubes(newCubes, cubePosition, _explosionForce, _explosionRadius);
        }

        _cubeSpawner.DestroyCube(cube.gameObject);
    }

    private bool CanCubeSplit(Cube cube)
    {
        float currentChance = cube.CurrentSplitChance;
        float newChance = currentChance * _chanceMultiplier;
        
        return Random.Range(0f, 1f) <= newChance;
    }
}