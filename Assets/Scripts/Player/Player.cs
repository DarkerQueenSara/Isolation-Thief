using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private UI_Inventory ui_Inventory;

    private Inventory inventory;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        Instance = this;
        inventory = new SimpleBag();
        ui_Inventory.SetInventory(inventory);
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    public void addToInventory(Item item)
    {
        this.inventory.AddItem(item);
        ui_Inventory.RefreshInventoryItems();
    }

    public void DisableMovement()
    {
        playerMovement.disabled = true;
    }

    public void changeInventoryVisible()
    {
        this.ui_Inventory.visible();
    }

    public float getTotalStolen()
    {
        return this.inventory.TotalValue;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
