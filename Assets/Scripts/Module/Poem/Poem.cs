using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Poem
{
    /// <summary>
    /// 诗词名
    /// </summary>
    public string title = "";

    /// <summary>
    /// 作者
    /// </summary>
    public string author = "";

    /// <summary>
    /// 朝代
    /// </summary>
    public string dynasty = "";

    /// <summary>
    /// 诗词内容
    /// </summary>
    public string content = "";

    /// <summary>
    /// 诗词类型
    /// </summary>
    public string type = "";

    /// <summary>
    /// 诗词所属诗籍
    /// </summary>
    public string book = "";

    /// <summary>
    /// 诗词注释
    /// </summary>
    public string annotation = "";

    /// <summary>
    /// 诗词翻译
    /// </summary>
    public string translation = "";

    /// <summary>
    /// 诗词赏析
    /// </summary>
    public string appreciation = "";

    public Poem(string _title, string _author, string _dynasty, string _content, string _type, string _book, string _annotation,
        string _translation, string _appreciation)
    {
        this.title = _title;
        this.author = _author;
        this.dynasty = _dynasty;
        this.content = _content;
        this.type = _type;
        this.book = _book;
        this.annotation = _annotation;
        this.translation = _translation;
        this.appreciation = _appreciation;
    }

    public override string ToString()
    {
        return string.Format("title: {0}, author: {1}, dynasty: {2}, content: {3}, type:{4} ",
            title, author, dynasty, content, type);
    }


}
