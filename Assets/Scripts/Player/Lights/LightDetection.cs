using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    private Light thisLight;
    private int mask;
    public bool status { get { return thisLight.intensity == 0 ? false : true; } }

    private void Start()
    {
        this.thisLight = gameObject.GetComponent<Light>();
        mask = LayerMask.GetMask("Lights");
        AILightManager.Instance.lightsDetection.Add(this);
    }

    public bool LitsPlayer(Player player)
    {
        Vector3 playerPos = player.transform.position + new Vector3(0f, 1.5f, 0f); ;
        Vector3 lightPos = transform.position;

        if ((playerPos - lightPos).magnitude <= thisLight.range)
        {
            bool gotHit = Physics.Linecast(lightPos, playerPos, out RaycastHit hit, ~(1 << 14));
            Debug.DrawRay(lightPos, playerPos - lightPos, Color.green, 4f, true);
            if (gotHit && hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player is lit by light with position: " + lightPos);
                return true;
            }

        }
        return false;
    }
}
