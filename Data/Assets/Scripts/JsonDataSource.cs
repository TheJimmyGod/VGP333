using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonFx;
public class JsonDataSource : ScriptableObject, IDataSource
{
    #region IDataSource Implemtation
    public string Id { get; set; }
    public bool IsLoading { get; set; }
    public bool IsLoaded { get; set; }
    public string LoadError { get; set; }
    public DateTime LoadedTIme { get; set; }


    public void OnEnable()
    {
        LoadError = string.Empty;
        IsLoading = false;
        DataDictionary = null;
        IsLoaded = false;
        LoadedTIme = System.DateTime.UtcNow;
        Id = string.Empty;
    }

    public void Load()
    {

    }

    #endregion
    public Action OnLoaded;
    public TextAsset JsonTextAsset;
    public Dictionary<string, object> DataDictionary;
    public IEnumerator LoadAsync()
    {
        try
        {
            object deserializedObject = JsonFx.Json.JsonReader.Deserialize(JsonTextAsset.text);
            if(deserializedObject is Dictionary<string,object>)
            {
                DataDictionary = (Dictionary<string, object>)deserializedObject;
                LoadedTIme = System.DateTime.UtcNow;
                Id = JsonTextAsset.name;
                IsLoaded = true;
            }
            else
            {
                this.LoadError = "TextAsset does not deserialize correctly. Check your JSON format";
            }
        }
        catch(System.Exception e)
        {
            LoadError = $"Exception occurred while trying to parse json: {e.Message}";
        }
        yield return new WaitForEndOfFrame();
    }
    public void HandleOnLoaded()
    {
        //if(OnLoaded != null)   |
        //{                      |
        //    OnLoaded.Invoke(); | == OnLoaded?.Invoke();
        //}                      |
        OnLoaded?.Invoke();
    }
}
