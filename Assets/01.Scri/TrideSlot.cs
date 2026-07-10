using TMPro;
using UnityEngine;

public class TrideSlot : MonoBehaviour
{
    public Sprite icon;
    public Sprite iconIn;
    public TextMeshProUGUI name;
    public TextMeshProUGUI character;
    public TextMeshProUGUI TrideDescription;

    public int TrideId = -1;
    public int GetTrideId() => TrideId;

    public void SetTride(int id ,Sprite icon, string name, string character, string Description)
    {
       TrideId = id;
        this.icon = icon;
        iconIn = icon;
        this.name.text = name;
        this.character.text = character;
        TrideDescription.text = Description;
    }

}
