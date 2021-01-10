using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance { get; private set; }

    //Game
    GameManager gameManager;
    private GadgetTree gadgetTree;
    public int level
    {
        get { return gameManager.level; }
        set { gameManager.level = value; }
    }

    //Level
    [SerializeField] private UI_Inventory ui_Inventory;
    public Inventory inventory { get; private set; }
    private PlayerMovement playerMovement;
    private Controls playerControls;
     public bool isLit = false;
    public List<Gadget> inInventory { get; private set;}
    public Gadget rightHand { get; set; }

    private void Awake()
    {

        Instance = this;
        inventory = new SimpleBag();
        ui_Inventory.SetInventory(inventory);
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerControls = gameObject.GetComponent<Controls>();
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        gadgetTree = gameManager.gadgetTree;
        GameManager.Instance.skillsTree.activateAllSkills();

        inInventory = new List<Gadget>();
        //inInventory.Add(gadgetTree.gadgets[SimpleLockpick.gadgetID]);
        //inInventory.Add(gadgetTree.gadgets[FastLockpick.gadgetID]);

        //rightHand = gadgetTree.gadgets[SimpleLockpick.gadgetID];
    }

    public void changeMoney(int value)
    {
        gameManager.money += value;
    }

    public float getMoney()
    {
        return gameManager.money;
    }

    public void AddToInventory(Item item)
    {
        this.inventory.AddItem(item);
        ui_Inventory.Refresh();
    }

	public void DisableMovement()
	{
		playerMovement.disabled = true;
	}

	public void ChangeInventoryVisible()
	{
		this.ui_Inventory.visible();
		this.playerControls.setDisabled(this.ui_Inventory.isVisible());
	}

	public int GetTotalStolen()
	{
		return this.inventory.TotalValue;
	}


    public GadgetTree GetGadgetTree()
    {
        return this.gadgetTree;
    }

	public void unlockGadget(string gadgetName)
	{
		Gadget gadget = this.gadgetTree.gadgets[gadgetName];

		if (gadget != null)
		{
			gadget.unlocked = true;
		}
	}

    public void setInventoryGadgets(List<Gadget> chosenGadgets)
    {
        this.inInventory = chosenGadgets;
        this.rightHand = null;
        this.ui_Inventory.Refresh();
    }
    public bool hasGadgetOnHand(String name)
    {
        return rightHand != null && rightHand.getID() == name;
    }

    public bool hasGadgetOnHand(GadgetType type)
    {
        return rightHand != null && rightHand.getGadgetType() == type;
    }

    public Gadget getGadgetOnHand(GadgetType type)
    {
        if(rightHand != null && rightHand.getGadgetType() == type)
        {
            return rightHand;
        }
        return null;
    }

    public Gadget getGadgetUseAnywhereOnHand()
    {
        if (rightHand != null && rightHand.CanUseAnywhere())
        {
            return rightHand;
        }
        return null;
    }

    public void RefreshUIInventory()
    {
        ui_Inventory.Refresh();
    }



	// Update is called once per frame
	void Update()
	{

	}
}
