using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBedroom : MonoBehaviour
{
    //The list of colliders currently inside the trigger
    List<Collider> TriggerList = new List<Collider>();
    private int lastCount;

    void Start()
    {
        lastCount = TriggerList.Count;
    }

    private void Update()
    {
        if (lastCount != TriggerList.Count)
        {
            CheckBool();
        }
        lastCount = TriggerList.Count;
    }

    void CheckBool()
    {
        if (!LevelManager.Instance.enteredEmptyBedroom)
        {
            bool player = false;
            bool npc = false;
            foreach (Collider col in TriggerList)
            {
                if (col.gameObject.CompareTag("Player"))
                {
                    player = true;
                }
                if (col.gameObject.CompareTag("NPC"))
                {
                    npc = true;
                }
            }
            if (player && !npc)
            {
                LevelManager.Instance.enteredEmptyBedroom = true;
            }
        }
    }

    //called when something enters the trigger
    void OnTriggerEnter(Collider other)
    {
        //if the object is not already in the list
        if (!TriggerList.Contains(other))
        {
            //add the object to the list
            TriggerList.Add(other);
        }
    }

    //called when something exits the trigger
    void OnTriggerExit(Collider other)
    {
        //if the object is in the list
        if (TriggerList.Contains(other))
        {
            //remove it from the list
            TriggerList.Remove(other);
        }
    }
}
