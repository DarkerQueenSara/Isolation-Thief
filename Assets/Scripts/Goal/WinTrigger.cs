using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("I hit the trigger");
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.endGame();
        }
    }
}
