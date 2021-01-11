using UnityEngine;
using System.Collections;

public class ItemWrapper
{
    public Item item;
    public int id;
    public GameObject itemObject;

    public ItemWrapper(Item item, int id, GameObject itemObject)
    {
        this.item = item;
        this.id = id;
        this.itemObject = itemObject;
    }
}