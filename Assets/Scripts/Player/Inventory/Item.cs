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
        Cash,
    }

    public ItemType itemType;
    public int amount;
    public int value;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.TV:       return ItemAssets.Instance.tvSprite;
            case ItemType.Laptop:   return ItemAssets.Instance.laptopSprite;
            case ItemType.Phone:    return ItemAssets.Instance.phoneSprite;
            case ItemType.Tablet:   return ItemAssets.Instance.tabletSprite;
            case ItemType.Gold:     return ItemAssets.Instance.goldSprite;
            case ItemType.Cash:     return ItemAssets.Instance.cashSprite;
        }
    }
}
