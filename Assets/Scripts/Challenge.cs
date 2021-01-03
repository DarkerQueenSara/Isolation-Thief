using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge
{
    public int number;
    public string name;
    public string description;
    public bool fullfilled;
    public int xp;
    private Func<bool> verificationMethod;

    public Challenge(int number_, string name_, string description_, Func<bool> verificationMethod_, int xp_)
    {
        number = number_;
        xp = xp_;
        name = name_;
        description = description_;
        verificationMethod = verificationMethod_;
        fullfilled = false;
    }

    public void checkFullfiled()
    {
        if (!fullfilled && verificationMethod())
        {
            fullfilled = true;
        }
    }


}
