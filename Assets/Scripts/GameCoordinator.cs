using UnityEngine;
using System.Collections.Generic;

public class GameCoordinator : MonoBehaviour
{
    [Header("Настройки игры")]
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _initialSplitChance = 1f;

    [Header("Системы игры")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private MouseClickRaycaster _mouseClickRaycaster;
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private CubeExploder _cubeExploder;
    
    private void Awake()
    {
        FindMissingReferences();
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    
    private void FindMissingReferences()
    {
        if (_inputReader == null)
        {
            _inputReader = FindObjectOfType<InputReader>();
        }

        if (_mouseClickRaycaster == null)
        {
            _mouseClickRaycaster = FindObjectOfType<MouseClickRaycaster>();
        }

        if (_cubeFactory == null)
        {
            _cubeFactory = FindObjectOfType<CubeFactory>();
        }

        if (_cubeExploder == null)
        {
            _cubeExploder = FindObjectOfType<CubeExploder>();
        }
    }

    private void SubscribeToEvents()
    {
        _inputReader.Clicked += HandleClick;
        _mouseClickRaycaster.CubeClicked += HandleCubeClick;
    }

    private void UnsubscribeFromEvents()
    {
        _inputReader.Clicked -= HandleClick;
        _mouseClickRaycaster.CubeClicked -= HandleCubeClick;
    }
    
    private void HandleClick(Vector3 mousePosition)
    {
        _mouseClickRaycaster.CastRayFromMousePosition(mousePosition);
    }

    private void HandleCubeClick(Cube cube)
    {
        if (CanCubeSplit(cube))
        {
            SplitCube(cube);
        }
        
        _cubeFactory.DestroyCube(cube);
    }

    private bool CanCubeSplit(Cube cube)
    {
        return Random.Range(0f, 1f) <= cube.SplitChance;
    }

    private void SplitCube(Cube cube)
    {
        Vector3 cubePosition = cube.transform.position;
        
        List<Cube> newCubes = _cubeFactory.CreateCubes(cubePosition, cube);
        _cubeExploder.ExplodeCubes(newCubes, cubePosition, _explosionForce, _explosionRadius);
    }
}