using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BattleUi : LobbyUiManager
{
    [SerializeField] GameObject Battle;
    public GameObject BattleSlot;

    private Image SelectIcon;
    private TextMeshProUGUI SelectName;
    private TextMeshProUGUI SelectDescription;
    private TextMeshProUGUI SelectCharter;

    private int battleId =-1;

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
    public void SelectBattle(int id)
    {

        battleId = id;

        var TrideData = DataManager.instance.GetTrideData(battleId);
        if (TrideData != null)
        {
            SelectIcon.sprite = TrideData.icon;
            SelectName.text = TrideData.name;
            SelectDescription.text = TrideData.trideDescription;
            SelectCharter.text = TrideData.character;
        }


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

