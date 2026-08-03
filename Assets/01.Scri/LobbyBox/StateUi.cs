using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StateUi : LobbyUiManager
{

    public static StateUi Instance;

    [SerializeField] GameObject SimpleState;
    
    [SerializeField] GameObject StateSlot1;
    [SerializeField] Transform SimpleCanva;

    [SerializeField] Image EndAfter1;
    [SerializeField] public TextMeshProUGUI save;
    [SerializeField] public TextMeshProUGUI load;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

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

       save.gameObject.SetActive(false);
       load.gameObject.SetActive(false);

        save.gameObject.SetActive(false);
        load.gameObject.SetActive(false);
        save.text = "흠냐흠냐 오늘 있던 일을 일기에 적습니다. (-.-)zZ";
        load.text = "오늘 하루도 힘차게 출발합니다.(*ㅅ*)>";

        SimpleState.SetActive(false);
       
    }
    public override void OpenPanel()
    {
        SetState();
        base.OpenPanel();
        SimpleState.SetActive(true);

    }
    public override void ExitPanel()
    {
        SimpleState.SetActive(false);
       
        base.ExitPanel();
    }
   

    public void SetState()
    {
        if (SimpleCanva != null)
        {
            foreach (Transform t in SimpleCanva)
            {
                Destroy(t.gameObject);
            }
        }
        if (PlayerManager.Instance != null && PlayerManager.Instance.PlayerData != null)
        {



            var playerState = PlayerManager.Instance.PlayerData.TrideDataDictionnary();

            foreach (var state in playerState)
            {
                GameObject go = Instantiate(StateSlot1, SimpleCanva);
                StateSlot slot = go.GetComponent<StateSlot>();
                if (slot != null)
                {
                    slot.SetStateSlot(state.Key, state.Value);
                }
            }
        }
    }


}

