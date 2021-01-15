using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : Interactable
{
    public GameObject flameAnim;
    public GameObject toDestroy;
    protected bool isBurning;
    protected string objectName;
    private AudioManager audioManager;
    public float distractionTime = 10.0f;

    public void Awake()
    {
        objectName = gameObject.name;
        isBurning = false;
        audioManager = this.gameObject.GetComponent<AudioManager>();
    }

    new public void Start()
    {
        base.Start();
        flameAnim.SetActive(false);
    }

    public override void interact()
    {
        //this method is ment to be blank
        if (!isBurning)
        {
            Lighter lighter = (Lighter)player.getGadgetOnHand(GadgetType.LIGHTER);
            if (lighter != null && lighter.CanUse())
            {
                isBurning = true;
                lighter.Use();
                flameAnim.SetActive(true);
                LevelManager.Instance.objectsBurned++;
                audioManager.Play("Burn");
                Invoke("StopBurning", distractionTime);
                //TODO enviar som ao NPC e mter maxDistance que nao pus
                NPCManager.Instance?.InvestigateSound(gameObject.transform.position, 5000,7f);
            }
        }
    }

    public void StopBurning()
    {
        isBurning = false;
        flameAnim.SetActive(false);
        toDestroy.SetActive(false);
        audioManager.Stop("Burn");
        //TODO parar de mandar som ao NPC
    }

    public override string getInteractingText()
    {
        if (!isBurning)
        {
            Lighter lighter = (Lighter)player.getGadgetOnHand(GadgetType.LIGHTER);
            if (lighter != null && lighter.CanUse())
            {
                return "Light " + objectName + " on fire";
            }
        }
        return "";
    }
}
