using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string mail;
    public string password;
    public string name;

    public User(string _mail,string _password,string _name)
    {
        mail = _mail;
        password = _password;
        name = _name;
    }
}
