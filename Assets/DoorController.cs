using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();   
    }

    void onTriggerEnter(Collider collider)
	{
        Debug.Log("Door on trigger enter");
        if (Input.GetKeyDown(KeyCode.E))
		{
            animator.SetTrigger("OpenCloseDoor");
            Debug.Log("E pressed");
		}
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
		{
            animator.SetTrigger("OpenCloseDoor");
            Debug.Log("E pressed");
		}
    }
}
