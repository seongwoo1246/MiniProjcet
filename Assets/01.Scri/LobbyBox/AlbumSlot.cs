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

    public void SetMemori(int id, Sprite sprite)
    {
        Albumid = id;
        memori.sprite = sprite;

    }
}
