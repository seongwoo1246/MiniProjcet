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

    public List<Vector3Int> shortCutSpring = new List<Vector3Int>();
    public List<Vector3Int> shortCutSummer = new List<Vector3Int>();
    public List<Vector3Int> shortCutAutumn = new List<Vector3Int>();
    
    

    private void Awake()
    {
        if(instance == null)
            instance = this;
        SetPath();
    }

    public Vector3 GetWorldPosition(Vector3Int space)
    {
        return roadmap.GetCellCenterWorld(space);
    }

    private void SetPath()
    {
         mainPathSpace.Clear();
        shortCutSpring.Clear();
        shortCutSummer.Clear();
        shortCutAutumn.Clear();


       
        
        for (int i = -3;i<=5;i+=2)
            {
                mainPathSpace.Add(new Vector3Int(4, i));
                
            }

          for(int i=2; i>=-6;i-=2)
        {
            mainPathSpace.Add(new Vector3Int(i, 5));
        }
          for(int i=3; i>=-5;i-=2)
        {
            mainPathSpace.Add(new Vector3Int(-6, i));
        }
          for(int i=-4; i<=4;i+=2)
        {
            mainPathSpace.Add(new Vector3Int(i, -5));
        }

      

        //가을 후트 첫번째 코너에서 중앙 타고 골인 루트
       
        shortCutAutumn.Add(new Vector3Int(-1, 0));
        shortCutAutumn.Add(new Vector3Int(1, -2));
        shortCutAutumn.Add(new Vector3Int(2, -3));
        //봄 루트 두번째 코너에서 중앙쪽으로 오는 루트
        shortCutSpring.Add(new Vector3Int(-6, 5));
        shortCutSpring.Add(new Vector3Int(-4, 3));
        shortCutSpring.Add(new Vector3Int(-3, 2));
        shortCutSpring.Add(new Vector3Int(-1, 0));
        shortCutSpring.Add(new Vector3Int(1, -2));
        shortCutSpring.Add(new Vector3Int(2, -3));
        //여름 루트 첫번째 코너에서 중앙 안타고 외곽으로 도는 루트
        shortCutSummer.Add(new Vector3Int(4,5));
        shortCutSummer.Add(new Vector3Int(2, 3));
        shortCutSummer.Add(new Vector3Int(1, 2));
        shortCutSummer.Add(new Vector3Int(-1, 0));
        shortCutSummer.Add(new Vector3Int(-3, -2));
        shortCutSummer.Add(new Vector3Int(-4, -3));

    }


}
