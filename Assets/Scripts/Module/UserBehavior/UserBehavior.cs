using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserBehavior 
{
    public string mail;
    public string behavior;
    public string date;
    public UserBehavior(string _mail, string _behavior, string _date)
    {
        mail = _mail;
        behavior = _behavior;
        date = _date;
    }
}
