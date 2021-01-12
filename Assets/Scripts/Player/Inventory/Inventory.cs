using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory
{
    public const float MAX_WEIGHT = 100.0f;

    public int TotalValue;
    public float TotalWeight;
    protected List<ItemWrapper> Items;
    protected int counter;

    public Inventory()
    {
        TotalValue = 0;
        TotalWeight = 0.0f;
        Items = new List<ItemWrapper>();
        counter = 0;
    }

    public abstract void AddItem(Item item, GameObject itemObject);

    public virtual List<ItemWrapper> GetItemList()
    {
        return this.Items;
    }

    public float getTotalValue()
    {
        return TotalValue;
    }

    public float getTotalWeight()
    {
        return TotalWeight;
    }

    public bool hasItem(string itemName)
    {
        foreach(ItemWrapper itemW in Items)
        {
            //See if names are equal ignoring case
            if(string.Compare(itemW.item.name, itemName, true) == 0)
            {
                return true;
            }
        }
        return false;
    }

    public Item popItem(string itemName)
    {
        ItemWrapper itemToPop = null;
        foreach (ItemWrapper itemW in Items)
        {
            //See if names are equal ignoring case
            if (string.Compare(itemW.item.name, itemName, true) == 0)
            {
                itemToPop = itemW;
                break;
            }
        }
        if(itemToPop != null)
        {
            this.RemoveItem(itemToPop);
            Player.Instance.RefreshUIInventory();
        }
        return itemToPop.item;
    }

    public Item popItemDisappearingGameObject(string itemName)
    {
        ItemWrapper itemToPop = null;
        foreach (ItemWrapper itemW in Items)
        {
            //See if names are equal ignoring case
            if (string.Compare(itemW.item.name, itemName, true) == 0)
            {
                itemToPop = itemW;
                break;
            }
        }
        if (itemToPop != null)
        {
            this.DestroyItem(itemToPop);
            Player.Instance.RefreshUIInventory();
        }
        return itemToPop.item;
    }

    public void DestroyItem(ItemWrapper itemW)
    {
        if (itemW == null) return;
        Items.Remove(itemW);
        this.TotalValue -= itemW.item.value;
        this.TotalWeight -= itemW.item.weight;
    }

    public void RemoveItem(ItemWrapper itemW)
    {
        if (itemW == null) return;
        itemW.itemObject?.SetActive(true);
        this.TotalValue -= itemW.item.value;
        this.TotalWeight -= itemW.item.weight;
        Items.Remove(itemW);
    }

    public void DestroyItem(int itemID)
    {
        ItemWrapper toDestoy = null;
        foreach(ItemWrapper itemW in Items)
        {
            if(itemW.id == itemID)
            {
                toDestoy = itemW;
                break;
            }
        }
        this.DestroyItem(toDestoy);
    }

    public void RemoveItem(int itemID)
    {
        ItemWrapper toRemove = null;
        foreach (ItemWrapper itemW in Items)
        {
            if (itemW.id == itemID)
            {
                toRemove = itemW;
                break;
            }
        }
        this.RemoveItem(toRemove);
    }

    // Update is called once per frame

}
