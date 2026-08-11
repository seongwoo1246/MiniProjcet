using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class Laincher : MonoBehaviourPunCallbacks
{
    public TMP_InputField nickNameInput;
    public TMP_InputField createRoomNameInput;
    public TMP_InputField searchRoomNameInput;

    public Transform roomListContent;
    public GameObject roomItemPrefab;

    private Dictionary<string,RoomInfo> cacheRoomList = new Dictionary<string,RoomInfo>();


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
        cacheRoomList.Clear();
    }

    public void SetPlayerNickname()
    {
        if(string.IsNullOrEmpty(nickNameInput.text))
        {
            Debug.Log("닉네임을 입력해주세요.");
            return;
        }
        PhotonNetwork.NickName= nickNameInput.text;
        Debug.Log("닉네임 설정 완료.");
    }

    public void CreateCustomRoom()
    {
        if(string.IsNullOrEmpty(createRoomNameInput.text))
        {
            Debug.Log("방 이름을 입력해주세요.");
            return;
        }
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 5,
            IsVisible = true,
            IsOpen = true,
        };

        PhotonNetwork.CreateRoom(createRoomNameInput.text, roomOptions);
    }

    public void JoinSearchRoom()
    {
        if(string.IsNullOrEmpty (searchRoomNameInput.text))
        {
            Debug.Log("방을 찾을 수가 없습니다.");
            return;
        }
        PhotonNetwork.JoinRoom(searchRoomNameInput.text);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
        UpdateRoomListUi();
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomlist)
    {
        foreach(RoomInfo room in roomlist)
        {
            if(room.RemovedFromList||!room.IsVisible||!room.IsOpen)
            {
                if(cacheRoomList.ContainsKey(room.Name))
                {
                    cacheRoomList.Remove(room.Name);
                }
            }
            else
            {
                cacheRoomList[room.Name] = room;
            }
        }
    }
    private void UpdateRoomListUi()
    {
        foreach(Transform child in roomListContent)
        {
            Destroy(child.gameObject);
        }

        foreach(RoomInfo room in cacheRoomList.Values)
        {
            GameObject go = Instantiate(roomItemPrefab, roomListContent);

            RoomUi roomScript = go.GetComponent<RoomUi>();
            if (roomScript != null)
            {
                roomScript.SetRoomInfo(room.Name, room.PlayerCount, room.MaxPlayers);
            }

        }
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
