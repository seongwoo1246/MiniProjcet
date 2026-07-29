using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class BattleUi : LobbyUiManager
{
    public static BattleUi Instance;



    [SerializeField] TrideDataManager TrideM;
    [SerializeField] GameObject Battle;
    [SerializeField] Transform BattleCanvas;
    [SerializeField] GameObject BattleSlot;
    [SerializeField] VideoPlayer LoToBa;
    [SerializeField] VideoPlayer LoToEnd;
    [SerializeField] Button LastBattle;


    private scenetpye ST;
    public GameObject SelectEnemy;

    public Image iconIn;
    public TextMeshProUGUI name1;
    public TextMeshProUGUI character;
    public TextMeshProUGUI TrideDescription;

    private int battleId =-1;

    public List<BattleSlot> battleSlots = new List<BattleSlot>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
            ItBattleSlot();
        }
        else
            Destroy(gameObject);
    }



    public override void Start()
    {
        LastBattle.gameObject.SetActive(false);
        Battle.SetActive(false);
       SelectEnemy.SetActive(false);
        LoToBa.gameObject.SetActive(false);
        LoToEnd.gameObject.SetActive(false);

        LoToEnd.loopPointReached += ToEnd;
        LoToBa.loopPointReached += ToBattle;
    }

    // 보스한태 가는 버튼 
    public void LastBattleStart()
    {

        LoToBa.gameObject.SetActive(true);
        LoToBa.Play();
        
    }

    

    void ToEnd(VideoPlayer player)
    {
        LoToBa.gameObject.SetActive(false);
        //ScenesM.instance.LoadScenes();
    }
    void ToBattle(VideoPlayer player)
    {
        LoToBa.gameObject.SetActive(false);
        ScenesM.instance.LoadScenes(ST);
    }

    
    


    public void ItBattleSlot()
    {

        for (int i = 0; i < TrideM.TrideList.Count; i++)
        {
          
            var TrideData = TrideM.TrideList[i].Clone();
           
            if (TrideData != null)
            {
                GameObject go = Instantiate(BattleSlot, BattleCanvas);
                BattleSlot slot = go.GetComponent<BattleSlot>();

                if (slot != null)
                {
                    slot.gameObject.SetActive(true);
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
        SelectEnemy.SetActive(false);
    }
    public void SelectBattle(int id)
    {

        battleId = id;

        var TrideData = TrideM.TrideList[battleId].Clone();
        if (TrideData != null)
        {
            iconIn.sprite = TrideData.icon;
            name1.text = TrideData.name;
            TrideDescription.text = TrideData.trideDescription;
            character.text = TrideData.character;
        }


    }
    public void StartBattel()
    {
        if (battleId == -1)
            return;
        Battle.SetActive(false);
        SelectEnemy.SetActive(false);

       
        switch (battleId)
        {
            case 0:
                ST = scenetpye.humun;

                break;

            case 1:
                ST = scenetpye.goblin;

                break;

            case 2:
                ST = scenetpye.elf;

                break;

            case 3:
                ST = scenetpye.undead;

                break;

            case 4:
                ST = scenetpye.angel;

                break;
        }

        LoToBa.gameObject.SetActive(true);
        LoToBa.Play();
        Destroy(dimClone);

      
        

    }


}

