using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private UI_Inventory ui_Inventory;

    private Inventory inventory;

    private void Awake()
    {
        Instance = this;
        inventory = new SimpleBag();
        ui_Inventory.SetInventory(inventory);
    }

    public void addToInventory(Item item)
    {
        this.inventory.AddItem(item);
        ui_Inventory.RefreshInventoryItems();
    }

    public void changeInventoryVisible()
    {
        this.ui_Inventory.visible();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
