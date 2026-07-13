using UnityEngine;
using UnityEngine.UI;

public class AlbumSlot : MonoBehaviour
{
    private AlbumUi album;
    private Sprite memori;

    private int Albumid = -1;

    public Sprite GetSprite() => memori;

    private void Start()
    {
        album = GetComponent<AlbumUi>();
        Button button = GetComponent<Button>();
        if(button != null )
        { button.onClick.AddListener(ViewMemori); }
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
        memori = sprite;

    }
}
