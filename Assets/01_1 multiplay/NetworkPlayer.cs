using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkPlayer : MonoBehaviourPun
{
    public string playername;
    public int remainpieceCount = 4;
    public int escapepieceCount = 0;

    private void Start()
    {
        if(photonView.IsMine)
        {
            playername = PhotonNetwork.NickName;
        }
        else
        {
            playername = photonView.Owner.NickName;
        }



    }


  


}
