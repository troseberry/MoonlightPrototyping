using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalNavBarController : MonoBehaviour
{
    public static GlobalNavBarController Instance;

    private AppController currentOpenAppController;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        // Debug.Log("Current App: " + currentOpenAppController);
    }
    
    public void SetCurrentOpenApp(AppController newApp)
    {
        currentOpenAppController = newApp;
        currentOpenAppController.OpenApp();
    }

    public AppController GetCurrentOpenApp() { return currentOpenAppController; }

    public void HomeButtonPressed()
    {
        if (!currentOpenAppController) return;

        currentOpenAppController.CloseApp();
        currentOpenAppController = null;
    }

    public void BackButtonPressed()
    {
        if (!currentOpenAppController) return;

        currentOpenAppController.BackButtonPressed();
    }
}
