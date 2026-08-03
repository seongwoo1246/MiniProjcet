
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tride
{
    public int id;
    public Sprite icon;
    public string name;
    public float maxHp;
    public float hp;
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
    public string character;
    public string trideDescription;
    public bool isUnLocked;

    public Tride(int _id, Sprite _icon, string _name, float _maxHp, float _hp, int _damage, int _depence, float _critical, int _moneyUp,
        int _maxCharacter, int _heal, float _luck, float _block, float _miss, int _length, float _infection, float _kidanp, float _rivival, string _cahr, string _trideDescription)
    {
        id = _id;
        icon = _icon;
        name = _name;
        maxHp = _maxHp;
        hp = _hp;
        damage = _damage;
        depence = _depence;
        critical = _critical;
        moneyUp = _moneyUp;
        maxCharacter = _maxCharacter;
        heal = _heal;
        luck = _luck;
        block = _block;
        miss = _miss;
        length = _length;
        infection = _infection;
        kidnap = _kidanp;
        rivival = _rivival;
        character = _cahr;
        trideDescription = _trideDescription;
        isUnLocked = false;
    }

    public Tride Clone()
    {
        Tride tride= new Tride(id, icon, name, maxHp, hp, damage, depence, critical, moneyUp, maxCharacter, heal, luck, block, miss, length, infection, kidnap, rivival, character, trideDescription);
        tride.isUnLocked = isUnLocked;
        return tride;
    }


   public  Dictionary<String, String> TrideDataDictionnary()
    {
        return new Dictionary<string, string>()
        {
            {"이름",name },
            {"체력",maxHp.ToString()},
            {"공격력",damage.ToString()},
            {"방어력" ,depence.ToString()},
            {"치명율",$"{critical*100 }%".ToString() },
            {"돈 획득량",moneyUp.ToString() },
            {"최대 말 갯수",maxCharacter.ToString() },
            {"치유력" ,heal.ToString()},
            {"행운",$"{luck*100}%".ToString() },
            {"방해확율" ,$"{block * 100}%".ToString()},
            {"회피율" ,$"{miss * 100} %".ToString()},
            {"사거리" ,length.ToString()},
            {"전염확율" ,$"{infection * 100} %".ToString()},
            {"납치 확율" ,$"{kidnap * 100} %".ToString()},
            {"부활확률",$"{rivival * 100} %".ToString() }
        };
    } 
}





[CreateAssetMenu(fileName = "TrideData",menuName = "Data/Tride")]
public class TrideDataManager : ScriptableObject
{
    public List<Tride> TrideList = new List<Tride>();
}
