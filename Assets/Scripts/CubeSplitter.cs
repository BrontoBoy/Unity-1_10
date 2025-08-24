using UnityEngine;
using System.Collections.Generic;

public class CubeSplitter : MonoBehaviour  
{
    [SerializeField] private float explosionPower = 50f;
    [SerializeField] private float explosionRange = 5f;
    [SerializeField] private GameObject cubePrefab;
    
    public bool CheckShouldSplit(float splitChance)  
    {
        float newSplitChance = splitChance * 0.5f;
        bool shouldSplit = Random.Range(0f, 1f) <= newSplitChance;
        bool chanceIsBigEnough = newSplitChance > 0.01f;
        return shouldSplit && chanceIsBigEnough;  
    }
    
    public void ProcessSplit(bool shouldSplit, Vector3 position, Vector3 scale, float parentChance)  
    {
        if (shouldSplit)
        {
            CreateNewCubes(position, scale, parentChance);
        }
    }
    
    private void CreateNewCubes(Vector3 spawnPosition, Vector3 originalScale, float parentSplitChance)  
    {
        if (cubePrefab == null)  
        {
            Debug.LogError("Префаб не найден!"); 
            
            return; 
        }
        
        int cubeCount = Random.Range(2, 7);
        Vector3 newScale = originalScale * 0.5f;
        float newSplitChance = parentSplitChance * 0.5f; 
        List<GameObject> newCubes = new List<GameObject>();

        for (int i = 0; i < cubeCount; i++)  
        {
            Vector3 pos = spawnPosition + Random.insideUnitSphere * 0.5f;  
            GameObject newCube = Instantiate(cubePrefab, pos, Random.rotation);  
            CubeProperties props = newCube.GetComponent<CubeProperties>();

            if (props != null)
            {
                props.SetCubeSize(newScale.x);
            }  
            
            CubeClickHandler handler = newCube.GetComponent<CubeClickHandler>();

            if (handler != null)
            {
                handler.SetSplitChance(newSplitChance);
            }  
            
            newCubes.Add(newCube);
        }

        ApplyExplosionForce(newCubes, spawnPosition);
    }
    
    private void ApplyExplosionForce(List<GameObject> cubes, Vector3 explosionCenter)  
    {
        foreach (GameObject cube in cubes)  
        {
            Rigidbody cubeBody = cube.GetComponent<Rigidbody>();  
            
            if (cubeBody != null)  
            {
                Vector3 direction = (cube.transform.position - explosionCenter).normalized;  
                float distance = Vector3.Distance(cube.transform.position, explosionCenter);  
                float force = explosionPower * (1f - Mathf.Clamp01(distance / explosionRange));  
                cubeBody.AddForce(direction * force, ForceMode.Impulse);  
                cubeBody.AddTorque(Random.insideUnitSphere * force * 0.1f, ForceMode.Impulse);  
            }
        }
    }
}
