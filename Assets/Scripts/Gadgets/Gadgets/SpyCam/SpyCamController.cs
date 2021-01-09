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

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than one instance of SpyCamController");
        }
        Instance = this;
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
        }
    }

    public void ShowSpyCam()
    {
        spyCam.enabled = true;
        playerCam.enabled = false;
    }

    public void StopShowingSpyCam()
    {
        playerCam.enabled = true;
        spyCam.enabled = false;
    }

    public void RetrieveSpyCam()
    {
        playerCam.enabled = true;
        spyCam.enabled = false;
        Destroy(spyCamObject);
    }
}
