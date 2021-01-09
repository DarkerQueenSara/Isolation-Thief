using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
	private Camera cam;
	private InteractionTextManager interactionTextManager;

	public bool disabled = false;
	private void Awake()
	{
		cam = GetComponentInChildren<Camera>();
	}
	void Start()
	{
		interactionTextManager = InteractionTextManager.instance;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Pause"))
		{
			Debug.Log("show paused menu");
			PausedMenu.Instance.visible();
		}

		if (Input.GetButtonDown("Inventory"))
		{
			Player.Instance.ChangeInventoryVisible();
		}

		if (disabled) return;

		if (Input.GetButtonDown("Gadget2"))
		{
			//TODO gadgets que nao precisam de interactables
			Gadget onHand = Player.Instance.getGadgetUseAnywhereOnHand();
			if (onHand != null && onHand.CanUse())
				onHand.Use();
		}


		interact();
	}

	private Interactable lastInteractable;
	private void interact()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			//ONLY FOR DEBUG PURPOSES
			if (Input.GetButtonDown("Interact"))
			{
				//Debug.Log("I'm looking at " + hit.transform.name);
			}
			//----------------------------------------------------
			Interactable interactable = hit.collider.GetComponent<Interactable>();
			if (interactable != null && interactable.canInteract(hit.distance))
			{
				if (lastInteractable != interactable)
				{
					if (lastInteractable != null)
					{
						//Debug.Log("Interacting with something else!");
						lastInteractable.stopInteracting();
					}
					interactionTextManager.setInteractingText(interactable.getInteractingText());
				}

				if (Input.GetButtonDown("Interact"))
				{
					//interactable.interact();
					interactable.startInteracting();
					interactionTextManager.setInteractingText("");
				}
				if (Input.GetButtonUp("Interact"))
				{
					//Debug.Log("Interact button went up!");
					interactable.stopInteracting();
					interactionTextManager.setInteractingText(interactable.getInteractingText());
				}

				lastInteractable = interactable;
			}
			else
			{
				if (lastInteractable != null)
				{
					//Debug.Log("Looked away!");
					//Debug.Log(hit.distance);
					//Debug.Log(hit.collider);
					lastInteractable.stopInteracting();
				}

				if (interactionTextManager.HasText())
				{
					interactionTextManager.setInteractingText("");
				}
				lastInteractable = null;
			}
		}
	}

	public void setDisabled(bool disabled)
	{
		this.disabled = disabled;
        if (disabled)
        {
			interactionTextManager.setInteractingText("");
		}
	}
}
