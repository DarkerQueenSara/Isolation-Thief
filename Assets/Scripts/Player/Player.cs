using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private UI_Inventory ui_Inventory;

    private Inventory inventory;

    private void Awake()
    {
        inventory = new SimpleBag();
        ui_Inventory.SetInventory(inventory);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
