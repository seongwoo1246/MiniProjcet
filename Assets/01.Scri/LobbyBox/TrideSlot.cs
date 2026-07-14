using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TrideSlot : MonoBehaviour
{
    public Image icon;

    protected TrideUi trideUi;
    protected BattleUi battleUi;
 
    private int TrideId = -1;
   
     void Start()
    {
       
        trideUi = GetComponent<TrideUi>();
      battleUi = GetComponent<BattleUi>();
        
    }
    public void OnSlotTride()
    {
        if (TrideId == -1)
            return;
        trideUi.SelectTride(TrideId);
        trideUi.TrideSelectSpace.SetActive(true);
    }

   
   

    public void SetTride(int id ,Sprite icon, string name, string character, string Description)
    {


       TrideId = id;
       this.icon.sprite = icon;
        TrideUi.instance.iconIn.sprite = icon;
        TrideUi.instance.name1.text = name;
        TrideUi.instance.character.text = character;
        TrideUi.instance.TrideDescription.text = Description;
    }

}
