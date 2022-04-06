using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class PoemMgr
{
    public static string folderName = "ServicePoemFolder";
    
    public static bool GenPoemToJson(List<Poem> poems, string fileName)
    {
        if(poems.Count == 0)
        {
            Debug.Log("诗词数目为0，无法生成新的诗词文件");
            return false;
        }

        PoemData poemData = new PoemData();
        poemData.poems =  poems.ToArray();
        poemData.count = poems.Count;

        string poemsJsonStr = JsonUtility.ToJson(poemData, true);
        string dirSavePath = Application.persistentDataPath + "/UploadPoem/" + fileName;
        if (File.Exists(dirSavePath))
        {
            File.Delete(dirSavePath);
        }

        StreamWriter writer = new StreamWriter(dirSavePath, false);
        writer.WriteLine(poemsJsonStr);
        writer.Close();

        return true;
    }

    public static List<Poem> GetPoemFromJson(string fileName)
    {
        List<Poem> poems = new List<Poem>();
        string dirPoemJsonPath = Application.persistentDataPath + "/DownloadPoem/" + fileName;
        if(!File.Exists(dirPoemJsonPath))
        {
            Debug.Log("文件不存在");
        }
        else
        {
            StreamReader reader = new StreamReader(dirPoemJsonPath);
            string poemJsonStr = reader.ReadToEnd();
        }
        return poems;
    }

    public static string PoemSpiderToJson(PoemSpider poemSpider)
    {
        Poem poem = new Poem(poemSpider.title, poemSpider.author, poemSpider.dynasty, 
            poemSpider.content, poemSpider.type, poemSpider.book, poemSpider.annotation,
            poemSpider.translation, poemSpider.appreciation);
        string jsonStr = JsonUtility.ToJson(poem, true);
        return jsonStr;
    }

    public static string PoemToJson(Poem poem)
    {
        string jsonStr = JsonUtility.ToJson(poem, true);
        return jsonStr;
    }
}

[Serializable]
class PoemData
{
    public Poem[] poems;
    public int count;
}

