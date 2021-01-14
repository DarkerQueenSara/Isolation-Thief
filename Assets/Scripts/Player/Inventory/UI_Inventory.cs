using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
	public GameObject crosshair;
	public GameObject itemSlotPrefab;
	public GameObject gadgetSlotPrefab;
	public GameObject rightHandPrefab;
	private Inventory inventory;

	private Transform uiInventory;

	private Transform stolenItems;
	private Transform itemSlot;

	private Transform info;

	public TextMeshProUGUI stolenValueText; // "Stolen : " + stolenValue
	private TextMeshProUGUI missingValueText; // missingValue
	private TextMeshProUGUI goalValueText; // "Goal : " + goalValue
	private TextMeshProUGUI weightValueText; // "Weight (Max: " + maxWeight + "Kg): " + weight + "Kg"
	bool showInventory = false;

	private Transform gadgetsStuff;
	private Transform rightHand;
	private Transform container;

	private Transform itemPreview;
	private ItemWrapper previewedItem;

	private Player player;


	private void Awake()
	{
		uiInventory = transform.Find("Inventory");

		stolenItems = uiInventory.Find("Scroll View").Find("Viewport").Find("StolenItems");

		//TODO
		itemSlot = stolenItems.Find("itemSlot");

		info = uiInventory.Find("Info");
		stolenValueText = info.Find("Stolen").Find("stolenText").GetComponent<TextMeshProUGUI>();
		missingValueText = info.Find("MissingValue").Find("missingValueText").GetComponent<TextMeshProUGUI>();
		goalValueText = info.Find("Goal").Find("goalText").GetComponent<TextMeshProUGUI>();
		weightValueText = info.Find("Weight").Find("weightText").GetComponent<TextMeshProUGUI>();

		gadgetsStuff = uiInventory.Find("gadgets");
		rightHand = gadgetsStuff.Find("right_hand");
		container = gadgetsStuff.Find("container");

		itemPreview = uiInventory.Find("ItemPreview");
		itemPreview.transform.gameObject.SetActive(false);

		gameObject.SetActive(false);
	}

	private void Start()
	{
		this.goalValueText.text = "Goal : " + LevelManager.Instance.moneyGoal.ToString() + " $";
		this.player = Player.Instance;
		RefreshInventoryGadgets();
	}

	public void SetInventory(Inventory inventory)
	{
		this.inventory = inventory;
		RefreshInventoryItems();
	}

	public void Refresh()
	{
		if (player == null)
		{
			player = Player.Instance;
		}
		RefreshInventoryItems();
		RefreshInventoryGadgets();
	}


	public void RefreshInventoryItems()
	{

		foreach (Transform child in stolenItems)
		{
			Destroy(child.gameObject);
		}

		foreach (ItemWrapper itemW in inventory.GetItemList())
		{
			GameObject temp = Instantiate(itemSlotPrefab, stolenItems);
			temp.transform.Find("itemButton").Find("icon").GetComponent<Image>().sprite = itemW.item.GetSprite();

			Image selected = temp.transform.Find("selected").GetComponent<Image>();

			Button itemButton = temp.transform.Find("itemButton").GetComponent<Button>();
			itemButton.onClick.AddListener(delegate
			{
				if (selected.transform.gameObject.active)
				{
					//selected.transform.gameObject.SetActive(false);
					hidePreviewItem();
				}
				else
				{
					//selected.transform.gameObject.SetActive(true);
					previewItem(itemW);
				}
			});

			Button removeButton = temp.transform.Find("removeButton").GetComponent<Button>();

			removeButton.onClick.AddListener(delegate
			{
				if (previewedItem.id == itemW.id)
				{
					hidePreviewItem();
				}
				player.RemoveFromInventory(itemW.id);
			});
		}

		stolenValueText.text = "Total Value Stolen: " + inventory.getTotalValue();
		this.goalValueText.text = "Goal : " + LevelManager.Instance.moneyGoal.ToString() + " $";
		this.stolenValueText.text = "Stolen : " + inventory.getTotalValue() + " $";
		this.weightValueText.text = "Weight (Max: " + Inventory.MAX_WEIGHT + "Kg): " + inventory.getTotalWeight() + "Kg";

		float missingValue = LevelManager.Instance.moneyGoal - inventory.getTotalValue();
		missingValue = missingValue < 0 ? 0 : missingValue;
		this.missingValueText.text = missingValue + " $";
	}

	public void hidePreviewItem()
	{
		itemPreview.transform.gameObject.SetActive(false);
	}

	public void previewItem(ItemWrapper itemW)
	{
		previewedItem = itemW;

		TextMeshProUGUI name = itemPreview.Find("Name").GetComponent<TextMeshProUGUI>();
		name.text = itemW.item.name;

		TextMeshProUGUI value = itemPreview.Find("Value").GetComponent<TextMeshProUGUI>();
		value.text = itemW.item.value.ToString() + " $";

		TextMeshProUGUI weight = itemPreview.Find("Weight").GetComponent<TextMeshProUGUI>();
		weight.text = itemW.item.weight.ToString() + " Kg";

		itemPreview.Find("Item").Find("icon").GetComponent<Image>().sprite = itemW.item.GetSprite();

		itemPreview.transform.gameObject.SetActive(true);
	}

	public void RefreshInventoryGadgets()
	{
		#region destruction
		if (rightHand != null)
		{
			Destroy(rightHand.gameObject);
		}
		foreach (Transform child in container)
		{
			Destroy(child.gameObject);
		}
		#endregion

		if (player.rightHand != null)
		{
			GameObject temp = createGadgetSlot(rightHandPrefab, gadgetsStuff, player.rightHand);
			rightHand = temp.transform;
			Button button = temp.transform.Find("GadgetSlot").Find("GadgetButton").GetComponent<Button>();
			button.onClick.AddListener(delegate
			{
				player.rightHand = null;
				this.Refresh();
			});
		}


		foreach (Gadget gadget in player.inInventory)
		{
			if (player.hasGadgetOnHand(gadget.getID())) continue;
			GameObject temp = Instantiate(gadgetSlotPrefab, container);
			Button button = temp.transform.Find("GadgetButton").GetComponent<Button>();
			temp.transform.Find("GadgetButton").Find("name").GetComponent<TextMeshProUGUI>().text = gadget.getID();
			temp.transform.Find("GadgetButton").Find("icon").GetComponent<Image>().sprite = gadget.getSprite();
			button.onClick.AddListener(delegate
			{
				player.rightHand = gadget;
				this.Refresh();
			});
			//temp.transform.Find("itemButton").Find("icon").GetComponent<Image>().sprite = item.GetSprite();
		}
	}

	private GameObject createGadgetSlot(GameObject prefab, Transform place, Gadget gadget)
	{
		GameObject temp = Instantiate(prefab, place);
		temp.transform.Find("GadgetSlot").Find("GadgetButton").Find("name").GetComponent<TextMeshProUGUI>().text = gadget.getID();
		temp.transform.Find("GadgetSlot").Find("GadgetButton").Find("icon").GetComponent<Image>().sprite = gadget.getSprite();
		return temp;
	}

	public bool isVisible()
	{
		return showInventory;
	}
	public void visible()
	{
		showInventory = !showInventory;

		if (showInventory)
		{
			Time.timeScale = 0;
			gameObject.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			crosshair.SetActive(false);
		}
		else
		{
			Time.timeScale = 1;
			gameObject.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			crosshair.SetActive(true);
		}
	}
}