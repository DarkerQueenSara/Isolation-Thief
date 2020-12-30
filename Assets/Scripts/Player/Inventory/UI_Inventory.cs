using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject itemSlotPrefab;
    public GameObject gadgetSlotPrefab;
    public GameObject leftHandPrefab;
    public GameObject rightHandPrefab;
    private Inventory inventory;

    private Transform uiInventory;

    private Transform stolenItems;
    private Transform itemSlot;

    private Transform info;

    public TextMeshProUGUI stolenValueText; // "Stolen : " + stolenValue
    private TextMeshProUGUI missingValueText; // missingValue
    private TextMeshProUGUI goalValueText; // "Goal : " + goalValue
    bool showInventory = false;

    private Transform gadgetsStuff;
    private Transform leftHand;
    private Transform rightHand;
    private Transform leftContainer;
    private Transform rightContainer;

    private Player player;


    private void Awake()
    {
        uiInventory = transform.Find("Inventory");

        stolenItems = uiInventory.Find("Scroll View").Find("Viewport").Find("StolenItems");

        //TODO
        itemSlot = stolenItems.Find("itemSlot");

        info = uiInventory.Find("Info");
        stolenValueText = info.Find("Stolen").Find("stolenText").GetComponent<TextMeshProUGUI>();
        missingValueText = info.Find("MissingValue").Find("missingValueText").GetComponent<TextMeshProUGUI>();
        goalValueText = info.Find("Goal").Find("goalText").GetComponent<TextMeshProUGUI>();

        gadgetsStuff = uiInventory.Find("gadgets");
        leftHand = gadgetsStuff.Find("left_hand");
        rightHand = gadgetsStuff.Find("right_hand");
        leftContainer = gadgetsStuff.Find("container").Find("left");
        rightContainer = gadgetsStuff.Find("container").Find("right");

        gameObject.SetActive(false);
    }

    private void Start()
    {
        this.goalValueText.text = "Goal : 2500$";
        this.player = Player.Instance;
        RefreshInventoryGadgets();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    public void Refresh()
    {
        if (player == null)
        {
            player = Player.Instance;
        }
        RefreshInventoryItems();
        RefreshInventoryGadgets();
    }


    public void RefreshInventoryItems()
    {

        foreach (Transform child in stolenItems)
        {
           Destroy(child.gameObject);
        }

        foreach (Item item in inventory.GetItemList())
        {
            GameObject temp = Instantiate(itemSlotPrefab, stolenItems);
            temp.transform.Find("itemButton").Find("icon").GetComponent<Image>().sprite = item.GetSprite();
        }

        stolenValueText.text = "Total Value Stolen: " + inventory.getTotalValue();
        this.goalValueText.text = "Goal : 2500$";
        this.stolenValueText.text = "Stolen :" + inventory.getTotalValue() + "$";

        float missingValue = 2500 - inventory.getTotalValue();
        missingValue = missingValue < 0 ? 0 : missingValue;
        this.missingValueText.text = missingValue + "$";
    }

    public void RefreshInventoryGadgets()
    {
        #region destruction
        if(leftHand != null)
        {
            Destroy(leftHand.gameObject);
        }
        if(rightHand != null)
        {
            Destroy(rightHand.gameObject);
        }
        foreach (Transform child in leftContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in rightContainer)
        {
            Destroy(child.gameObject);
        }
        #endregion
        if (player.leftHand != null)
        {
            leftHand = createGadgetSlot(leftHandPrefab, gadgetsStuff, player.leftHand).transform;
        }

        if(player.rightHand != null)
        {
            rightHand = createGadgetSlot(rightHandPrefab, gadgetsStuff, player.rightHand).transform;
        }


        foreach (Gadget gadget in player.inInventory)
        {
            if (player.hasGadgetOnHand(gadget.getID())) continue;
            GameObject temp;
            if (gadget.getIsTypeF())
            {
                temp = Instantiate(gadgetSlotPrefab, leftContainer);
            }
            else
            {
                temp = Instantiate(gadgetSlotPrefab, rightContainer);
            }
            Button button = temp.transform.Find("GadgetButton").GetComponent<Button>();
            temp.transform.Find("GadgetButton").Find("name").GetComponent<TextMeshProUGUI>().text = gadget.getID();
            temp.transform.Find("GadgetButton").Find("icon").GetComponent<Image>().sprite = gadget.getSprite();
            button.onClick.AddListener(delegate {
                if (gadget.getIsTypeF())
                {
                    player.leftHand = gadget;
                }
                else
                {
                    player.rightHand = gadget;
                }
                this.Refresh();
            });
            //temp.transform.Find("itemButton").Find("icon").GetComponent<Image>().sprite = item.GetSprite();
        }
    }

    private GameObject createGadgetSlot(GameObject prefab, Transform place, Gadget gadget)
    {
        GameObject temp = Instantiate(prefab, place);
        temp.transform.Find("GadgetSlot").Find("GadgetButton").Find("name").GetComponent<TextMeshProUGUI>().text = gadget.getID();
        temp.transform.Find("GadgetSlot").Find("GadgetButton").Find("icon").GetComponent<Image>().sprite = gadget.getSprite();
        return temp;
    }

    public bool isVisible()
    {
        return showInventory;
    }
    public void visible()
    {
        showInventory = !showInventory;

        if (showInventory)
        {
            gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crosshair.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            crosshair.SetActive(true);
        }
    }
}