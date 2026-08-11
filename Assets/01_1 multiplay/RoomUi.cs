using UnityEngine;
using TMPro;
using Photon.Pun;


public class RoomUi : MonoBehaviour
{
    public TextMeshProUGUI roomNameText;
    public TextMeshProUGUI playerCountText;

    private string roomName;

    public void SetRoomInfo(string name , int currentplayers , int MaxPlayers)
    {
        roomName = name;
        roomNameText.text = roomName;
        playerCountText.text = $"{currentplayers}/{MaxPlayers}";
    }

    public void OnClickJoinRoom()
    {
        if(!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }
}
