using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionTextManager : MonoBehaviour
{
    Text textUI;
    bool hasText;


    #region SINGLETON
    public static InteractionTextManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of InteractionTextManager found!");
        }
        instance = this;
        textUI = GetComponent<Text>();
    }
    #endregion

    public void setInteractingText(string text)
    {
        hasText = !"".Equals(text);
        this.textUI.text = text;

    }

    public bool HasText()
    {
        return hasText;
    }
}
