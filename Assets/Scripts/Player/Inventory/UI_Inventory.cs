using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    public GameObject crosshair;
    private Inventory inventory;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    public Text totalValueText;
    public float totalValue;
    bool showInventory = false;



    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        totalValueText = transform.Find("infoText").Find("totalStolen").GetComponent<Text>();
        totalValue = 0;
        gameObject.SetActive(false);
        //Debug.Log("itemSlotContainer is null? " + itemSlotContainer == null);
        //Debug.Log("itemSlotTemplate is null? " + itemSlotTemplate == null);
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }


    public void RefreshInventoryItems()
    {
        totalValue = 0;
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
            totalValue += item.value;
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

        totalValueText.text = "Total Value Stolen: " + totalValue;
    }

    public void visible()
    {
        //Player.Instance.stopMovement();
        if (showInventory)
        {
            Debug.Log("showInv = true");
            showInventory = false;

        }
        else
        {
            Debug.Log("showInv = true");
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

    /*void Update()
    {
        //Show/Hide inventory
            Debug.Log("tab");
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Player.Instance.stopMovement();
            if (showInventory)
            {
                showInventory = false;

            } else
            {
                Debug.Log("showInv = true");
                showInventory = true;
            }

        }

        if (showInventory)
        {
            gameObject.SetActive(true);

        }
        else
        {
            gameObject.SetActive(false);
        }

    }*/
}