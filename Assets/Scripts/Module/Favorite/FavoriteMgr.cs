using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FavoriteMgr
{
    public static List<Favorite> JsonToFavorite(string jsonStr)
    {
        List<Favorite> favorites = new List<Favorite>();
        FavoriteData favoriteData = JsonUtility.FromJson<FavoriteData>(jsonStr);
        if(favoriteData == null || favoriteData.favorites == null)
        {
            return favorites;
        }
        foreach (Favorite favorite in favoriteData.favorites)
        {
            favorites.Add(favorite);
        }
        return favorites;
    }
}

[Serializable]
public class FavoriteData
{
    public Favorite[] favorites;
    public int count;
}