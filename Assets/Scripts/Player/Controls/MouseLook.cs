using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 250f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Hide and lock cursor in screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Cursor.lockState == CursorLockMode.None)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //rotation is flipped
        xRotation -= mouseY;
        //Limit rotation so you cant rotate more than -90 degrees to 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate camera around X axis
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //rotate player body around Y axis
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
