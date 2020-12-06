using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private UI_Inventory ui_Inventory;

    private Inventory inventory;

    private PlayerMovement playerMovement;


    public int level { get; private set; }

    private GadgetTree gadgetTree;

    private void Awake()
    {

        Instance = this;
        inventory = new SimpleBag();
        ui_Inventory.SetInventory(inventory);
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        level = 1;
        gadgetTree = new GadgetTree();
    }

    public void AddToInventory(Item item)
    {
        this.inventory.AddItem(item);
        ui_Inventory.RefreshInventoryItems();
    }

    public void DisableMovement()
    {
        playerMovement.disabled = true;
    }

    public void ChangeInventoryVisible()
    {
        this.ui_Inventory.visible();
    }

    public float GetTotalStolen()
    {
        return this.inventory.TotalValue;
    }

    public bool CanLockpick()
    {
        Lockpick lockpick = gadgetTree.gadgets.ContainsKey("lockpick") ? (Lockpick)gadgetTree.gadgets["lockpick"] : null;

        if(lockpick != null)
        {
            return lockpick.CanUse();
        }

        return false;
    }

    public float LockpickingTime()
    {
        Lockpick lockpick = gadgetTree.gadgets.ContainsKey("lockpick") ? (Lockpick)gadgetTree.gadgets["lockpick"] : null;

        if (lockpick != null)
        {
            return lockpick.GetLockPickingTime();
        }
        return float.MaxValue;
    }

    public void unlockGadget(string gadgetName)
    {
        Gadget gadget = this.gadgetTree.gadgets[gadgetName];

        if(gadget != null)
        {
            gadget.unlocked = true;
        }
    }

    void Start()
    {
        //TODO remove this (or not!)
        this.unlockGadget("lockpick");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
