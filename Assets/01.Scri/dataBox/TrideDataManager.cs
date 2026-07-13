
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

    public Tride(int _id,Sprite _icon,string _name,float _maxHp,float _hp,int _damage,int _depence,float _critical,int _moneyUp,
        int _maxCharacter,int _heal,float _luck, float _block, float _miss,int _length,float _infection, float _kidanp, float _rivival, string _cahr,string _trideDescription)
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
    }

    public Tride Clone() 
    {
        return new Tride(id,icon,name,maxHp,hp,damage,depence,critical,moneyUp,maxCharacter,heal,luck,block,miss,length,infection,kidnap,rivival,character, trideDescription);
    }

}





[CreateAssetMenu(fileName = "TrideData",menuName = "Data/Tride")]
public class TrideDataManager : ScriptableObject
{
    public List<Tride> TrideList = new List<Tride>();
}
