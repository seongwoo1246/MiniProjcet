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

    TrideUi trideUi;
    BattleUi battleUi;
 
    private int TrideId = -1;
   
    private void Start()
    {
       
        trideUi = GetComponent<TrideUi>();
      battleUi = GetComponent<BattleUi>();
    }
    public void OnSlotTride()
    {
        if (TrideId == -1)
            return;
        trideUi.SelectTride(TrideId);
        trideUi.TrideSlot.SetActive(true);
    }

    public void OnSlotBattle()
    {
        if (TrideId == -1)
            return;
        trideUi.SelectTride(TrideId);
        battleUi.BattleSlot.SetActive(true);
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
