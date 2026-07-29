
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class StartUi : MonoBehaviour
{
    [SerializeField] Button StartB;
    [SerializeField] Button Easy;
    [SerializeField] Button Normal;
    [SerializeField] Button Hard;
    [SerializeField] VideoPlayer VideoPlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Easy.gameObject.SetActive(false);
        Normal.gameObject.SetActive(false);
        Hard.gameObject.SetActive(false);
        VideoPlay.gameObject.SetActive(false);
        VideoPlay.loopPointReached += OnvideoEnd;
    }

    void OnvideoEnd(VideoPlayer videoPlayer)
    {
        VideoPlay.gameObject.SetActive(false);
        ScenesM.instance.LoadScenes(scenetpye.Lobby);

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
        VideoPlay.gameObject.SetActive(true);
        VideoPlay.Play();
        
    }
    public void NormalStart()
    {
        ScenesM.instance.SetDifficulty(Difficulty.normal);
        VideoPlay.gameObject.SetActive(true);
        VideoPlay.Play();

    }

    public void HardStart()
    {
        ScenesM.instance.SetDifficulty(Difficulty.hard);
        VideoPlay.gameObject.SetActive(true);
        VideoPlay.Play();


    }
}
