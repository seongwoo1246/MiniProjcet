
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class StartUi : MonoBehaviour
{
    [SerializeField] Button StartB;
    [SerializeField] Button Easy;
    [SerializeField] Button Normal;
    [SerializeField] Button Hard;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Easy.gameObject.SetActive(false);
        Normal.gameObject.SetActive(false);
        Hard.gameObject.SetActive(false);

    }

   public void GameStart()
    {
        
        StartB.gameObject.SetActive(false);
        Easy.gameObject.SetActive(true);
        Normal.gameObject.SetActive(true);
        Hard.gameObject.SetActive(true);
    }

    public void EasyStart()
    {
        ScenesM.instance.SetDifficulty(Difficulty.easy);
        ScenesM.instance.LoadScenes(scenetpye.Lobby);
    }
    public void NormalStart()
    {
        ScenesM.instance.SetDifficulty(Difficulty.normal);
        ScenesM.instance.LoadScenes(scenetpye.Lobby); ;
    }

    public void HardStart()
    {
        ScenesM.instance.SetDifficulty(Difficulty.hard);
        ScenesM.instance.LoadScenes(scenetpye.Lobby);
       
    }
}
