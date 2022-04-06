using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AdminUser
{
    public string name;
    public string password;
    public string mail;

    public AdminUser(string _name, string _password, string _mail)
    {
        this.name = _name;
        this.password = _password;
        this.mail = _mail;
    }
}
