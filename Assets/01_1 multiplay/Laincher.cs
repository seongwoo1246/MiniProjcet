using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class Laincher : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    private void Start()
    {
        Debug.Log("포톤 서버에 연결중 ");
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("포톤 서버 접속 완료! 로비 입장 중...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비 입장 완료 이제 방에 들어갈 수 있습니다.");
    }

    //방참가 버튼
    public void JoinOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 5 };
        PhotonNetwork.JoinOrCreateRoom("MyRoom", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"방 입장 성공! 현재 방 :" + PhotonNetwork.CurrentRoom.Name);
       
    }

   

}
