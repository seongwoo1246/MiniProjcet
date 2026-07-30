using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TrideSlot : MonoBehaviour
{
    public Image icon1;
    public TextMeshProUGUI SelectSlotName;

   
    public Image UnLockedTride;


    protected int TrideId = -1;
   
   
    public void OnSlotTride()
    {
        if (TrideId == -1)
            return;
        TrideUi.instance.SelectTride(TrideId);
        TrideUi.instance.TrideSelect.SetActive(true);
    }

   
   

    public virtual void SetTride(Tride tride)
    {
        if(tride.id == -1) return;
       TrideId = tride.id;
        if(tride.icon != null)
       icon1.sprite = tride.icon;
        if(name != null)
            SelectSlotName.text = tride.name;

        UnLockedTride.gameObject.SetActive(!tride.isUnLocked);
        if (TrideUi.instance != null)
        {
            TrideUi.instance.iconIn.sprite = tride.icon;
            TrideUi.instance.name1.text = tride.name;
            TrideUi.instance.character.text = tride.character;
            TrideUi.instance.TrideDescription.text = tride.trideDescription;
        }
     
    }

}
