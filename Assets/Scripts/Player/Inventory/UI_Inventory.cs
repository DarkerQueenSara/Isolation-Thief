using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    public GameObject crosshair;
    private Inventory inventory;

    private Transform uiInventory;

    private Transform stolenItems;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private Transform info;

    public TextMeshProUGUI stolenValueText; // "Stolen : " + stolenValue
    private TextMeshProUGUI missingValueText; // missingValue
    private TextMeshProUGUI goalValueText; // "Goal : " + goalValue
    bool showInventory = false;



    private void Awake()
    {
        uiInventory = transform.Find("Inventory");

        stolenItems = uiInventory.Find("StolenItems");
        itemSlotContainer = stolenItems.Find("itemSlotContainer");

        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        info = uiInventory.Find("Info");
        stolenValueText = info.Find("Stolen").Find("stolenText").GetComponent<TextMeshProUGUI>();
        missingValueText = info.Find("MissingValue").Find("missingValueText").GetComponent<TextMeshProUGUI>();
        goalValueText = info.Find("Goal").Find("goalText").GetComponent<TextMeshProUGUI>();

        gameObject.SetActive(false);
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }


    public void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = -5;
        int y = 5;
        float itemSlotCellSize = 30.0f;
        foreach (Item item in inventory.GetItemList())
        {
            Transform temp = Instantiate(itemSlotTemplate, itemSlotContainer);
            RectTransform itemSlotRectTransform = temp.GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize + 17, y * itemSlotCellSize -17);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if (x > 5)
            {
                x = -5;
                y--;
            }
        }

        stolenValueText.text = "Total Value Stolen: " + inventory.getTotalValue();
    }

    public void visible()
    {
        if (showInventory)
        {
            showInventory = false;

        }
        else
        {
            showInventory = true;
        }

        if (showInventory)
        {
            gameObject.SetActive(true);
            crosshair.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
            crosshair.SetActive(true);
        }
    }
}