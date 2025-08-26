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
    [SerializeField] private float _minSplitChance = 0.01f;
    
    [Header("Настройки случайности")]
    [SerializeField] private float _minRandomValue = 0f;
    [SerializeField] private float _maxRandomValue = 1f;

    [Header("Системы игры")]
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private CubeExploder _cubeExploder;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private RaycastController _raycastController;
    
    private void Awake()
    {
        if (_inputHandler == null)
        {
            _inputHandler = FindObjectOfType<InputHandler>();
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
        _inputHandler.MouseButtonPressed += HandleMouseClick;
        _raycastController.CubeFound += HandleCubeFound;
    }
    
    private void OnDisable()
    {
        _inputHandler.MouseButtonPressed -= HandleMouseClick;
        _raycastController.CubeFound -= HandleCubeFound;
    }
    
    private void HandleMouseClick(Vector3 mousePosition)
    {
        _raycastController.CastRayFromMousePosition(mousePosition);
    }
    
    private void HandleCubeFound(GameObject cubeObject)
    {
        bool canSplit = CanCubeSplit(cubeObject);

        if (canSplit)
        {
            Cube cube = cubeObject.GetComponent<Cube>();
            Vector3 cubePosition = cubeObject.transform.position;
            Vector3 cubeScale = cube.GetCubeSize();
            List<Cube> newCubes = _cubeSpawner.CreateChildCubes(cubePosition, cubeScale, _initialSplitChance * _chanceMultiplier);
            _cubeExploder.ExplodeCubes(newCubes, cubePosition, _explosionForce, _explosionRadius);
        }
        
        _cubeSpawner.DestroyCube(cubeObject);
    }
    
    private bool CanCubeSplit(GameObject cubeObject)
    {
        Cube cube = cubeObject.GetComponent<Cube>();
        
        if (cube == null)
        {
            Debug.LogError("Объект не куб!");
            return false;
        }
        
        float currentChance = cube.GetCurrentSplitChance();
        float newChance = currentChance * _chanceMultiplier;
        bool canSplit = newChance > _minSplitChance;
        bool shouldSplit = Random.Range(_minRandomValue, _maxRandomValue) <= newChance;

        return shouldSplit && canSplit;
    }
}