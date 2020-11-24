using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        //Debug.Log("itemSlotContainer is null? " + itemSlotContainer == null);
        //Debug.Log("itemSlotTemplate is null? " + itemSlotTemplate == null);
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }


    private void RefreshInventoryItems()
    {
        int x = -1;
        int y = 1;
        float itemSlotCellSize = 30.0f;
        foreach(Item item in inventory.GetItemList())
        {
            Transform temp = Instantiate(itemSlotTemplate, itemSlotContainer);
            RectTransform itemSlotRectTransform = temp.GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x ++;
            if (x > 1)
            {
                x = -1;
                y--;
            }
        }
    }

}
