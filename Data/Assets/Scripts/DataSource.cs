using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataSource
{
    string Id { get; set; }
    bool IsLoading { get; set; }
    bool IsLoaded { get; set; }
    string LoadError { get; set; }
    DateTime LoadedTIme { get; set; }
    IEnumerator LoadAsync();
    void Load();
    void HandleOnLoaded();
}
