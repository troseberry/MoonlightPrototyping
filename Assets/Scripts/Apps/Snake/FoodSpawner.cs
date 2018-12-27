using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public static FoodSpawner Instance;

    public GameObject foodPrefab;

    public float gridHeightMax;
    public float gridHeightMin;
    public float gridWidthMax;
    public float gridWidthMin;
    
    void Start()
    {
        Instance = this;
    }

    
    void Update()
    {
        
    }

    public void SpawnFood()
    {
        float xPos = Random.Range(gridWidthMin, gridWidthMax);
        xPos = (Mathf.Round(xPos * 2)) / 2;

        float yPos = Random.Range(gridHeightMin, gridHeightMax);
        yPos = (Mathf.Round(yPos * 2)) / 2;

        Vector2 spawnPos = new Vector2 (xPos, yPos);
        Debug.Log("Food Spawn: " + spawnPos);

        Instantiate(foodPrefab, spawnPos, Quaternion.identity);
    }
}
