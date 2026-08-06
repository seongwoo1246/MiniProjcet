
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MultiYutGameManager : MonoBehaviourPunCallbacks
{
    public static MultiYutGameManager instance;

    [Header("UI 연결용")]
    public Button yutThrow;
    public Text turnInfoText;



    public int currentTurnPlayerActorNumber;



    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            SetTurn(PhotonNetwork.LocalPlayer.ActorNumber);
        }

    }


    public void SetTurn(int playerActorNumer)
    {
        photonView.RPC("RPC_UpdateTurn", RpcTarget.AllBuffered, playerActorNumer);
    }

    [PunRPC]
    public void RPC_UpdateTurn(int playerActorNumer)
    {
        currentTurnPlayerActorNumber = playerActorNumer;

        bool isMyTurn = (PhotonNetwork.LocalPlayer.ActorNumber == playerActorNumer);

        yutThrow.interactable = isMyTurn;
        if(isMyTurn)
        {
            turnInfoText.text = "내턴입니다. 윷을 던지세요.";
        }
        else
        {
            turnInfoText.text = "상대의 턴입니다.";
        }
    }

    public void OnClickThrowYut()
    {
        int yutResult = Random.Range(0,7);

        photonView.RPC("RPC_ShowYUtResult",RpcTarget.All, yutResult,PhotonNetwork.LocalPlayer.NickName);


    }

    [PunRPC]
    public void RRPC_ShowYUtResult(int result , string playername)
    {
        string[] yutNames = { "낙", "도", "개", "걸", "윷", "모", "빽도" };
        Debug.Log($"{playername}님이 [{yutNames[result]}]을(를) 던졌습니다.");
    }

    public void OnClickEndTurn()
    {
        Player nextPlayer = PhotonNetwork.LocalPlayer.GetNext();
        if(nextPlayer != null )
        {
            SetTurn(nextPlayer.ActorNumber);
        }
    }
}
