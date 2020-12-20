using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    protected Player player;

    public void Start()
    {
        player = Player.Instance;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    /*
     * Function for single use interaction 
     * 
     */
    public virtual void interact()
    {
        //this method is meant to be overwritten
        #region dummy_interact
        Debug.Log("Interacting with " + transform.name);
        if(gameObject.GetComponent<Renderer>().material.color == Color.red)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (gameObject.GetComponent<Renderer>().material.color == Color.blue)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        } else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        #endregion
    }

    /*
     * Function called every update while interacting
     * 
     */
    public virtual void interacting()
    {
        //this method is ment to be overriten
    }


    /*
     * Function to declare the ending of an interaction, 
     * if interaction is not continuous you can ignore this function,
     * if interaction is continuous use this function to reset values
     */
    public virtual void interactionStopped()
    {
        //this method is ment to be overriten
    }

    public virtual string getInteractingText()
    {
        //Show a label with text if we can interact
        return "Press 'E' to interact with " + transform.name;
    }

    public bool canInteract(float dist)
    {
        return dist < radius;
    }

    public virtual void initialize()
    {
        //function to be overriten if you want to initialize stuff
    }

    private bool isInteracting;
    public void startInteracting()
    {
        if (isInteracting)
            return;
        isInteracting = true;
        this.interact();
        //this.startInteraction();
    }

    public void Update()
    {
        if (isInteracting)
        {
            this.interacting();
        }
    }

    public void stopInteracting()
    {
        if (!isInteracting)
            return;
        isInteracting = false;
        this.interactionStopped();
    }
}
