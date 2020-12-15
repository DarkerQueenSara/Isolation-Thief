﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject itemSlotPrefab;
    private Inventory inventory;

    private Transform uiInventory;

    private Transform stolenItems;
    private Transform itemSlot;

    private Transform info;

    public TextMeshProUGUI stolenValueText; // "Stolen : " + stolenValue
    private TextMeshProUGUI missingValueText; // missingValue
    private TextMeshProUGUI goalValueText; // "Goal : " + goalValue
    bool showInventory = false;



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

        gameObject.SetActive(false);
    }

    private void Start()
    {
        this.goalValueText.text = "Goal : 2500$";
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
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

    public bool isVisible()
    {
        return showInventory;
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