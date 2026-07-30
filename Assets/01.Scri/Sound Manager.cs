using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;
    [SerializeField] AudioSource Voice;

    //BGM
    public AudioClip nomalStart;
    public AudioClip nomalLobby;
    public AudioClip hidenStart;
    public AudioClip hidenLobby;
    public AudioClip humen;
    public AudioClip goblin;
    public AudioClip elf;
    public AudioClip undead;
    public AudioClip angel;
    public AudioClip lastboss;

    //SFX와Voice 는 Resources/Sounds에서 이름으로 찾는 방식 채용
 

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
        BGM.volume = volume;
   
        PlayerPrefs.SetFloat("BGMSound",volume);
    }
    public void SetSFXVolume(float volume)
    {
        SFX.volume = volume;
        
        PlayerPrefs.SetFloat("SFXSound", volume);
    }

    public void SetVoiceVolume(float volume)
    {
        Voice.volume= volume;
    
        PlayerPrefs.SetFloat("VoiceSound",volume);
    }

    public void PlayBGM(AudioClip bgm)
    {
        if (BGM.clip == bgm) return;
        BGM.clip = bgm;
        BGM.loop = true;
        BGM.Play();
    
    }

    public void PlaySFX(string soundname)
    {
        AudioClip sfx = Resources.Load<AudioClip>("Sounds/"+soundname);
        if(sfx != null)
        {
            SFX.PlayOneShot(sfx);
        }
    }
    public void PlayVoice(string soundname)
    {
        AudioClip cv = Resources.Load<AudioClip>("Sounds/"+soundname);
        if(cv != null)
        {
            SFX.PlayOneShot(cv);
        }
    }

  




}
