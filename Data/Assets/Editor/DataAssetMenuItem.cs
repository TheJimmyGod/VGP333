using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ScriptableObjectUtillty
{
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        var asset = ScriptableObject.CreateInstance<T>();
        ProjectWindowUtil.CreateAsset(asset, "New " + typeof(T).Name + ".asset");
    }
}
public class DataAssetMenuItem : MonoBehaviour
{
    [MenuItem("Assets/Create/Data Source/Json Data Source", priority = 1)]
    public static void CreateJsonDataSource()
    {
        ScriptableObjectUtillty.CreateAsset<JsonDataSource>();
    }
}
