using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILightManager : MonoBehaviour
{

    public static AILightManager Instance { get; private set;}

    public List<LightDetection> lightsDetection;

    private AILightManager(){}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.LogError("More than 1 instance of AILightManager!");
        }
        lightsDetection = new List<LightDetection>();
    }


    Player player;

    void Start()
    {
        player = Player.Instance;
        //playerPos = player.GetComponentInChildren<Camera>().transform.position;
       
        InvokeRepeating(nameof(CheckLightsOnPlayer), 5.0f, 0.5f);
    }

    private void CheckLightsOnPlayer()
    {
        bool lit = false;

        foreach(LightDetection lightD in lightsDetection)
        {
            if (lightD.status)
            {
                if (lightD.LitsPlayer(player))
                {
                    lit = true;
                }
            }
        }
        player.isLit = lit;
    }
}
