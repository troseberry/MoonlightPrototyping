using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAppController : AppController
{
    public static SnakeAppController Instance;

    public GameObject snakeGameObjects;
    public GameObject gameOverMenu;

    public GameObject snakePrefab;
    public GameObject foodPrefab;

    private Vector2 snakeStart;
    private Vector2 foodStart;

    void Start()
    {
        Instance = this;
        snakeStart = GameObject.Find("Snake").transform.position;
        foodStart = GameObject.Find("Food").transform.position;
    }

    
    public override void OpenApp()
    {
        snakeGameObjects.SetActive(true);
        // ResetGame();
    }

    public override void CloseApp()
    {
        HomeScreenController.Instance.ShowCanvas();
        base.CloseApp();
    }

    public override void BackButtonPressed()
    {
        base.BackButtonPressed();
    }

    public void ToggleGameOverMenu()
    {
        if (!gameOverMenu.activeSelf)
        {
            gameOverMenu.SetActive(true);
        }
        else
        {
            gameOverMenu.SetActive(false);
        }
    }

    public void ResetGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("Snake"));
        Destroy(GameObject.FindGameObjectWithTag("SnakeFood"));

        Instantiate(snakePrefab, snakeStart, Quaternion.identity, snakeGameObjects.transform);
        Instantiate(foodPrefab, foodStart, Quaternion.identity, snakeGameObjects.transform);

        ToggleGameOverMenu();
    }
}
