using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
   


    public Tride PlayerData {  get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    public void SetTridePlayer(Tride Data)
    {
        PlayerData = Data.Clone();
    }

    public void TakeDamage()
    {
        //회피율을 이용한 회피 여부
        //크리트컬 여부 계산후 공격력에 더해주기 (데미지 2배)
        //데미지 입을시 체력에 방어력 뺀 만큼 공격력만큼 줄이기
    }

    public void Attack()
    {
         
    }


}
