using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Core
{
    public class Debug : MonoBehaviour
    {
        public const string DEBUG_FLAG = "DEBUG_BUILD";

        [Conditional(DEBUG_FLAG)]
        static public void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
        [Conditional(DEBUG_FLAG)]
        static public void LogWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        static public void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}