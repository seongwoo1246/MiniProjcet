
using UnityEngine;
using UnityEngine.UI;

public class BattleSlot : TrideSlot
{
    private int battleId = -1;

    public Image UnLockedBattle;


    public void OnSlotBattle()
    {
        if (battleId == -1)
            return;
        BattleUi.Instance.SelectBattle(battleId);
        BattleUi.Instance.SelectEnemy.SetActive(true);
    }

    public override void SetTride(Tride tride)
    {
        if (tride.id == -1) return;
        battleId = tride.id;
        TrideId = tride.id;
        
        UnLockedBattle.gameObject.SetActive(!tride.isUnLocked);
        if (icon1 != null)
            icon1.sprite = tride.icon;
        
        if (name != null)
            SelectSlotName.text = tride.name;

        if ( BattleUi.Instance != null )
        {
            BattleUi.Instance.iconIn.sprite = tride.icon;
            BattleUi.Instance.name1.text = tride.name;
            BattleUi.Instance.character.text = tride.character;
            BattleUi.Instance.TrideDescription.text = tride.trideDescription;
        }
    }




}
