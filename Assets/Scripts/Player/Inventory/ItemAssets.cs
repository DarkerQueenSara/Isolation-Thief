using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public Sprite tvSprite;
    public Sprite laptopSprite;
    public Sprite phoneSprite;
    public Sprite tabletSprite;
    public Sprite goldSprite;
    public Sprite cashSprite;
}
