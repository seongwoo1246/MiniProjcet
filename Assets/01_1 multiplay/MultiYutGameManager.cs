
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;




public class MultiYutGameManager : MonoBehaviourPunCallbacks
{
    public static MultiYutGameManager instance;

   
    public int currentTurnPlayerActorNumber;
    public PhotonView view;


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

        BattleSceneManager.instance.ThrowButton.interactable = isMyTurn;
        if(isMyTurn)
        {
            BattleSceneManager.instance.resultYut.text = "내턴입니다. 윷을 던지세요.";
        }
        else
        {
            BattleSceneManager.instance.resultYut.text = "상대의 턴입니다.";
        }
    }

    [PunRPC]
    public void RRPC_ShowYUtResult(Yut result , string playername)
    {
        
        Debug.Log($"{playername}님이 {(result)}을(를) 던졌습니다.");
    }

    public void OnClickEndTurn()
    {
        Player nextPlayer = PhotonNetwork.LocalPlayer.GetNext();
        if(nextPlayer != null )
        {
            SetTurn(nextPlayer.ActorNumber);
        }
    }

    public void MovePiece(int pieceId)
    {
        if(PhotonNetwork.LocalPlayer.ActorNumber != currentTurnPlayerActorNumber)
        {
            Debug.Log("내 턴이 아닙니다.");
            return;
        }
        
        photonView.RPC("RPC_MovePiece",RpcTarget.All,pieceId);
    }

    [PunRPC]
    public void RPC_MovePiece(int pieceId)
    {
        YutPiace piace = null;

        foreach(YutPiace p in BattleSceneManager.instance.allActiveChar)
        {
            if(p.priceID == pieceId)
                piace = p;
             break;
        }
      
        if(piace !=  null)
        {
            piace.StartMove(BattleSceneManager.instance.selectMoveSpace);
        }
    }

  
 
}
