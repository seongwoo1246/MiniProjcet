
using UnityEngine;
using UnityEngine.UI;

public class BattleSlot : TrideSlot
{
    private int battleId = -1;

   


    public void OnSlotBattle()
    {
        if (battleId == -1)
            return;
        battleUi.SelectBattle(battleId);
        battleUi.SelectEnemy.SetActive(true);
    }

    public override void SetTride(int id, Sprite icon, string name, string character, string Description)
    {
        if (id == -1) return;
        battleId = id;
        TrideId = id;

        if(this.icon != null)
        {
            this.icon.sprite = icon;
        }
        
        if( BattleUi.Instance != null )
        {
            BattleUi.Instance.iconIn.sprite = icon;
            BattleUi.Instance.name1.text = name;
            BattleUi.Instance.character.text = character;
            BattleUi.Instance.TrideDescription.text = Description;
        }
    }
}
