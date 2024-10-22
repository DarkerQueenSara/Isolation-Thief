﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HackingMinigameController : MonoBehaviour, ILogicGate
{
    public static HackingMinigameController Instance;
    private Action<int> gameEndCallback;
    private bool isShown = false;
    private bool gameStarted = false;
    private bool won = false;
    private GameObject endLightOn;
    private GameObject endPopup;
    private Button quitButton;
    private int remainingTries;

    public const int LEFT_GAME = 0;
    public const int WON_GAME = 1;
    public const int LOST_GAME = 2;
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
        quitButton = transform.Find("TopLeft").Find("QuitButton").GetComponent<Button>();
        quitButton.onClick.AddListener(QuitBeforeEnd);
        Button quitButton2 = endPopup.transform.Find("QuitButton").GetComponent<Button>();
        quitButton2.onClick.AddListener(Quit);
    }
    private void Start()
    {
        endPopup.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void StartMinigame(Action<int> gameEndCallback, int remainingTries)
    {
        this.gameEndCallback = gameEndCallback;
        //this.gameObject.SetActive(true);
        won = false;
        this.remainingTries = remainingTries;
        TextMeshProUGUI result = transform.Find("RemainingTries").GetComponent<TextMeshProUGUI>();
        result.text = "Remaining tries: " + remainingTries;
        changeVisibility();
        StartCoroutine(gameStartedDelay());
    }

    private IEnumerator gameStartedDelay()
    {
        yield return new WaitForSeconds(0.1f);
        gameStarted = true;
    }

    public void EndGame()
    {
        StartCoroutine(showEndPopup());
        quitButton.interactable = false;
        TextMeshProUGUI result = transform.Find("RemainingTries").GetComponent<TextMeshProUGUI>();
        result.text = "Remaining tries: " + (this.remainingTries - 1);
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

    private void QuitBeforeEnd()
    {
        changeVisibility();
        gameEndCallback?.Invoke(LEFT_GAME);
    }

    private void Quit()
    {
        changeVisibility();
        if (won)
        {
            gameEndCallback?.Invoke(WON_GAME);
        }
        else
        {
            gameEndCallback?.Invoke(LOST_GAME);
        }
        this.reloadUI();

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

    public void reloadUI()
    {
        gameStarted = false;
        won = false;
        endLightOn?.SetActive(true);
        endPopup.SetActive(false);
        foreach (Transform wire in transform.Find("LogicGateMinigame").Find("Wires"))
        {
            WireController wireController = (WireController)wire.GetComponent<WireController>();
            if(wireController != null)
            {
                wireController.Reload();
            }
        }

    }
}
