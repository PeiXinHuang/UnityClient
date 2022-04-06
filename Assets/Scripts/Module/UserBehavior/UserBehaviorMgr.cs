using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserBehaviorMgr 
{
    public static List<UserBehavior> JsonToUserBehavior(string jsonStr)
    {
     
        List<UserBehavior> behaviors = new List<UserBehavior>();
        
        UserBehaviorData behaviorData = JsonUtility.FromJson<UserBehaviorData>(jsonStr);
        if(behaviorData == null || behaviorData.behaviors == null)
        {
            return behaviors;
        }
        foreach (UserBehavior behavior in behaviorData.behaviors)
        {
            behaviors.Add(behavior);
        }
        return behaviors;
    }
}

[Serializable]
class UserBehaviorData
{
    public UserBehavior[] behaviors;
    public int count;
}