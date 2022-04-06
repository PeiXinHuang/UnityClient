using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgPoem : MsgBase
{
    public MsgPoem() { protoName = "MsgPoem"; }
    public string poemName = "";
    //服务端回（0-成功，1-失败）
    public int result = 0;
} 

