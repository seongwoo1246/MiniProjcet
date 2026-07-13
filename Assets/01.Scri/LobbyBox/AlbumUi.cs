using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumUi : LobbyUiManager
{
    [SerializeField] private AlbumDataManager AlbumM;


    public Transform dimP;
    [SerializeField] GameObject Memori;
    public GameObject ViewMemoris;
    public Image viewMemoriSprite;
    public GameObject TrideAlbum;

    private Sprite imege;
    public Transform AlbumCanva;



    List<AlbumSlot> albumSlots = new List<AlbumSlot>();
    public override void Start()
    {
        ViewMemoris.SetActive(false);
        TrideAlbum.SetActive(false);
        Memori.SetActive(false);
        ItAlbumSlot();
    }

    public void ItAlbumSlot()
    {
        foreach (var slot in albumSlots)
        {
            if (slot != null) slot.gameObject.SetActive(false);
        }
        albumSlots.Clear();
        for (int i = 0; i < AlbumM.AlbumList.Count; i++)
        {
            int albumId = AlbumM.AlbumList[i].id;

            var albumData = DataManager.instance.GetAlbumData(albumId);
            if (albumData != null)
            {
                GameObject go = Instantiate(Memori, AlbumCanva);
                AlbumSlot slot = go.GetComponent<AlbumSlot>();
                if (slot != null)
                {
                    slot.SetMemori(albumData.id, albumData.image);
                    albumSlots.Add(slot);
                }
            }
        }
    }

    public void SelectMemori(int id)
    {


        var Data = DataManager.instance.GetAlbumData(id);
        if (Data != null)
        {
            imege = Data.image;
            viewMemoriSprite.sprite = Data.image;
        }
    }


    public override void OpenPanel()
    {
        
        TrideAlbum.SetActive(true);
    }

    public override void ExitPanel()
    {
        TrideAlbum.SetActive(false);
          
    }

    public void ViewMemoriExit()
    {
        ViewMemoris.SetActive(false);
    }


}
