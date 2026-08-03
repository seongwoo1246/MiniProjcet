using UnityEngine;

public class Playerdata : MonoBehaviour
{
    private void Awake()
    { if(PlayerManager.Instance != null)
        {// 플레이어 UI 갱신 담당 스크립트
            PlayerManager.Instance.UpdataUI(gameObject);
        }

       
    }
}
