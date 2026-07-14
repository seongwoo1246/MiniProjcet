
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
}
