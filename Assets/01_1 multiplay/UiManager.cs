using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class UiManager : MonoBehaviourPunCallbacks
{
    [Header("UI ½½·Ô ¼³Á¤")]
    public PlayerSlotUI mySlot;
    public List<PlayerSlotUI> otherSlot;

    private void Start()
    {
        UpdatePlayerSlots();
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerSlots();
    }

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        UpdatePlayerSlots();
    }

    public override void OnPlayerEnteredRoom(Player otherPlayer)
    {
        UpdatePlayerSlots();
    }

    public void UpdatePlayerSlots()
    {
        if (!PhotonNetwork.InRoom) return;

        Player Myplayer = PhotonNetwork.LocalPlayer;
        mySlot.SetPlayerInfo(Myplayer.NickName, 0, 4);

        foreach(var slot in otherSlot)
        {
            slot.ClearSlot();
        }

        int otherIndex = 0;
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsLocal) continue;

            if(otherIndex < otherSlot.Count)
            {
                otherSlot[otherIndex].SetPlayerInfo(player.NickName, 0, 4);
            }
        }
    }

}
