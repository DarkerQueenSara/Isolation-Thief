using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        TV,
        Laptop,
        Phone,
        Tablet,
        Gold,
        Chash,
    }

    public ItemType itemType;
    public int amount;
    public int value;
}
