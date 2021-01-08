using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory
{
    public int TotalValue;
    protected List<Item> Items;

    public Inventory()
    {
        TotalValue = 0;
        Items = new List<Item>();
    }

    public abstract void AddItem(Item item);

    public virtual List<Item> GetItemList()
    {
        return this.Items;
    }

    public float getTotalValue()
    {
        return TotalValue;
    }

    public bool hasItem(string itemName)
    {
        foreach(Item item in Items)
        {
            //See if names are equal ignoring case
            if(string.Compare(item.name, itemName, true) == 0)
            {
                return true;
            }
        }
        return false;
    }

    public Item popItem(string itemName)
    {
        Item itemToPop = null;
        foreach (Item item in Items)
        {
            //See if names are equal ignoring case
            if (string.Compare(item.name, itemName, true) == 0)
            {
                itemToPop = item;
                break;
            }
        }
        if(itemToPop != null)
        {
            Items.Remove(itemToPop);
            Player.Instance.RefreshUIInventory();
        }
        return itemToPop;
    }

    // Update is called once per frame

}
