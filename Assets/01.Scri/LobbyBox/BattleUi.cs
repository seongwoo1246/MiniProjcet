using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BattleUi : LobbyUiManager
{
    [SerializeField] TrideDataManager TrideM;
    [SerializeField] GameObject Battle;
    [SerializeField] Transform BattleCanvas;
    public GameObject BattleSlot;


    private Image SelectIcon;
    private TextMeshProUGUI SelectName;
    private TextMeshProUGUI SelectDescription;
    private TextMeshProUGUI SelectCharter;

    private int battleId =-1;

    List<BattleSlot> battleSlots = new List<BattleSlot>();

    public override void Start()
    {
        
        Battle.SetActive(false);
        BattleSlot.SetActive(false);
        ItBattleSlot();
    }


    public void ItBattleSlot()
    {

        for (int i = 0; i < battleSlots.Count; i++)
        {


            var TrideData = TrideM.TrideList[battleId].Clone();
           
            if (TrideData != null)
            {
                GameObject go = Instantiate(BattleSlot, BattleCanvas);
                BattleSlot slot = go.GetComponent<BattleSlot>();
               
                if (slot != null)
                {
                    slot.SetTride(TrideData.id, TrideData.icon, TrideData.name, TrideData.character, TrideData.trideDescription);
                    battleSlots.Add(slot);
                }
            }
        }
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

        var TrideData = TrideM.TrideList[battleId].Clone();
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

