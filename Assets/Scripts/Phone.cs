using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    //poupar uns get components
    public NPCMovement NPC;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC") && NPC.callingCops)
        {
            Debug.Log("Trocou as booleans");
            GameManager.Instance.copsCalled = true;
            NPC.callingCops = false;
        }
    }
}
