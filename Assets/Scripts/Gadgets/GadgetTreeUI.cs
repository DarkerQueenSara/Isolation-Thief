using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GadgetTreeUI : MonoBehaviour
{
    public static GadgetTreeUI Instance { get; private set; }
    //public GameObject crosshair;

    private GadgetTree gadgetTree;
    private Player player;
    private bool isShown = false;
    private void Awake()
    {
        Instance = this;
        //transform.Find("SimpleLockPickBtn FastLockPickBtn LanternBtn")
        
    }

    void disableButton(Button button)
    {
        //button.enabled = false;
        button.transform.gameObject.SetActive(false);
    }
    void updateVisualUnocked(Button button, TextMeshProUGUI priceText)
    {
        button.transform.gameObject.SetActive(false);
        priceText.transform.gameObject.SetActive(false);
        updateMoneyText();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.player = Player.Instance;
        this.gadgetTree = player.GetGadgetTree();

        updateMoneyText();


        Button FastLockPickBtn = transform.Find("FastLockPick").Find("Button").GetComponent<Button>();
        TextMeshProUGUI FastLockPickPrice = transform.Find("FastLockPick").Find("Price").GetComponent<TextMeshProUGUI>();
        FastLockPickPrice.text = gadgetTree.GetGadget(FastLockpick.gadgetID).getCost().ToString();
        FastLockPickBtn.onClick.AddListener(delegate {
            //Debug.Log("Clicked FastLockPick!");
            Gadget gadget = gadgetTree.GetGadget(FastLockpick.gadgetID);
            if (gadget.canUnlock())
            {
                Debug.Log("unlocking gadget!");
                gadget.unlock();
                updateVisualUnocked(FastLockPickBtn, FastLockPickPrice);
            }
            else
            {
                Debug.Log("Can't unlock gadget!");
            }
        });



        gameObject.SetActive(false);
    }

    private void updateMoneyText()
    {
        transform.Find("MoneyText").GetComponent<TextMeshProUGUI>().text = "Money: " + player.getMoney().ToString();
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
}
