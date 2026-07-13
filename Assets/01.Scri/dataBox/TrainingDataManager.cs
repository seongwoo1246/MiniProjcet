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

    public Training(int id, Sprite icon, string name, int price, int upgrad)
    {
        this.id = id;
        this.icon = icon;
        this.name = name;
        this.price = price;
        this.upgrad = upgrad;
    }

    public Training Clone()
    {
        return new Training(id, icon, name, price, upgrad);
    }
}





[CreateAssetMenu(fileName = "TrainingData", menuName = "Data/Training")]
public class TrainingDataManager : ScriptableObject
{
       public List<Training> TrainingList = new List<Training>();
}
