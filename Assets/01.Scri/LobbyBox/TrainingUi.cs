using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;
public class TrainingUi : LobbyUiManager
{
    public static TrainingUi Instance;

    [SerializeField] GameObject TrainingPanel;


    [SerializeField] private TrainingDataManager TrainingM;
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


    public override void Start()
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
      
        for (int i = 0; i < TrainingSlots.Count; i++)
        {
            
            var TrainingData = TrainingM.TrainingList[i].Clone();
           
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
        NoMoney.gameObject.SetActive(false);
    }


}
