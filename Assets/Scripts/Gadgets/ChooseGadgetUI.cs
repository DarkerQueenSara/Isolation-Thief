using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseGadgetUI : MonoBehaviour
{
	public static ChooseGadgetUI Instance { get; private set; }

	public const int MAX_GADGETS_CHOICE = 3;

	public GameObject gadgetSlotPrefab;

	private Transform gadgetsToChoose;

	List<Gadget> unlockedGadgets;

	List<Gadget> chosenGadgets;

	bool isShown = false;

	private void Awake()
	{
		Instance = this;
		gadgetsToChoose = transform.Find("Scroll View").Find("Viewport").Find("GadgetsToChoose");
		this.chosenGadgets = new List<Gadget>();
		transform.Find("DoneBtn").GetComponent<Button>().onClick.AddListener(delegate
		{
			this.doneChoosing();
		});
		gameObject.SetActive(false);
	}

	void Start()
	{
		GadgetTree gadgetTree = Player.Instance.GetGadgetTree();
		this.unlockedGadgets = gadgetTree.getUnlockedGadgets();
		this.refreshSlots();
	}

	public void refreshSlots()
	{
		this.unlockedGadgets = GameManager.Instance.gadgetTree.getUnlockedGadgets();

		this.chosenGadgets.Clear();
		foreach (Transform child in gadgetsToChoose)
		{
			Destroy(child.gameObject);
		}

		foreach (Gadget gadget in this.unlockedGadgets)
		{
			GameObject temp = Instantiate(gadgetSlotPrefab, gadgetsToChoose);
			Button button = temp.transform.Find("GadgetButton").GetComponent<Button>();
			temp.transform.Find("GadgetButton").Find("name").GetComponent<TextMeshProUGUI>().text = gadget.getID();
			temp.transform.Find("GadgetButton").Find("icon").GetComponent<Image>().sprite = gadget.getSprite();
			Image selected = temp.transform.Find("GadgetButton").Find("selected").GetComponent<Image>();
			button.onClick.AddListener(delegate
			{
				//gadget.unlocked = !gadget.unlocked;
				if (this.chosenGadgets.Contains(gadget))
				{
					Debug.Log(gadget.getID() + " was removed");
					selected.transform.gameObject.SetActive(false);
					this.chosenGadgets.Remove(gadget);
				}
				else
				{
					if(chosenGadgets.Count >= MAX_GADGETS_CHOICE)
                    {
						//TODO Avisar ao player que clicou demasiadas vezes
						Debug.Log("Already has " + MAX_GADGETS_CHOICE + " gadgets!");
					}
                    else
                    {
						Debug.Log(gadget.getID() + " was chosen");
						selected.transform.gameObject.SetActive(true);
						this.chosenGadgets.Add(gadget);
					}
				}
			});
			//temp.transform.Find("itemButton").Find("icon").GetComponent<Image>().sprite = item.GetSprite();
		}
	}

	public void doneChoosing()
	{
		Player.Instance.setInventoryGadgets(this.chosenGadgets);
		changeVisibility();
		LevelManager.Instance.StartGame();
	}

	public void changeVisibility()
	{
		isShown = !isShown;

		if (isShown)
		{
			refreshSlots();
			gameObject.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			//crosshair.SetActive(false);
		}
		else
		{
			gameObject.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			//crosshair.SetActive(true);
		}
	}
}
