using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


[Serializable]
public class TrideSaveData
{
    public int id;
    public bool isUnLocked;
    public float hp;
    public float maxhp;
    public int damage;
    public int depence;
    public float critical;
    public int moneyUp;
    public int maxCharacter;
    public int heal;
    public float luck;
    public float block;
    public float miss;
    public int length;
    public float infection;
    public float kidnap;
    public float rivival;
}
[Serializable]
public class AlbumSaveData
{
    public int id;
    public bool isUnLocked;
}
[Serializable]
public class TrainingSaveData
{
    public int trideid;
    public int slotid;
    public int upgrad;
    public int price;
}
[Serializable]
public class SaveData
{
    public int haveMoney;
    public int lastSelectTrideId;
    public bool CanAttackLastBossData;
    public bool CanGoEndData;
    public List<TrideSaveData> TribeStates = new List<TrideSaveData>();
    public List<AlbumSaveData> AlbumSaveDatas = new List<AlbumSaveData>();
    public List<TrainingSaveData> TrainingSaveDatas = new List<TrainingSaveData>();
}




public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] TrideDataManager trideM;
    [SerializeField] AlbumDataManager albumM;
    [SerializeField] TrainingDataManager trainingM;
    [SerializeField] TextMeshProUGUI save;
    [SerializeField] TextMeshProUGUI load;


    public static SaveLoadManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        save.gameObject.SetActive(false);
        load.gameObject.SetActive(false);
        save.text = "Èì³ÄÈì³Ä ¿À´Ã ÀÖ´ø ÀÏÀ» ÀÏ±â¿¡ Àû½À´Ï´Ù. (-.-)zZ";
        load.text = "¿À´Ã ÇÏ·çµµ ÈûÂ÷°Ô Ãâ¹ßÇÕ´Ï´Ù.(*¤µ*)>";
    }
    public IEnumerator SaveLoadFalseText(TextMeshProUGUI Text)
    {
        yield return new WaitForSeconds(2f);
        Text.gameObject.SetActive(false);

    }

    public void SaveGame()
    {
        SoundManager.instance.PlaySFX("»Í");
        save.gameObject.SetActive(true);
        StartCoroutine(SaveLoadFalseText(save));

        SaveData sd = new SaveData();
        sd.haveMoney = PlayerManager.Instance.haveMoney;
        sd.lastSelectTrideId = PlayerManager.Instance.PlayerData.id;
        sd.CanGoEndData = PlayerManager.Instance.CanGoEnd;
        sd.CanAttackLastBossData = PlayerManager.Instance.CanAttackLastBoss;

        foreach (var t in PlayerManager.Instance.TrideDataDic)
        {
            Tride T = t.Value;
            sd.TribeStates.Add(new TrideSaveData
            {
                id = T.id,
                isUnLocked = T.isUnLocked,
                hp = T.hp,
                maxhp = T.maxHp,
                damage = T.damage,
                depence = T.depence,
                critical = T.critical,
                moneyUp = T.moneyUp,
                maxCharacter = T.maxCharacter,
                heal = T.heal,
                luck = T.luck,
                block = T.block,
                miss = T.miss,
                length = T.length,
                infection = T.infection,
                kidnap = T.kidnap,
                rivival = T.rivival,
            });
        }

        foreach (var trideEntry in PlayerManager.Instance.TrideUpgradeLevels)
        {
            int trideId = trideEntry.Key;
            foreach (var slotEnttry in trideEntry.Value)
            {
                sd.TrainingSaveDatas.Add(new TrainingSaveData
                {
                    trideid = trideId,
                    slotid = slotEnttry.Key,
                    upgrad = slotEnttry.Value.upgrad,
                    price = slotEnttry.Value.price
                });

            }
        }

        foreach( var a in  albumM.AlbumList)
        {
            sd.AlbumSaveDatas.Add( new AlbumSaveData { id = a.id, isUnLocked = a.isUnLocked });
        }

        string json =JsonUtility.ToJson(sd,true);
        File.WriteAllText(Application.persistentDataPath+ "/save.json",json);
       
    }

    public void LoadGame()
    {
        SoundManager.instance.PlaySFX("»Í");
        load.gameObject.SetActive(true);
        StartCoroutine(SaveLoadFalseText(load));
        string path = Application.persistentDataPath+"/save.json";
     
        if (File.Exists(path) ==false) return;

        SaveData sd = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
        if (sd == null) return;
        PlayerManager.Instance.haveMoney = sd.haveMoney;
        TrainingUi.Instance.money.text = $"ÇöÀç ¼ÒÀ¯ µ· : {sd.haveMoney}";
        PlayerManager.Instance.CanGoEnd = sd.CanGoEndData;
        PlayerManager.Instance.CanAttackLastBoss = sd.CanAttackLastBossData;
        foreach (var saved in sd.TribeStates)
        {
            Tride original = trideM.TrideList.Find(t => t.id == saved.id);
            if (original == null) continue;

            original.isUnLocked = saved.isUnLocked;

            Tride t = original.Clone();

                t.isUnLocked = saved.isUnLocked;
                t.hp = saved.hp;
                t.maxHp = saved.maxhp;
                t.damage = saved.damage;
                t.depence = saved.depence;
                t.critical = saved.critical;
                t.moneyUp = saved.moneyUp;
                t.maxCharacter = saved.maxCharacter;
                t.heal = saved.heal;
                t.luck = saved.luck;
                t.block = saved.block;
                t.miss = saved.miss;
                t.length = saved.length;
                t.infection = saved.infection;
                t.kidnap = saved.kidnap;
                t.rivival = saved.rivival;

            PlayerManager.Instance.TrideDataDic[saved.id] = t;
            
        }

        foreach (var saved in sd.TrainingSaveDatas)
        {
            if (!PlayerManager.Instance.TrideUpgradeLevels.ContainsKey(saved.trideid))
            {
                PlayerManager.Instance.TrideUpgradeLevels.Add(saved.trideid, new Dictionary<int, Training>());
            }
                Training baseTraining = trainingM.TrainingList.Find(t => t.id ==saved.slotid);
                if(baseTraining != null)
                {
                    Training clone = baseTraining.Clone();
                    clone.upgrad = saved.upgrad;
                    clone.price = saved.price;
                    PlayerManager.Instance.TrideUpgradeLevels[saved.trideid][saved.slotid] = clone;
                }
            
        }

        foreach(var saved in sd.AlbumSaveDatas)
        {
            Album target = albumM.AlbumList.Find(a => a.id == saved.id);
            if( target!=null) target.isUnLocked = saved.isUnLocked;
        }

        if(PlayerManager.Instance.TrideDataDic.ContainsKey(sd.lastSelectTrideId))
        {
            PlayerManager.Instance.SelectTride(PlayerManager.Instance.TrideDataDic[sd.lastSelectTrideId]);
        }


        TrainingUi.Instance.TrainingReFreshSlot();
        TrideUi.instance.ReFreshTrideUI();
        AlbumUi.Instance.ReFreshAlbumUI();
        BattleUi.Instance.RefreshBattleUi();
        StateUi.Instance.SetState();

    }

   
    
       



    



}
