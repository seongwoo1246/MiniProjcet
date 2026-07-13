using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrideSlot : MonoBehaviour
{
    public Sprite icon;
    public Sprite iconIn;
    public TextMeshProUGUI name;
    public TextMeshProUGUI character;
    public TextMeshProUGUI TrideDescription;

    private int TrideId = -1;
   
    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        { button.onClick.AddListener(onSlotClicked);}
    }
    public void onSlotClicked()
    {
        if (TrideId == -1)
            return;
        TrideUi.instance.SelectTride(TrideId);
    }

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
