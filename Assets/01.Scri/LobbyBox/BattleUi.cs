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
    [SerializeField] RawImage BaRaw;
    [SerializeField] VideoPlayer LoToEnd;
    [SerializeField] RawImage EnRaw;
    [SerializeField] Button LastBattle;
    [SerializeField] Image EndAfter1;
    [SerializeField] Button GoEndB;

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
        if (ScenesM.instance.IsviewEnd == true)
        {
            SoundManager.instance.PlayBGM(SoundManager.instance.hidenLobby);
            EndAfter1.gameObject.SetActive(true);
        }
        else
        {
            SoundManager.instance.PlayBGM(SoundManager.instance.nomalLobby);
            EndAfter1.gameObject.SetActive(false);
        }


        if (PlayerManager.Instance.CanAttackLastBoss ==true)
        {
            LastBattle.gameObject.SetActive(true);
        }
        else
        {
            LastBattle.gameObject.SetActive(false);
        }


        if(PlayerManager.Instance.CanGoEnd)
        {
            GoEndB.gameObject.SetActive(true);
        }
        else
        {
            GoEndB.gameObject.SetActive(false);
        }
            
        LoToEnd.gameObject.SetActive(false);
        EnRaw.gameObject.SetActive(false);


        BaRaw.gameObject.SetActive(false);
       
        Battle.SetActive(false);
       SelectEnemy.SetActive(false);
        LoToBa.gameObject.SetActive(false);
        LoToEnd.gameObject.SetActive(false);

        LoToEnd.loopPointReached += ToEnd;
        LoToBa.loopPointReached += ToBattle;
    }

    public void GoEnd()
    {
      LoToEnd.gameObject.SetActive(true);
      EnRaw.gameObject.SetActive(true);
      LoToEnd.Play();
    }

    public void setstatebattle()
    {
        if (ScenesM.instance.IsviewEnd == true)
        {
            SoundManager.instance.PlayBGM(SoundManager.instance.hidenLobby);
            EndAfter1.gameObject.SetActive(true);
        }
        else
        {
            SoundManager.instance.PlayBGM(SoundManager.instance.nomalLobby);
            EndAfter1.gameObject.SetActive(false);
        }


        if (PlayerManager.Instance.CanAttackLastBoss == true)
        {
            LastBattle.gameObject.SetActive(true);
        }
        else
        {
            LastBattle.gameObject.SetActive(false);
        }

        if (PlayerManager.Instance.CanGoEnd == true)
        {
            GoEndB.gameObject.SetActive(true);
        }
        else
        {
            GoEndB.gameObject.SetActive(false);
        }
    }

    // º¸½ºÇÑÅÂ °¡´Â ¹öÆ° 
    public void LastBattleStart()
    {
       
        SoundManager.instance.PlaySFX("ÆøÆÈ");
        LoToBa.gameObject.SetActive(true);
        BaRaw.gameObject.SetActive(true);
        LoToBa.Play();
    }

    

    void ToEnd(VideoPlayer player)
    {
        LoToEnd.gameObject.SetActive(false);
        EnRaw.gameObject.SetActive(false);
        
        ScenesM.instance.LoadScenes(scenetpye.Ending);
    }
    void ToBattle(VideoPlayer player)
    {
        if (PlayerManager.Instance.CanAttackLastBoss == true)
        {
            SaveLoadManager.instance.saveB.gameObject.SetActive(false);
            SaveLoadManager.instance.loadB.gameObject.SetActive(false);
            LoToBa.gameObject.SetActive(false);
            BaRaw.gameObject.SetActive(false);
            ScenesM.instance.LoadScenes(scenetpye.last);
        }
        else
        {
            SaveLoadManager.instance.saveB.gameObject.SetActive(false);
            SaveLoadManager.instance.loadB.gameObject.SetActive(false);
            LoToBa.gameObject.SetActive(false);
            BaRaw.gameObject.SetActive(false);
            ScenesM.instance.LoadScenes(ST);
        }

       

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
                    slot.SetTride(TrideData);
                    battleSlots.Add(slot);
                   
                }
            }
        }
    }

    public void RefreshBattleUi()
    {
        foreach (var slot in battleSlots)
        {
            if(slot != null) Destroy(slot.gameObject); 
        }
        battleSlots.Clear();

        ItBattleSlot();
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
        SoundManager.instance.PlaySFX("»Í");
        SelectEnemy.SetActive(false);
    }
    public void SelectBattle(int id)
    {
        SoundManager.instance.PlaySFX("»Í");
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
        SoundManager.instance.PlaySFX("»Í");
        
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
        BaRaw.gameObject.SetActive(true);
        LoToBa.Play();
        Destroy(dimClone);

      
        

    }


}

