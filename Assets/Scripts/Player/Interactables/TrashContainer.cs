using UnityEngine;
using System.Collections;


public class TrashContainer : Interactable
{
    public const string TRASH_ITEM_NAME = "trash";
    public GameObject appearingBag;
    private bool hasPlaced;

    private void Awake()
    {
        appearingBag.SetActive(false);
        hasPlaced = false;
    }

    public override void interact()
    {
        if (player.inventory.hasItem(TRASH_ITEM_NAME) && !hasPlaced)
        {
            player.inventory.popItemDisappearingGameObject(TRASH_ITEM_NAME);
            appearingBag.SetActive(true);
            hasPlaced = true;
            LevelManager.Instance.trashInCan = true;
        }
    }

    public override string getInteractingText()
    {
        if (player.inventory.hasItem(TRASH_ITEM_NAME))
        {
            if (hasPlaced)
            {
                return "Trash is full";
            }
            else
            {
                return "Leave trash";
            }
        }
        return "";
    }
}