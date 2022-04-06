using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgAdminLogin : MsgBase
{
    public MsgAdminLogin() { protoName = "MsgAdminLogin"; }
    //客户端发
    public string mail = "";
    public string name = "";
    public string password = "";
    //服务端回（0-成功，1-失败）
    public int result = 0;
}

public class MsgAdminRegister : MsgBase
{
    public MsgAdminRegister() { protoName = "MsgAdminRegister"; }
    //客户端发
    public string name = "";
    public string password = "";
    public string mail = "";
    //服务端回（0-成功，1-失败）
    public int result = 0;
}

public class MsgAdminModifyPassword : MsgBase
{
    public MsgAdminModifyPassword() { protoName = "MsgAdminModifyPassword"; }
    public string mail = "";
    public string password = "";
    public string name = "";
    //服务端回（0-成功，1-失败, 2-邮箱已经存在，因此注册失败）
    public int result = 0;
}


class MsgGetAdminDetail : MsgBase
{
    public MsgGetAdminDetail() { protoName = "MsgGetAdminDetail"; }

    public string userMail;

    public string userName;

    public string behaviorContent;

    public int result = 0;

}

class MsgGetAdminList : MsgBase
{
    public MsgGetAdminList() { protoName = "MsgGetAdminList"; }
    public string content;
    //服务端回（0-成功，1-失败）
    public int result = 0;
}