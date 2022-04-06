using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MsgUploadPoem : MsgBase
{
    public MsgUploadPoem() { protoName = "MsgUploadPoem"; }
    public string poemContent;
    //服务端回（0-成功，1-已修改数据库， 2-失败）
    public int result = 0;
}

class MsgUploadSpiderPoem : MsgBase
{
    public MsgUploadSpiderPoem() { protoName = "MsgUploadSpiderPoem"; }
    public string poemContent;
    //服务端回（0-成功，1-已修改数据库，2-失败）
    public int result = 0;
}