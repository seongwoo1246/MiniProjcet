using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
public class YutBoardController : MonoBehaviour
{
    public static YutBoardController instance;

    public Tilemap roadmap;
    public Grid gridSystem;
    //인스펙터 창에서 보이게 하는 기능
    [Header("윷판 메인 경로(0번 : 출발지 마지막 : 골인)")]
    public List<Vector3Int> mainPathSpace = new List<Vector3Int>();

    public List<Vector3Int> shortCutSpace = new List<Vector3Int>();

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public Vector3 GetWorldPosition(Vector3Int space)
    {
        return roadmap.GetCellCenterWorld(space);
    }

    private void SetPath()
    {
        mainPathSpace.Clear();
        shortCutSpace.Clear();

        
     
          for(int i = -3;i<=5;i+=2)
            {
                mainPathSpace.Add(new Vector3Int(4, i));
                
            }

        
        
    }


}
