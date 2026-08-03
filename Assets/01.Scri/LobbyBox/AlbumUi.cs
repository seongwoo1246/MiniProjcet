using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumUi : LobbyUiManager
{
    public static AlbumUi Instance;

    [SerializeField] private AlbumDataManager AlbumM;


    
    [SerializeField] GameObject Memori;
    public GameObject ViewMemoris;
    public Image viewMemoriSprite;
    public GameObject TrideAlbum;

   
    public Transform AlbumCanva;

    

    public List<AlbumSlot> albumSlots = new List<AlbumSlot>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
            Destroy(gameObject);
    }




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
         
            var albumData = AlbumM.AlbumList[i];
           
            
            if (albumData != null)
            {
                GameObject go = Instantiate(Memori, AlbumCanva);
                AlbumSlot slot = go.GetComponent<AlbumSlot>();
             
                if (slot != null)
                {
                    slot.SetMemori(albumData);
                    albumSlots.Add(slot);
                    slot.gameObject.SetActive(true);
                }
            }
        }
    }

    public void ReFreshAlbumUI()
    {
        foreach (var slot in albumSlots)
        {
            if(slot != null) Destroy(slot.gameObject);
        }
        albumSlots.Clear();

        ItAlbumSlot();
    }
    public void SelectMemori(int id )
    {
        SoundManager.instance.PlaySFX("»Í");
        //id°¡ 1000ÀÌ¸é ¸®½ºÆ® 1000¹øÂ° ¾øÀ¸´Ï ¹üÀ§¸¦ ¹þ¾î³µ´Ù°í ³ª¿È
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
        SoundManager.instance.PlaySFX("»Í");
        ViewMemoris.SetActive(false);
    }


}
