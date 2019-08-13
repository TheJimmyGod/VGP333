using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Text _loadingText;
    public Slider _condition;

    public LoadingScreen Init()
    {
        _loadingText.text = "";
        _condition.value = 0.0f;
        return this;
    }

    public void UpdateLoadingStep(string text)
    {
        _loadingText.text = text;
    }

    public void UpdateLoadingBar (float complete)
    {
        _condition.value = complete;
    }
}
