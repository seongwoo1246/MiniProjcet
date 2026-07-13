
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Album
{
    public int id;
    public Sprite image;

    public Album(int _id,Sprite _image)
    {
        id = _id;
        image = _image;
    }

    public Album Clone() { return new Album(id,image); }
}

[CreateAssetMenu(fileName ="AlbumData",menuName ="Data/Album")]
public class AlbumDataManager : ScriptableObject
{
    public List<Album> AlbumList = new List<Album>();
}
