//注册
public class MsgRegister:MsgBase {
	public MsgRegister() {protoName = "MsgRegister";}
	//客户端发
	public string id = "";
	public string pw = "";
	//服务端回（0-成功，1-失败）
	public int result = 0;
}


//登陆
public class MsgLogin:MsgBase {
	public MsgLogin() {protoName = "MsgLogin";}
	//客户端发
	public string username = "";
	public string password = "";
	//服务端回（0-成功，1-失败）
	public int result = 0;
}


//踢下线（服务端推送）
public class MsgKick:MsgBase {
	public MsgKick() {protoName = "MsgKick";}
	//原因（0-其他人登陆同一账号）
	public int reason = 0;
}

public class MsgModifyUserName : MsgBase
{
    public MsgModifyUserName() { protoName = "MsgModifyUserName"; }
    //客户端发
    public string mail = "";
    public string name = "";
    //服务端回（0-成功，1-失败）
    public int result = 0;
}

public class MsgModifyPassword : MsgBase
{
    public MsgModifyPassword() { protoName = "MsgModifyPassword"; }
    //客户端发
    public string mail = "";
    public string password = "";
    //服务端回（0-成功，1-失败）
    public int result = 0;
}

class MsgGetUserDetail : MsgBase
{
    public MsgGetUserDetail() { protoName = "MsgGetUserDetail"; }

    public string userMail;

    public string userName;

    public string userPassword;

    public string behaviorContent;

    public string favoriteContent;

    public int result = 0;

}

class MsgGetUserList : MsgBase
{
    public MsgGetUserList() { protoName = "MsgGetUserList"; }
    public string content;
    //服务端回（0-成功，1-失败）
    public int result = 0;
}

class MsgDeleteUser : MsgBase
{
    public MsgDeleteUser() { protoName = "MsgDeleteUser"; }
    public string userMail;
    //服务端回（0-成功，1-失败）
    public int result = 0;
}