﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private Camera cam;
    private InteractionTextManager interactionTextManager;
    private GadgetTreeUI gadgetTreeUI;
    private ChooseGadgetUI chooseGadgetUI;


    public bool disabled = false;
    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }
    void Start()
    {
        gadgetTreeUI = GadgetTreeUI.Instance;
        chooseGadgetUI = ChooseGadgetUI.Instance;
        interactionTextManager = InteractionTextManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            Player.Instance.ChangeInventoryVisible();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            gadgetTreeUI.changeVisibility();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            chooseGadgetUI.changeVisibility();
        }

        if (disabled) return;

        if (Input.GetButtonDown("Gadget2"))
        {
            //TODO gadgets que nao precisam de interactables
            Gadget onHand = Player.Instance.getGadgetTypeFOnHand();
            if(onHand.CanUse())
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
                    if(lastInteractable != null)
                        lastInteractable.stopInteracting();
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
                    interactable.stopInteracting();
                    interactionTextManager.setInteractingText(interactable.getInteractingText());
                }

                lastInteractable = interactable;
            }
            else
            {
                if (lastInteractable != null)
                {
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
    }
}
