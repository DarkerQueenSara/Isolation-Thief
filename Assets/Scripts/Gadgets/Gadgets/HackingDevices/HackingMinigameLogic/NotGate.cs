using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NotGate : MonoBehaviour, ILogicGate
{
    public List<GameObject> nextObjects;
    public List<GameObject> nextLines;

    private List<ILogicGate> nextGates;

    private void Awake()
    {
        nextGates = new List<ILogicGate>();
        foreach (GameObject obj in nextObjects)
        {
            nextGates.Add(obj.GetComponent<ILogicGate>());
        }
    }
    public void updateLogic(bool logic, ILogicGate updater)
    {
        foreach (ILogicGate nextGate in nextGates)
        {
            nextGate?.updateLogic(!logic, this);
            updateLines(!logic);
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
