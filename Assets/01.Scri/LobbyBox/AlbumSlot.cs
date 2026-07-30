using UnityEngine;
using UnityEngine.UI;

public class AlbumSlot : MonoBehaviour
{
    private AlbumUi album;
    public Image memori;
   

    private int Albumid = -1;

    public Sprite GetSprite() => memori.sprite;

    private void Start()
    {
        album = FindAnyObjectByType<AlbumUi>();
       
    }

    public void ViewMemori()
    {
        if(GetSprite()==null)
        {
            return;
        }
        if(Albumid == -1)
        {
            return;
        }
        album.SelectMemori(Albumid);
        album.ViewMemoris.SetActive(true);


    }

    public void SetMemori(Album album)
    {
        Albumid = album.id;
        if(album.isUnLocked)
        {
            memori.sprite = album.image;
            memori.color = Color.white;
        }
        else
        {
            memori.sprite = null;
            memori.color = Color.black;
        }
       
        

    }
}
