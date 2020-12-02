using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndText : MonoBehaviour
{
    Transform Data;
    TextMeshProUGUI Title;
    TextMeshProUGUI Goal;
    TextMeshProUGUI Value;
    TextMeshProUGUI Result;
    public Button yourButton;
    bool hasText;


    #region SINGLETON
    public static LevelEndText instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InteractionTextManager found!");
        }
        instance = this;
        Data = transform.Find("Data");
        Title = Data.Find("Title").GetComponent<TextMeshProUGUI>();
        Goal = Data.Find("Goal").GetComponent<TextMeshProUGUI>();
        Value = Data.Find("Value").GetComponent<TextMeshProUGUI>();
        Result = Data.Find("Result").GetComponent<TextMeshProUGUI>();
        Button btn = Data.Find("Button").GetComponent<Button>();
        btn.onClick.AddListener(Quit);
    }
    #endregion

    public void Quit()
    {
        Debug.Log("Clicked quit");
        Application.Quit();
    }

    public void setText(string title, string goal, string value, string result, bool win)
    {
        //TODO Check if it has text?

        Title.text = title;
        Goal.text = goal;
        Value.text = value;
        Result.text = result;

        if (win) Result.color = Color.green;
        else Result.color = Color.red;

        Data.gameObject.SetActive(true);
    }

    public bool HasText()
    {
        return hasText;
    }
}
