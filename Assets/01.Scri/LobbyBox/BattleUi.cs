using UnityEngine;


public class BattleUi : LobbyUiManager
{
    [SerializeField] GameObject Battle;
    public GameObject BattleSlot;
    //이거 아닌거 같기는 한데 안돼면 Slot에서 직접 번호 받아오기
    private int battleId;

    public override void Start()
    {
        Battle.SetActive(false);
        BattleSlot.SetActive(false);
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        Battle.SetActive(true);

    }

    public override void ExitPanel()
    {
        Battle.SetActive(false);
        base.ExitPanel();
    }

    public void ExitSelectBattle()
    {
        BattleSlot.SetActive(false);
    }

    public void StartBattel()
    {
        if (battleId == -1)
            return;
        switch (battleId)
        {
            case 0: ScenesM.instance.LoadHumenScene(); break;

            case 1: ScenesM.instance.LoadGoblinScene(); break;

            case 2: ScenesM.instance.LoadElfScene(); break;

            case 3: ScenesM.instance.LoadUndeadScene(); break;

            case 4: ScenesM.instance.LoadAngelScene(); break;
        }
    }


}

