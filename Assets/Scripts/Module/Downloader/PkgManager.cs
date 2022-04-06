using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public static class PkgManager
{
    //public static string globalVersionFilePath = Application.persistentDataPath + "/" + "GlobalVersion.json";
    //public static string localVersionFilePath = Application.persistentDataPath + "/" + "LocalVersion.json";
    public static string poemsSaveFolder = Application.persistentDataPath + "/" + "Poems"; 
    public static string pkgSavePath = Application.persistentDataPath + "/" + "Poems.zip";
    public static string poemsFolderSavePath = Application.persistentDataPath + "/" + "Poems";

    private static string host = "120.25.231.117";//服务器端ip地址
    private static IPAddress ip = IPAddress.Parse(host);
    private static IPEndPoint ipe = new IPEndPoint(ip, 8000);
    private static Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


    public static void BeginReceivePkg()
    {
        Thread receivePkgThread = new Thread(ReceivePkg);
        receivePkgThread.Start();
    }

    private static void ReceivePkg()
    {
        listenSocket.Connect(ipe);
        string sendMsg = "DownloadPkg";
        byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg);
        listenSocket.Send(arrClientSendMsg);

        //获得[文件名]   
        string SendFileName = System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(listenSocket));

        //获得[包的大小]   
        string bagSize = System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(listenSocket));

        //获得[包的总数量]   
        int bagCount = int.Parse(System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(listenSocket)));

        //获得[最后一个包的大小]   
        string bagLast = System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(listenSocket));

        FileInfo t = new FileInfo(pkgSavePath);
        if (t.Exists) //判断文件是否存在
        {
            t.Delete();
        }

        //创建一个新文件   
        FileStream MyFileStream = new FileStream(pkgSavePath, FileMode.Create, FileAccess.Write);

        //已发送包的个数   
        int SendedCount = 0;
       
        byte[] data = TransferFiles.ReceiveVarData(listenSocket);
        if (data.Length == 0)
        {
            //关闭文件流   
            MyFileStream.Close();
            //关闭套接字   
            listenSocket.Close();
            return;
        }
        else
        {
            SendedCount++;
            MyFileStream.Write(data, 0, data.Length);
        }
        
        //关闭文件流   
        MyFileStream.Close();
        //关闭套接字   
        listenSocket.Close();
        Debug.Log("成功从服务器上下载到" + SendFileName);
        DownloaderMgr.OnDownloadPkg(pkgSavePath);
    }

    public static void UnZipPkg()
    {
        if (Directory.Exists(poemsFolderSavePath))
        {
            DelectDir(poemsFolderSavePath);
        }
        
        try
        {
            ZipFile.ExtractToDirectory(pkgSavePath, poemsSaveFolder);
            Debug.Log("资源解压完成");
         
        }
        catch (Exception ex)
        {
            Debug.LogError("文件解压失败 " + ex.Message);
        }
      
    }

    public static void DelectDir(string srcPath)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)            //判断是否文件夹
                {
                    DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                    subdir.Delete(true);          //删除子目录和文件
                }
                else
                {
                    File.Delete(i.FullName);      //删除指定文件
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    //public static void BeginReceivePkgAuthor(string author)
    //{
    //    Thread thread = new Thread(ReceivePkgAuthor);
    //    thread.Start(author);
    //}

    //private static void ReceivePkgAuthor(object author)
    //{
    //    Debug.Log(0);
    //    //listenSocket.Connect(ipe);

    //    string authorStr = (string)author;
    //    string sendMsg = "DownloadPoemsByAuthor"  + " "+ authorStr; 
    //    byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg);
    //    listenSocket.Send(arrClientSendMsg);

    //    Debug.Log(1);
    //    //获得[文件名]   
    //    string SendFileName = System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(listenSocket));

    //    //获得[包的大小]   
    //    string bagSize = System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(listenSocket));

    //    //获得[包的总数量]   
    //    int bagCount = int.Parse(System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(listenSocket)));

    //    //获得[最后一个包的大小]   
    //    string bagLast = System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(listenSocket));

    //    Debug.Log("成功从服务器上下载到" + SendFileName);

    //    string fullPath = downloadPoemsFolderPath + "/" + "Author" + "/" + authorStr + ".json";
    //    //创建一个新文件   
    //    FileStream MyFileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);

    //    //已发送包的个数   
    //    int SendedCount = 0;

    //    byte[] data = TransferFiles.ReceiveVarData(listenSocket);
    //    if (data.Length == 0)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        SendedCount++;
    //        MyFileStream.Write(data, 0, data.Length);
    //    }

    //    //关闭文件流   
    //    MyFileStream.Close();
    //    //关闭套接字   
    //    listenSocket.Close();
    //    DownloaderMgr.OnDownloadPoemByAuthor(fullPath);
    //}


}

//[Serializable] 
//public class PkgVersionData
//{
//    public PkgVersionChildData[] authors;
//    public PkgVersionChildData[] types;
//    public PkgVersionChildData[] book;
//    public PkgVersionChildData[] dynasty;
//}

//[Serializable]
//public class PkgVersionChildData
//{
//    public string dataName;
//    public string version;
//    public string md5;
//}