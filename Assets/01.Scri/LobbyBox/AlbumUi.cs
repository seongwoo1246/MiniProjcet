using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumUi : LobbyUiManager
{
    [SerializeField] private AlbumDataManager AlbumM;


    
    [SerializeField] GameObject Memori;
    public GameObject ViewMemoris;
    public Image viewMemoriSprite;
    public GameObject TrideAlbum;

   
    public Transform AlbumCanva;

    

    public List<AlbumSlot> albumSlots = new List<AlbumSlot>();
    public override void Start()
    {
        
        ViewMemoris.SetActive(false);
        TrideAlbum.SetActive(false);
        
        ItAlbumSlot();
    }

    public void ItAlbumSlot()
    {
        
        for (int i = 0; i < AlbumM.AlbumList.Count; i++)
        {
         
            var albumData = AlbumM.AlbumList[i].Clone();
           
            
            if (albumData != null)
            {
                GameObject go = Instantiate(Memori, AlbumCanva);
                AlbumSlot slot = go.GetComponent<AlbumSlot>();
             
                if (slot != null)
                {
                    slot.SetMemori(albumData.id, albumData.image);
                    albumSlots.Add(slot);
                    slot.gameObject.SetActive(true);
                }
            }
        }
    }

    public void SelectMemori(int id )
    {

        //id가 1000이면 리스트 1000번째 없으니 범위를 벗어났다고 나옴
        var Data = AlbumM.AlbumList[id-1000].Clone();
       
        
        if (Data != null)
        {
           
            viewMemoriSprite.sprite = Data.image;
        }
    }


    public override void OpenPanel()
    {
        base.OpenPanel();
        TrideAlbum.SetActive(true);
    }

    public override void ExitPanel()
    {
        TrideAlbum.SetActive(false);
          base.ExitPanel();
    }

    public void ViewMemoriExit()
    {
        ViewMemoris.SetActive(false);
    }


}
