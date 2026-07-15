using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Training
{
    public int id;
    public Sprite icon;
    public string name;
    public int price;
    public int upgrad;

    public string des;
    public float value;

    public Training(int id, Sprite icon, string name, int price, int upgrad , string des ,float value)
    {
        this.id = id;
        this.icon = icon;
        this.name = name;
        this.price = price;
        this.upgrad = upgrad;
        this.des = des;
        this.value = value;
    }

    public Training Clone()
    {
        return new Training(id, icon, name, price, upgrad,des,value);
    }

    public string GetDesc()
    {
        return string.Format(des, name ,value);
    }
}





[CreateAssetMenu(fileName = "TrainingData", menuName = "Data/Training")]
public class TrainingDataManager : ScriptableObject
{
       public List<Training> TrainingList = new List<Training>();
}
