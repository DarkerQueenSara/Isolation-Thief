using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBag : Inventory
{
    public SimpleBag()
    {
        
    }

    public override void AddItem(Item item, GameObject itemObject)
    {
        if(item.weight + TotalWeight <= MAX_WEIGHT + 0.00001)
        {
            counter++;
            this.TotalValue += item.value;
            this.TotalWeight += item.weight;
            itemObject.SetActive(false);
            ItemWrapper itemWrapper = new ItemWrapper(item, counter, itemObject);
            this.Items.Add(itemWrapper);
        }
        else
        {
            //Launch error saing that inventory is too heavy to add item
        }
    }
}
