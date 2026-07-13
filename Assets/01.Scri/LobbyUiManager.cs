using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;


public class LobbyUiManager : MonoBehaviour
{
    [SerializeField] protected GameObject dim;
    public void Start()
    {
        
    }

    virtual public void  OpenPanel()
    {
        Instantiate(dim);
    }

    virtual public void ExitPanel()
    {
        Destroy(dim);
    }

    

}
public class SetUPUi : LobbyUiManager
{
    [SerializeField] GameObject SetUP;
    [SerializeField] Slider BGMVolume;
    [SerializeField] Slider SFXVolume;
    [SerializeField] Slider VoiceVolume;
    private void Start()
    {
        SetUP.SetActive(false);
    }
    public override void OpenPanel()
    {
        base.OpenPanel();
        SetUP.SetActive(true);
        
    }
    public override void ExitPanel()
    {
        SetUP?.SetActive(false);
        base.ExitPanel();
        
    }

    public void GetBGMVolume(float volume)
    {
        volume = BGMVolume.value;
        SoundManager.instance.SetBGMVolume(volume);
    }
    public void GetVoiceVolume(float volume)
    { 
        volume = VoiceVolume.value;
        SoundManager.instance.SetVoiceVolume(volume);
    }
    public void GetSFXVolume(float volume)
    {
        volume = SFXVolume.value;
        SoundManager.instance.SetSFXVolume(volume);
    }
}
public class TrideUi : LobbyUiManager
{
    

     public GameObject TrideSlot;
    [SerializeField] GameObject TrideSelect;
    [SerializeField] TrideDataManager TrideM;
    [SerializeField] Transform TrideCanva;
   
    private Image SelectIcon;
    private TextMeshProUGUI SelectName;
    private TextMeshProUGUI SelectDescription;
    private TextMeshProUGUI SelectCharter;



    private int TrideId = -1;


    public List<TrideSlot> TrideUiList = new List<TrideSlot>();
 
    public void ItTrideSlot()
    {
        foreach (TrideSlot slot in TrideUiList)
        {
            if (slot != null) slot.gameObject.SetActive(false);
        }
        TrideUiList.Clear();
        for (int i = 0; i < TrideUiList.Count; i++)
        {
            int TrideId = TrideM.TrideList[i].id;

            var TrideData = DataManager.instance.GetTrideData(TrideId);
            if (TrideData != null)
            {
                GameObject go = Instantiate(TrideSlot, TrideCanva);
                TrideSlot slot = go.GetComponent<TrideSlot>();
                if (slot != null)
                {
                    slot.SetTride(TrideData.id, TrideData.icon, TrideData.name, TrideData.character, TrideData.trideDescription);
                    TrideUiList.Add(slot);
                }
            }
        }
    }


    private void Start()
    {
        TrideSlot.SetActive(false);
        TrideSelect.SetActive(false);
    }
    public override void OpenPanel()
    {
       base.OpenPanel();
        TrideSlot.SetActive(true);
    }
    public override void ExitPanel()
    {
        TrideSlot.SetActive(false);
        base.ExitPanel();
    }

    public void SelectTride(int id)
    {
       
        TrideId = id;

            var TrideData = DataManager.instance.GetTrideData(TrideId);
            if (TrideData != null)
            {
                SelectIcon.sprite = TrideData.icon;
                SelectName.text = TrideData.name;
                SelectDescription.text = TrideData.trideDescription;
                SelectCharter.text = TrideData.character;
            }
       

    }
    public void ExitSelect()
    {
        TrideSelect.SetActive(false);
    }

    public void TrideSlectOk()
    {
        if(TrideId ==-1)
        { return; }
        var TrideData = DataManager.instance.GetTrideData(TrideId);
        if (TrideData != null)
        {
            // 플레이어의 캐릭터가 이걸로 바뀔 예정
            // 선택한 id를 비교해서 이름 특성 종족설명 바뀜
        }


        TrideSelect.SetActive(false);
        TrideSlot.SetActive(false);
        Destroy(dim);
    }
}

public class TrainingUi : LobbyUiManager
{
    public static TrainingUi Instance;

    [SerializeField] GameObject TrainingPanel;


    [SerializeField] private TrainingDataManager TrainingM;
    public Sprite icon;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI name;
    public TextMeshProUGUI price;
    public GameObject Trainingslot;
    public Transform TrainingContent;
    public TextMeshProUGUI money;
    public TextMeshProUGUI NoMoney;
    public int haveMoney = 0;



    List<TrainingSlot> TrainingSlots = new List<TrainingSlot>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
        

    private void Start()
    {
        ItTrainingSlot();
        TrainingPanel.SetActive(false);
        NoMoney.gameObject.SetActive(false);
        money.text = $" 현재 소유 금액 : {haveMoney}";
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        TrainingPanel.SetActive(true);

    }
    public override void ExitPanel()
    {
        TrainingPanel.SetActive(false);
        base.ExitPanel();
    }
    public void ItTrainingSlot()
    {
        foreach(TrainingSlot slot in TrainingSlots)
        {
            if(slot != null) slot.gameObject.SetActive(false);
        }
        TrainingSlots.Clear();
        for(int i = 0; i < TrainingSlots.Count; i++)
        {
            int TrainingId = TrainingM.TrainingList[i].id;

            var TrainingData = DataManager.instance.GetTrainingData(TrainingId);
            if (TrainingData != null)
            {
                GameObject go = Instantiate(Trainingslot, TrainingContent);
                TrainingSlot slot = go.GetComponent<TrainingSlot>();
                if (slot != null)
                {
                    slot.SetTraining(TrainingData.id, TrainingData.icon, TrainingData.name, TrainingData.price, TrainingData.upgrad);
                    TrainingSlots.Add(slot);
                }
            }
        }
    }

    public void BuyXX()
    {
        NoMoney.gameObject.SetActive(true);
        StartCoroutine(BuyX(1));

        
    }
    
    IEnumerator BuyX(float time)
    {
        yield return new WaitForSeconds(time);
        NoMoney .gameObject.SetActive(false);
    }


}
public class StateUi : LobbyUiManager
{
    [SerializeField] GameObject SimpleState;
    [SerializeField] GameObject DetailState;


    private void Start()
    {
        SimpleState.SetActive(false);
        DetailState.SetActive(false);
    }
    public override void OpenPanel()
    {
        base.OpenPanel();
        SimpleState.SetActive(true);

    }

    public void OpenDetail()
    {
        DetailState.SetActive(true);
    }

    public override void ExitPanel()
    {
        SimpleState.SetActive(false);
        DetailState.SetActive(false);
        base.ExitPanel();
    }
    public void ExitDetail()
    {
        DetailState.SetActive(false);
    }

}

public class BattleUi : LobbyUiManager
{
    [SerializeField] GameObject Battle;
    public GameObject BattleSlot;
    //이거 아닌거 같기는 한데 안돼면 Slot에서 직접 번호 받아오기
    private int battleId;

    private void Start()
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
       switch(battleId)
        {
            case 0: ScenesM.instance.LoadHumenScene(); break;

            case 1: ScenesM.instance.LoadGoblinScene(); break;

            case 2: ScenesM.instance.LoadElfScene(); break;

            case 3: ScenesM.instance.LoadUndeadScene(); break;

            case 4: ScenesM.instance.LoadAngelScene(); break;
        }
    }


}

public class AlbumUi : LobbyUiManager
{
    [SerializeField] GameObject Memori;
    public GameObject TrideAlbum;

    private int AlbumId = -1;

    private void Start()
    {
        TrideAlbum.SetActive(false);
        Memori.SetActive(false);
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        TrideAlbum.SetActive(true);
    }

    public override void ExitPanel()
    {
        TrideAlbum.SetActive(false);
        base.ExitPanel();
    }

    public void ExitMemori()
    {
        Memori.SetActive(false);
    }

    public void ViewMemori()
    {
        if (AlbumId == -1)
            return;
        
    }
}
