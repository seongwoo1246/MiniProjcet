using UnityEngine;


public class StateUi : LobbyUiManager
{
    [SerializeField] GameObject SimpleState;
    
    [SerializeField] GameObject StateSlot1;
    [SerializeField] Transform SimpleCanva;
    

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
                t.gameObject.SetActive(false);
            }
        }
        if (PlayerManager.instance != null || PlayerManager.instance.PlayerData != null)
        {



            var playerState = PlayerManager.instance.PlayerData.TrideDataDictionnary();

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

