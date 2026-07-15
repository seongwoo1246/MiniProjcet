using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TrideSlot : MonoBehaviour
{
    public Image icon1;
    public TextMeshProUGUI SelectSlotName;

    
 
    protected int TrideId = -1;
   
   
    public void OnSlotTride()
    {
        if (TrideId == -1)
            return;
        TrideUi.instance.SelectTride(TrideId);
        TrideUi.instance.TrideSelect.SetActive(true);
    }

   
   

    public virtual void SetTride(int id ,Sprite icon, string name, string character, string Description)
    {
        if(id == -1) return;
       TrideId = id;
        if(icon != null)
       icon1.sprite = icon;
        if(name != null)
            SelectSlotName.text = name;

        if(TrideUi.instance != null)
        {
            TrideUi.instance.iconIn.sprite = icon;
            TrideUi.instance.name1.text = name;
            TrideUi.instance.character.text = character;
            TrideUi.instance.TrideDescription.text = Description;
        }
     
    }

}
