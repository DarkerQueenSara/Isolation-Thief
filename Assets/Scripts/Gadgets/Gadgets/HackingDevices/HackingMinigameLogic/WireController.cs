using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireController : MonoBehaviour, ILogicGate
{
    //public GameObject next;
    public List<GameObject> nextObjects;
    //public GameObject nextLine;
    public List<GameObject> nextLines;
    public bool isOn;
    //private ILogicGate nextGate;
    public List<ILogicGate> nextGates;
    private void Awake()
    {
        Button b = transform.Find("Button").GetComponent<Button>();
        b.onClick.AddListener(CutWire);
        transform.Find("Light_on").gameObject.SetActive(isOn);

        nextGates = new List<ILogicGate>();
        foreach (GameObject obj in nextObjects)
        {
            nextGates.Add(obj.GetComponent<ILogicGate>());
        }
    }

    private void Start()
    {
        updateWireLogic();
    }

    public void CutWire()
    {
        transform.Find("Wire_overlay").gameObject.SetActive(false);
        transform.Find("Button").gameObject.SetActive(false);
        isOn = false;
        updateWireLogic();
    }

    private void updateWireLogic()
    {
        foreach (ILogicGate nextGate in nextGates)
        {
            nextGate?.updateLogic(isOn, this);
        }

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

    public void updateLogic(bool logic, ILogicGate updater)
    {
        throw new System.NotImplementedException();
    }

    public void Reload()
    {
        transform.Find("Wire_overlay").gameObject.SetActive(true);
        transform.Find("Button").gameObject.SetActive(true);
        isOn = true;
        updateWireLogic();
    }
}
