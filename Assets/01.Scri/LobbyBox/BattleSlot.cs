
using UnityEngine.UI;

public class BattleSlot : TrideSlot
{
    private int battleId = -1;

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnSlotBattle);
    }


    public void OnSlotBattle()
    {
        if (battleId == -1)
            return;
        battleUi.SelectBattle(battleId);
        battleUi.BattleSlot.SetActive(true);
    }
}
