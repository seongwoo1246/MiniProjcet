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
    [SerializeField] GameObject TrideSlot;
    [SerializeField] GameObject TrideSelect;
    private Image H_Icon1;
    private Image G_Icon2;
    private Image E_Icon3;
    private Image U_Icon4;
    private Image A_Icon5;
    private TextMeshProUGUI H_name;
    private TextMeshProUGUI G_name;
    private TextMeshProUGUI E_name;
    private TextMeshProUGUI U_name;
    private TextMeshProUGUI A_name;
    private Image SelectIcon;
    private TextMeshProUGUI SelectName;
    private TextMeshProUGUI SelectDescription;
    private TextMeshProUGUI SelectCharter;
    // 반복문으로 줄일 수 있는건 줄이자

    public int TrideId = -1;
    public int GetTrideId() => TrideId;


    public List<TrideUi> TrideUiList = new List<TrideUi>();
    

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

    public void OpenSelect()
    {
        TrideSelect.SetActive(true);
        // 플레이어의 캐릭터가 이걸로 바뀔 예정
        // 선택한 id를 비교해서 맞으면 정보 교환 하는 식
    }
    public void ExitSelect()
    {
        TrideSelect.SetActive(false);
    }

    public void TrideSlectOk()
    {
        // 플레이어의 캐릭터가 이걸로 바뀔 예정
        // 선택한 id를 비교해서 이름 특성 종족설명 바뀜
        TrideSelect.SetActive(false);
        TrideSlot.SetActive(false);
        Destroy(dim);
    }
}

public class TrainingUi : LobbyUiManager
{
    public static TrainingUi Instance;

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
        NoMoney.gameObject.SetActive(false);
        money.text = $" 현재 소유 금액 : {haveMoney}";
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
