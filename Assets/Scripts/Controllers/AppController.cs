﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    
    public virtual void OpenApp()
    {

    }

    public virtual void BackButtonPressed()
    {
        CloseApp();
    }

    public virtual void CloseApp()
    {
        gameObject.SetActive(false);
    }
}
