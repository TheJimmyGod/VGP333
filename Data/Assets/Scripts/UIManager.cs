using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    // Need hud;
    private Canvas canvas;
    public Text winloseText;
    private Text objCount;
    private Slider HPBar;

    private const string WIN_MESSAGE = "You Win!!";

    private void Awake()
    {
        Debug.Log("UI Manager Initializing");
        Init();
    }

    public UIManager Init()
    {
        winloseText.text = "";
        //objCount.text = "";
        return this;
    }

    public void SetWinText()
    {
        winloseText.text = WIN_MESSAGE;
    }

    public void UpdateObjectCount(int count)
    {
        objCount.text = count.ToString();
    }
}
