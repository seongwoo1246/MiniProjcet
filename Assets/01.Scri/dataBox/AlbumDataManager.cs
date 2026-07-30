
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Album
{
    public int id;
    public Sprite image;
    public bool isUnLocked;

    public Album(int _id,Sprite _image)
    {
        id = _id;
        image = _image;
        isUnLocked = false;
    }

    public Album Clone() 
    { Album album = new Album(id,image);
        album.isUnLocked = isUnLocked;
        return album;
    }
}

[CreateAssetMenu(fileName ="AlbumData",menuName ="Data/Album")]
public class AlbumDataManager : ScriptableObject
{
    public List<Album> AlbumList = new List<Album>();
}
