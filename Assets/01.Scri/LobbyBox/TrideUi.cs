using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TrideUi : LobbyUiManager
{
    public static TrideUi instance;

    [SerializeField] GameObject TrideSlot;
    [SerializeField] TrideDataManager TrideM;
    [SerializeField] Transform TrideCanva;
    [SerializeField] GameObject TrideSelectSpace;
    public GameObject TrideSelect;

    public Image iconIn;
    public TextMeshProUGUI name1;
    public TextMeshProUGUI character;
    public TextMeshProUGUI TrideDescription;

    private int TrideId = -1;


    public List<TrideSlot> TrideUiList = new List<TrideSlot>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            ItTrideSlot();
            
        }
        else
            Destroy(gameObject);
            
    }
   


    public override void Start()
    {

        TrideSelectSpace.SetActive(false);
        TrideSelect.SetActive(false);
    }

    public void ItTrideSlot()
    {

        for (int i = 0; i < TrideM.TrideList.Count; i++)
        {
           
            var TrideData = TrideM.TrideList[i].Clone();

            if (TrideData != null)
            {
                GameObject go = Instantiate(TrideSlot, TrideCanva);
                TrideSlot slot = go.GetComponent<TrideSlot>();


                if (slot != null)
                {
                    slot.SetTride(TrideData.id, TrideData.icon, TrideData.name, TrideData.character, TrideData.trideDescription);
                    TrideUiList.Add(slot);
                    slot.gameObject.SetActive(true);
                }
            }
        }
    }
    public override void OpenPanel()
    {
        base.OpenPanel();
        TrideSelectSpace.SetActive(true);
    }
    public override void ExitPanel()
    {
        TrideSelectSpace.SetActive(false);
        base.ExitPanel();
    }

    public void SelectTride(int id)
    {

        TrideId = id;

        var TrideData = TrideM.TrideList[TrideId].Clone();
        if (TrideData != null)
        {
           iconIn.sprite = TrideData.icon;
            name1.text = TrideData.name;
            TrideDescription.text = TrideData.trideDescription;
            character.text = TrideData.character;
        }


    }
    public void ExitSelect()
    {
        TrideSelect.SetActive(false);
    }

    public void TrideSlectOk()
    {
        if (TrideId == -1)
        { return; }
        var TrideData = TrideM.TrideList[TrideId].Clone();
        if (TrideData != null)
        {
           PlayerManager.instance.SetTridePlayer(TrideData);  
        }


        TrideSelect.SetActive(false);
        TrideSelectSpace.SetActive(false);
        Destroy(dim);
    }
}
