using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class XorGate : MonoBehaviour, ILogicGate
{
    public GameObject previousL, previousR;
    public List<GameObject> nextObjects;
    public List<GameObject> nextLines;

    private ILogicGate previousLGate, previousRGate;
    private List<ILogicGate> nextGates;
    private bool left = false, right = false;

    private void Awake()
    {
        previousLGate = previousL?.GetComponent<ILogicGate>();
        previousRGate = previousR?.GetComponent<ILogicGate>();
        nextGates = new List<ILogicGate>();
        foreach (GameObject obj in nextObjects)
        {
            nextGates.Add(obj.GetComponent<ILogicGate>());
        }
    }

    public void updateLogic(bool logic, ILogicGate updater)
    {
        if(updater == previousLGate)
        {
            left = logic;
        }
        else
        {
            right = logic;
        }
        foreach (ILogicGate nextGate in nextGates)
        {
            nextGate?.updateLogic(left ^ right, this);
            updateLines(left ^ right);
        }
    }
    void updateLines(bool isOn)
    {
        foreach (GameObject nextLine in nextLines)
        {
            Image image = nextLine?.GetComponent<Image>();
            if (image != null)
            {
                if (isOn)
                {
                    image.color = new Color32(0, 255, 255, 255);
                }
                else
                {
                    image.color = new Color32(56, 56, 56, 255);
                }
            }
        }
    }
}