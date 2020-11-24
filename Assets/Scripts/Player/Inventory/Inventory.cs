using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory
{
    protected float TotalValue;
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

    // Update is called once per frame
    
}
