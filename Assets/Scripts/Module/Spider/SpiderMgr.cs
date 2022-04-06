using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public static class SpiderMgr 
{
    public static string spiderFolder;
    public static string exeFolder;
    public static void Init()
    {
        spiderFolder = Application.persistentDataPath + "/SpiderFolder/";
        exeFolder = System.Environment.CurrentDirectory + "/Exe/";
    }




    #region 获取诗词分类
    private static event UnityAction<List<string>> getAllAuthorEvent;
    private static event UnityAction<List<string>> getAllDynastyEvent;
    private static event UnityAction<List<string>> getAllTypeEvent;
    private static event UnityAction<List<string>> getAllBookEvent;
    public static void AddEventListener(string eventName, UnityAction<List<string>> function)
    {
        if (eventName == "getAllAuthorEvent")
            getAllAuthorEvent += function;
        else if (eventName == "getAllDynastyEvent")
            getAllDynastyEvent += function;
        else if (eventName == "getAllTypeEvent")
            getAllTypeEvent += function;
        else if (eventName == "getAllBookEvent")
            getAllBookEvent += function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }
    public static void RemoveEventListener(string eventName, UnityAction<List<string>> function)
    {
        if (eventName == "getAllAuthorEvent")
            getAllAuthorEvent -= function;
        else if (eventName == "getAllDynastyEvent")
            getAllDynastyEvent -= function;
        else if (eventName == "getAllTypeEvent")
            getAllTypeEvent -= function;
        else if (eventName == "getAllBookEvent")
            getAllBookEvent -= function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }

    public static List<string> authors = null;
    public static bool hasChangeAuthors = false;
    /// <summary>
    /// 爬虫爬取作者文件，文件已经存在就不爬取
    /// </summary>
    public static void GetAllAuthor()
    {
        try
        {   
            if(!File.Exists(spiderFolder + "author.json"))
            {
                if (!Directory.Exists(spiderFolder))
                {
                    Directory.CreateDirectory(spiderFolder);
                }
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(exeFolder + "GetAllAuthor.exe", spiderFolder + "author.json");
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                myprocess.StartInfo = startInfo;
                myprocess.Start();
                myprocess.WaitForExit();
            }    
            
            authors = new List<string>();
            if (!File.Exists(spiderFolder + "author.json"))
            {
                UnityEngine.Debug.Log("文件不存在,爬虫爬取文件失败");
            }
            else
            {
                StreamReader reader = new StreamReader(spiderFolder + "author.json");
                string jsonStr = reader.ReadToEnd();
               
                AllAuthors allAuthors = JsonUtility.FromJson<AllAuthors>(jsonStr);
                foreach (string author in allAuthors.authorList)
                {
                    authors.Add(author);
                }
                hasChangeAuthors = true;
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("爬虫爬取文件失败，出错原因：" + ex.Message);
        }
    }

    public static List<string> dynastys = new List<string>();
    public static bool hasChangeDynastys = false;
    /// <summary>
    /// 爬虫爬取文件，文件已经存在就不爬取
    /// </summary>
    public static void GetAllDynasty()
    {
        try
        {
            if (!File.Exists(spiderFolder + "dynasty.json"))
            {
                if (!Directory.Exists(spiderFolder))
                {
                    Directory.CreateDirectory(spiderFolder);
                }
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(exeFolder + "GetAllDynasty.exe", spiderFolder + "dynasty.json");
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                myprocess.StartInfo = startInfo;
                myprocess.Start();
                myprocess.WaitForExit();
            }

            dynastys = new List<string>();
            if (!File.Exists(spiderFolder + "dynasty.json"))
            {
                UnityEngine.Debug.Log("文件不存在,爬虫爬取文件失败");
            }
            else
            {
                StreamReader reader = new StreamReader(spiderFolder + "dynasty.json");
                string jsonStr = reader.ReadToEnd();
                AllDynastys allDynastys = JsonUtility.FromJson<AllDynastys>(jsonStr);
                foreach (string dynasty in allDynastys.dynastyList)
                {
                    dynastys.Add(dynasty);
                }
                hasChangeDynastys = true;
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("爬虫爬取文件失败，出错原因：" + ex.Message);
        }
    }

    public static List<string> types = null;
    public static bool hasChangeTypes = false;
    public static List<string> bookCanSetNames = new List<string>() {
        "小学古诗",
        "初中古诗",
        "高中古诗",
        "小学文言文",
        "初中文言文",
        "高中文言文",
        "唐诗三百首",
        "古诗三百首",
        "宋词三百首",
        "古诗十九首"
    };
    public static List<string> books = null;
    public static bool hasChangeBooks = false;
    /// <summary>
    /// 爬虫爬取文件，文件已经存在就不爬取
    /// </summary>
    public static void GetAllType()
    {
        try
        {
            if (!File.Exists(spiderFolder + "type.json"))
            {
                if (!Directory.Exists(spiderFolder))
                {
                    Directory.CreateDirectory(spiderFolder);
                }
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(exeFolder + "GetAllType.exe", spiderFolder + "type.json");
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                myprocess.StartInfo = startInfo;
                myprocess.Start();
                myprocess.WaitForExit();
            }

            types = new List<string>();
            books = new List<string>();
            
            if (!File.Exists(spiderFolder + "type.json"))
            {
                UnityEngine.Debug.Log("文件不存在,爬虫爬取文件失败");
            }
            else
            {
                StreamReader reader = new StreamReader(spiderFolder + "type.json");
                string jsonStr = reader.ReadToEnd();

                AllTypes allTypes = JsonUtility.FromJson<AllTypes>(jsonStr);
                foreach (string type in allTypes.typeList)
                {
                    if (bookCanSetNames.Contains(type))
                    {
                        books.Add(type);
                    }
                    else
                    {
                        types.Add(type);
                    }
                }
                hasChangeTypes = true;
                hasChangeBooks = true;
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("爬虫爬取文件失败，出错原因：" + ex.Message);
        }
    }


    #endregion

    #region 获取诗词
    private static event UnityAction<List<PoemSpider>> getPoemEvent;
    public static void AddEventListener(string eventName, UnityAction<List<PoemSpider>> function)
    {
        if (eventName == "getPoemEvent")
            getPoemEvent += function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }
    public static void RemoveEventListener(string eventName, UnityAction<List<PoemSpider>> function)
    {
        if (eventName == "getPoemEvent")
            getPoemEvent -= function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }
    private static bool hasChangePoem = false;
    private static List<PoemSpider> poems = null;
    public static void GetPoemByAuthor(object authorObj)
    {
        string author = (string)authorObj;
        try
        {
            if (!File.Exists(spiderFolder + author+".json"))
            {
                if (!Directory.Exists(spiderFolder))
                {
                    Directory.CreateDirectory(spiderFolder);
                }
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(exeFolder + "GetPoemByAuthor.exe", author + " " + spiderFolder + author + ".json");
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                myprocess.StartInfo = startInfo;
                myprocess.Start();
                myprocess.WaitForExit();
            }

            poems = new List<PoemSpider>();
            if (!File.Exists(spiderFolder + author + ".json"))
            {
                UnityEngine.Debug.Log("文件不存在,爬虫爬取文件失败 " + spiderFolder + author + ".json");
            }
            else
            {
                StreamReader reader = new StreamReader(spiderFolder + author + ".json");
                string jsonStr = reader.ReadToEnd();

                PoemSpiderData poemSpiderData = JsonUtility.FromJson<PoemSpiderData>(jsonStr);
                foreach (PoemSpider poemSpider in poemSpiderData.poem)
                {
                    poems.Add(poemSpider);
                }
                hasChangePoem = true;
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("爬虫爬取文件失败，出错原因：" + ex.Message);
        }
    }
    public static void GetPoemByType(object typeObj)
    {
        string type = (string)typeObj;
        try
        {
            if (!File.Exists(spiderFolder + type + ".json"))
            {
                if (!Directory.Exists(spiderFolder))
                {
                    Directory.CreateDirectory(spiderFolder);
                }
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(exeFolder + "GetPoemByType.exe", type + " " + spiderFolder + type + ".json");
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                myprocess.StartInfo = startInfo;
                myprocess.Start();
                myprocess.WaitForExit();
            }

            poems = new List<PoemSpider>();
            if (!File.Exists(spiderFolder + type + ".json"))
            {
                UnityEngine.Debug.Log("文件不存在,爬虫爬取文件失败 " + spiderFolder + type + ".json");
            }
            else
            {
                StreamReader reader = new StreamReader(spiderFolder + type + ".json");
                string jsonStr = reader.ReadToEnd();

                PoemSpiderData poemSpiderData = JsonUtility.FromJson<PoemSpiderData>(jsonStr);
                foreach (PoemSpider poemSpider in poemSpiderData.poem)
                {
                    poemSpider.type = type;
                    poems.Add(poemSpider);
                }
                hasChangePoem = true;
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("爬虫爬取文件失败，出错原因：" + ex.Message);
        }
    }

    public static void GetPoemByDynasty(object dynastyObj)
    {
        string dynasty = (string)dynastyObj;
        try
        {
            if (!File.Exists(spiderFolder + dynasty + ".json"))
            {
                if (!Directory.Exists(spiderFolder))
                {
                    Directory.CreateDirectory(spiderFolder);
                }
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(exeFolder + "GetPoemByDynasty.exe", dynasty + " " + spiderFolder + dynasty + ".json");
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                myprocess.StartInfo = startInfo;
                myprocess.Start();
                myprocess.WaitForExit();
            }

            poems = new List<PoemSpider>();
            if (!File.Exists(spiderFolder + dynasty + ".json"))
            {
                UnityEngine.Debug.Log("文件不存在,爬虫爬取文件失败 " + spiderFolder + dynasty + ".json");
            }
            else
            {
                StreamReader reader = new StreamReader(spiderFolder + dynasty + ".json");
                string jsonStr = reader.ReadToEnd();

                PoemSpiderData poemSpiderData = JsonUtility.FromJson<PoemSpiderData>(jsonStr);
                foreach (PoemSpider poemSpider in poemSpiderData.poem)
                {
                    poems.Add(poemSpider);
                }
                hasChangePoem = true;
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("爬虫爬取文件失败，出错原因：" + ex.Message);
        }
    }

    public static void GetPoemByBook(object bookObj)
    {
        string book = (string)bookObj;
        try
        {
            if (!File.Exists(spiderFolder + book + ".json"))
            {
                if (!Directory.Exists(spiderFolder))
                {
                    Directory.CreateDirectory(spiderFolder);
                }
                Process myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(exeFolder + "GetPoemByBoook.exe", book + " " + spiderFolder + book + ".json");
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                myprocess.StartInfo = startInfo;
                myprocess.Start();
                myprocess.WaitForExit();
            }

            poems = new List<PoemSpider>();
            if (!File.Exists(spiderFolder + book + ".json"))
            {
                UnityEngine.Debug.Log("文件不存在,爬虫爬取文件失败 " + spiderFolder + book + ".json");
            }
            else
            {
                StreamReader reader = new StreamReader(spiderFolder + book + ".json");
                string jsonStr = reader.ReadToEnd();

                PoemSpiderData poemSpiderData = JsonUtility.FromJson<PoemSpiderData>(jsonStr);
                foreach (PoemSpider poemSpider in poemSpiderData.poem)
                {
                    poemSpider.book = book;
                    poems.Add(poemSpider);
                }
                hasChangePoem = true;
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("爬虫爬取文件失败，出错原因：" + ex.Message);
        }
    }
    #endregion


    public static void Update()
    {
        if (hasChangeAuthors)
        {
            getAllAuthorEvent(authors);
            hasChangeAuthors = false;
        }
        if (hasChangeDynastys)
        {
            getAllDynastyEvent(dynastys);
            hasChangeDynastys = false;
        }
        if (hasChangeTypes)
        {
            getAllTypeEvent(types);
            hasChangeTypes = false;
        }
        if (hasChangeBooks)
        {
            getAllBookEvent(books);
            hasChangeBooks = false;
        }

        if (hasChangePoem)
        {
            getPoemEvent(poems);
            hasChangePoem = false;
        }
    }
}



[Serializable]
class AllAuthors
{
    public string[] authorList;
    public int count;
}

[Serializable]
class AllDynastys
{
    public string[] dynastyList;
    public int count;
}

[Serializable]
class AllTypes
{
    public string[] typeList;
    public int count;
}

[Serializable]
class PoemSpiderData
{
    public PoemSpider[] poem;
    public int count;
}

[Serializable]
public class PoemSpider
{
    public string title;
    public string author;
    public string dynasty;
    public string content;
    public string type;
    public string book;
    public string url;
    public string page;
    public string index;
    public string translation;
    public string annotation;
    public string appreciation;
}