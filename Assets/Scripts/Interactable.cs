using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public virtual void interact()
    {
        //this method is meant to be overwritten
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


    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
