using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

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
    public List<TrideSaveData> TribeStates = new List<TrideSaveData>();
    public List<AlbumSaveData> AlbumSaveDatas = new List<AlbumSaveData>();
    public List<TrainingSaveData> TrainingSaveDatas = new List<TrainingSaveData>();
}




public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] TrideDataManager trideM;
    [SerializeField] AlbumDataManager albumM;
    [SerializeField] TrainingDataManager trainingM;
    [SerializeField] Button SaveB;
    [SerializeField] Button LoadB;


    public void SaveGame()
    {
        SaveData sd = new SaveData();
        sd.haveMoney = PlayerManager.Instance.haveMoney;

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
        string path = Application.persistentDataPath+"/save.json";

        if (!File.Exists(path)) return;

        SaveData sd = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
        PlayerManager.Instance.haveMoney = sd.haveMoney;

        foreach (var saved in sd.TribeStates)
        {
            Tride original = trideM.TrideList.Find(t => t.id == saved.id);
            if (original == null) continue;
            
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

        foreach( var saved in sd.TrainingSaveDatas)
        {
            if(!PlayerManager.Instance.TrideUpgradeLevels.ContainsKey(saved.trideid))
            {   
                PlayerManager.Instance.TrideUpgradeLevels.Add(saved.trideid, new Dictionary<int, Training>());
                Training baseTraining = trainingM.TrainingList.Find(t => t.id ==saved.trideid);
                if(baseTraining != null)
                {
                    Training clone = baseTraining.Clone();
                    clone.upgrad = saved.upgrad;
                    clone.price = saved.price;
                    PlayerManager.Instance.TrideUpgradeLevels[saved.trideid][saved.slotid] = clone;
                }
            }
        }

        foreach(var saved in sd.AlbumSaveDatas)
        {
            Album target = albumM.AlbumList.Find(a => a.id == saved.id);
            if( target!=null) target.isUnLocked = saved.isUnLocked;
        }
        
    }





}
