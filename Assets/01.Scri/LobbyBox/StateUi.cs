using UnityEngine;


public class StateUi : LobbyUiManager
{
    [SerializeField] GameObject SimpleState;
    [SerializeField] GameObject DetailState;


    public override void Start()
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

