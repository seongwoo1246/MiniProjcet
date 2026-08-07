
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum Yut
{
    zero,
    one,
    two,
    three,
    four,
    five,
    back = -1
}


public class MultiYutGameManager : MonoBehaviourPunCallbacks
{
    public static MultiYutGameManager instance;

    [SerializeField] TextMeshProUGUI resultYut;
    [SerializeField] TextMeshProUGUI TurnCount;
    [SerializeField] TextMeshProUGUI moCount;
    [SerializeField] TextMeshProUGUI yutCount;
    [SerializeField] TextMeshProUGUI yutname;
    [SerializeField] TextMeshProUGUI First;
    [SerializeField] public TextMeshProUGUI MaxCharCaption;

    [SerializeField] Button mo;
    [SerializeField] Button yut;
    [SerializeField] Button elseyut;
    [SerializeField] Button MyChar;
    [SerializeField] Button GoLobby;
    [SerializeField] Button TurnEND;

    [Header("UI 연결용")]
    public Button yutThrow;
    public Text turnInfoText;

    private bool canThrow = false;

    public int currentTurnPlayerActorNumber;

    public List<Yut> TurnYutResult = new List<Yut>();

    public List<YutPiace> allActiveChar = new List<YutPiace>();


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
        if (PhotonNetwork.LocalPlayer.ActorNumber != currentTurnPlayerActorNumber) return;

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

    public void MovePiece(int pieceId, int targetTileIndex)
    {
        if(PhotonNetwork.LocalPlayer.ActorNumber != currentTurnPlayerActorNumber)
        {
            Debug.Log("내 턴이 아닙니다.");
            return;
        }
        
        photonView.RPC("RPC_MovePiece",RpcTarget.All,pieceId, targetTileIndex);
    }

    [PunRPC]
    public void RPC_MovePiece(int pieceId, int targetTileIndex)
    {
        Debug.Log($"[말이동]{pieceId}번 말이 {targetTileIndex}칸으로 이동합니다.");


    }

    public Yut GetYut()
    {
        int backSideYut = 0;

        for (int i = 0; i < 6; i++)
        {
            if (Random.Range(0, 2) == 1)
            {
                backSideYut++;
            }
        }

        Yut result;

        switch (backSideYut)
        {
            case 6: result = Yut.back; break;
            case 5: result = Yut.zero; break;
            case 1: result = Yut.one; break;
            case 2: result = Yut.two; break;
            case 3: result = Yut.three; break;
            case 4: result = Yut.four; break;
            case 0: result = Yut.five; break;
            default: result = Yut.zero; break;
        }

        return result;
    }

    public void ThrowButtonControll()
    {
        if (canThrow)
        {
            yutThrow.gameObject.SetActive(true);
        }
        else
        {
            yutThrow.gameObject.SetActive(false);
        }
    }

    public void OnClickThrowButton()
    {
        SoundManager.instance.PlaySFX("뽕");
        if (!canThrow)
        { return; }
        ThrowYut();
    }

    public void ThrowYut()
    {



        

        

        Yut currentYut = GetYut();
        TurnYutResult.Add(currentYut);

        if (currentYut == Yut.zero)
        {
            SoundManager.instance.PlayVoice("앙대");
            resultYut.text = "저런 낙이 나왔습니다 턴을 넘기세요.";
            resultYut.gameObject.SetActive(true);
            StartCoroutine(FalseText(resultYut));
            TurnYutResult.Clear();
            moC = 0;
            moCount.text = $"{moC}";
            yutC = 0;
            yutCount.text = $"{yutC}";
            currentRestYut = Yut.zero;
            yutname.text = "";
            CanThrowEnemy = false;
            canthrow = false;
            return;
        }
        if (currentYut == Yut.four || currentYut == Yut.five)
        {
            if (currentYut == Yut.four)
            { yutC++; yutCount.text = $"{yutC}"; }
            else
            { moC++; moCount.text = $"{moC}"; }

            resultYut.text = $" {ChangeYutText(currentYut)}이 나왔군요. 한 번 더 던지세요";

            if (!IsEnemy)
            {
                CanThrow = true;
                canUseYut = false;
            }
            else
            {
                CanThrowEnemy = true;
                resultYut.gameObject.SetActive(true);
                StartCoroutine(FalseText(resultYut));
                return;
            }
        }
        else
        {
            resultYut.text = ChangeYutText(currentYut);
            yutname.text = ChangeYutText(currentYut);
            CanThrow = false;
            currentRestYut = currentYut;

            if (IsEnemy)
            {

                CanThrowEnemy = false;
            }
        }
        resultYut.gameObject.SetActive(true);
        StartCoroutine(FalseText(resultYut));
        if (!IsEnemy)
        { canUseYut = true; }
    }
}
