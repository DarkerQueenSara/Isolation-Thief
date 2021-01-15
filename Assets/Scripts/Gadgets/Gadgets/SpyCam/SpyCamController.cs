using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyCamController : MonoBehaviour
{
    public static SpyCamController Instance;
    public Camera playerCam;
    public GameObject spyCamPrefab;
    private GameObject spyCamObject;
    private Camera spyCam;
    public GameObject crosshair;
    private AudioManager playerAudioManager;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than one instance of SpyCamController");
        }
        Instance = this;
        playerAudioManager = GameObject.FindGameObjectWithTag("Player")?.GetComponent<AudioManager>();
    }

    public void CreateSpyCam()
    {
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 camPos = hit.point;

            var forward = playerCam.transform.forward;
            forward.y = 0f;
            
            camPos += forward* (-0.025f);
            spyCamObject = Instantiate(this.spyCamPrefab, camPos, Quaternion.LookRotation(hit.normal));
            spyCam = spyCamObject.transform.Find("Camera").GetComponent<Camera>();
            playerAudioManager.Play("Blip");
        }
    }

    public void ShowSpyCam()
    {
        spyCam.enabled = true;
        playerCam.enabled = false;
        blockPlayerMovement();
    }

    public void StopShowingSpyCam()
    {
        playerCam.enabled = true;
        spyCam.enabled = false;
        enablePlayerMovement();
    }

    public void RetrieveSpyCam()
    {
        playerCam.enabled = true;
        spyCam.enabled = false;
        Destroy(spyCamObject);
        enablePlayerMovement();
    }

    private void blockPlayerMovement()
    {
        PlayerMovement.Instance.disabled = true;
        InteractionTextManager.instance.setInteractingText("");
        crosshair?.SetActive(false);
    }

    private void enablePlayerMovement()
    {
        PlayerMovement.Instance.disabled = false;
        crosshair?.SetActive(true);
    }
}
