using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StateUi : LobbyUiManager
{

    public static StateUi Instance;

    [SerializeField] GameObject SimpleState;
    
    [SerializeField] GameObject StateSlot1;
    [SerializeField] Transform SimpleCanva;

    
 
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

