using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HackingMinigameController : MonoBehaviour, ILogicGate
{
    public static HackingMinigameController Instance;
    private Action<Boolean> gameEndCallback;
    private bool isShown = false;
    private bool gameStarted = false;
    private bool won = false;
    private GameObject endLightOn;
    private GameObject endPopup;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of HackingMinigameController found!");
        }
        #endregion

        endLightOn = transform.Find("LogicGateMinigame").Find("EndLight").Find("LightOn").gameObject;
        endPopup = this.transform.Find("EndPopup").gameObject;
        Button quitButton = endPopup.transform.Find("QuitButton").GetComponent<Button>();
        quitButton.onClick.AddListener(Quit);
    }
    private void Start()
    {
        endPopup.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void StartMinigame(Action<Boolean> gameEndCallback)
    {
        this.gameEndCallback = gameEndCallback;
        //this.gameObject.SetActive(true);
        gameStarted = true;
        won = false;
        changeVisibility();
    }

    public void EndGame()
    {
        StartCoroutine(showEndPopup());
    }

    private IEnumerator showEndPopup()
    {
        yield return new WaitForSeconds(0.5f);

        TextMeshProUGUI result = endPopup.transform.Find("Result").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI description = endPopup.transform.Find("Description").GetComponent<TextMeshProUGUI>();

        result.text = won ? "Hack succeeded" : "Hack failed";
        description.text = won ? "Device was successfuly hacked" : "Device could not be hacked";

        endPopup.SetActive(true);
    }

    private void Quit()
    {
        changeVisibility();
        gameEndCallback?.Invoke(won);
    }

    public void changeVisibility()
    {
        isShown = !isShown;

        if (isShown)
        {
            gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //crosshair.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //crosshair.SetActive(true);
        }
    }

    public void updateLogic(bool logic, ILogicGate updater)
    {
        endLightOn?.SetActive(logic);
        if (gameStarted)
        {
            Debug.Log("Logic updated: " + logic);
            won = !logic;
            EndGame();
        }
    }
}
