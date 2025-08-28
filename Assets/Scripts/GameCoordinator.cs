using UnityEngine;
using System.Collections.Generic;

public class GameCoordinator : MonoBehaviour
{
    [Header("Настройки игры")]
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _initialSplitChance = 1f;

    [Header("Системы игры")]
    [SerializeField] private MouseClickRaycaster _mouseClickRaycaster;
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private CubeExploder _cubeExploder;
    
    private void OnEnable()
    {
        if (_mouseClickRaycaster != null)
        {
            _mouseClickRaycaster.CubeClicked += HandleCubeClick;
        }
    }

    private void OnDisable()
    {
        if (_mouseClickRaycaster != null)
        {
            _mouseClickRaycaster.CubeClicked -= HandleCubeClick;
        }
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