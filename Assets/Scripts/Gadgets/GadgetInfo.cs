using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gadget", menuName = "Inventory/Gadget")]
public class GadgetInfo : ScriptableObject
{
    new public string name = "New Gadget";
    public Sprite icon = null;
    public float cost;

    public Sprite GetSprite()
    {
        return icon;
    }
}
