using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



class MsgDownloadAdminUserList : MsgBase
{
    public MsgDownloadAdminUserList() { protoName = "MsgDownloadAdminUserList"; }
    public string content;
    //服务端回（0-成功，1-失败）
    public int result = 0;
}

class MsgDownloadUserBehaivor : MsgBase
{
    public MsgDownloadUserBehaivor() { protoName = "MsgDownloadUserBehaivor"; }
    public string userMail;
    public string content;
    //服务端回（0-成功，1-失败）
    public int result = 0;
}



//class MsgDownloadPoemAuthor : MsgBase
//{
//    public MsgDownloadPoemAuthor() { protoName = "MsgDownloadPoemAuthor"; }
//    public string author;
//    public string md5;
//    public int totalCount = 0;
//    //服务端回（0-成功，1-失败）
//    public int result = 0;
//}

//class MsgDownloadPoemDynasty : MsgBase
//{
//    public MsgDownloadPoemDynasty() { protoName = "MsgDownloadPoemDynasty"; }
//    public string dynasty;
//    public string content;
//    public int totalCount = 0;
//    //服务端回（0-成功，1-失败）
//    public int result = 0;
//}

//class MsgDownloadPoemType : MsgBase
//{
//    public MsgDownloadPoemType() { protoName = "MsgDownloadPoemType"; }
//    public string type;
//    public string content;
//    public int totalCount = 0;
//    //服务端回（0-成功，1-失败）
//    public int result = 0;
//}

//class MsgDownloadPoemBook : MsgBase
//{
//    public MsgDownloadPoemBook() { protoName = "MsgDownloadPoemBook"; }
//    public string book;
//    public string content;
//    public int totalCount = 0;
//    //服务端回（0-成功，1-失败）
//    public int result = 0;
//}

//class MsgDownloadPoemAuthorList : MsgBase
//{
//    public MsgDownloadPoemAuthorList() { protoName = "MsgDownloadPoemAuthorList"; }
//    public string content;
//    public int totalCount = 0;
//    //服务端回（0-成功，1-失败）
//    public int result = 0;
//}

//class MsgDownloadPoemDynastyList : MsgBase
//{
//    public MsgDownloadPoemDynastyList() { protoName = "MsgDownloadPoemDynastyList"; }
//    public string content;
//    public int totalCount = 0;
//    //服务端回（0-成功，1-失败）
//    public int result = 0;
//}

//class MsgDownloadPoemBookList : MsgBase
//{
//    public MsgDownloadPoemBookList() { protoName = "MsgDownloadPoemBookList"; }
//    public string content;
//    //服务端回（0-成功，1-失败）
//    public int result = 0;
//}

//class MsgDownloadPoemTypeList : MsgBase
//{
//    public MsgDownloadPoemTypeList() { protoName = "MsgDownloadPoemTypeList"; }
//    public string content;
//    //服务端回（0-成功，1-失败）
//    public int result = 0;
//}

class MsgDownloadAudioSource : MsgBase
{
    public MsgDownloadAudioSource() { protoName = "MsgDownloadAudioSource"; }
    public string poemId;
    public string content;
    //服务端回（0-成功，1-失败）
    public int result = 0;
}

class MsgDownloadPkg: MsgBase
{
    public MsgDownloadPkg() { protoName = "MsgDownloadPkg"; }
    public string md5; //用于校验文件下载是否成功
    public int version;
    //服务端回（0-成功，1-失败）
    public int result = 0;
}

