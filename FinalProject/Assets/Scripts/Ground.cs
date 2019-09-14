using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public System.Action _Killed;

    public void Initialize(System.Action Onkilled)
    {
        _Killed += Onkilled;
    }

        void Update()
    {
        
    }
}
