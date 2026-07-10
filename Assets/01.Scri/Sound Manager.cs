using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // 배경음 ( 배경음 조절때 같이 조절 될 예정) 시간 없으면 배틀 BGM은 통일 예정
    [SerializeField] AudioSource StartSceneBgm;
    [SerializeField] AudioSource LobbySceneBgm;
    [SerializeField] AudioSource HumenSceneBgm;
    [SerializeField] AudioSource UndeadSceneBgm;
    [SerializeField] AudioSource GoblinSceneBgm;
    [SerializeField] AudioSource ElfSceneBgm;
    [SerializeField] AudioSource AngelSceneBgm;
    [SerializeField] AudioSource GameEndSceneBgm;


    //효과음 (효과음 조절떄 같이 조절 될 예정)
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource DrowSound;
    [SerializeField] AudioSource WinSound;
    [SerializeField] AudioSource DefeatSound;
    [SerializeField] AudioSource TakeDamageSound;
    
    

    //캐릭터 음성 (캐릭터 음성 조절시 같이 조절 될 예정)우선순위 나중
    [SerializeField] AudioSource HumenSound;
    [SerializeField] AudioSource GoblinSound;
    [SerializeField] AudioSource ElfSound;
    [SerializeField] AudioSource AngelSound;
    [SerializeField] AudioSource UndeadSound;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetBGMVolume(PlayerPrefs.GetFloat("BGMSound", 0.5f));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXSound", 0.5f));
        SetVoiceVolume(PlayerPrefs.GetFloat("VoiceSound", 0.5f));
    }

    public void SetBGMVolume(float volume)
    {
        StartSceneBgm.volume = volume;
        LobbySceneBgm.volume = volume;
        HumenSceneBgm.volume = volume;
        UndeadSceneBgm.volume = volume;
        GoblinSceneBgm.volume = volume;
        ElfSceneBgm.volume = volume;
        AngelSceneBgm.volume = volume;
        GameEndSceneBgm.volume = volume;
        PlayerPrefs.SetFloat("BGMSound",volume);
    }
    public void SetSFXVolume(float volume)
    {
        clickSound.volume = volume;
        DrowSound.volume = volume;
        WinSound.volume = volume;
        DefeatSound.volume = volume;
        TakeDamageSound.volume = volume;
        PlayerPrefs.SetFloat("SFXSound", volume);
    }

    public void SetVoiceVolume(float volume)
    {
        HumenSound.volume= volume;
        GoblinSound.volume= volume;
        ElfSound.volume= volume;
        AngelSound.volume= volume;
        UndeadSound.volume= volume;
        PlayerPrefs.SetFloat("VoiceSound",volume);
    }

  
}
