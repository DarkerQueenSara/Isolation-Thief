using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class SkillInfo : ScriptableObject
{
    new public string name = "New Skill";
    public Sprite icon = null;
    public int cost;

    public Sprite GetSprite()
    {
        return icon;
    }
}
