using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;

    private void Awake()
    {
        Debug.Log("UI Manager Initializing");
        Init();
    }

    private void Init()
    {

    }

    public void PrintSpeed(float speed)
    {
        Debug.Log("Player Speed: " + speed);
    }
}
