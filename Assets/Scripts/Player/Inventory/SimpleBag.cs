using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBag : Inventory
{


    // Start is called before the first frame update

    public SimpleBag()
    {
        this.Items.Add(new Item { itemType = Item.ItemType.TV, amount = 1});
        Debug.Log(Items.Count);
    }

    public override void AddItem(Item item)
    {
        this.Items.Add(item);
    }
}
