using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;
    public GameObject HealthBar;
    public GameObject GameOverPage;

    private void Awake()
    {
        Instance = this;
    }
    
    
    
}
