using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TroopManager : MonoBehaviour
{
    private void Start()
    {
        
    }

    [SerializeField] private Spawners.TroopsAvaiable[] troopsChoosed = new Spawners.TroopsAvaiable[8];

    public void CreateJSON()
    {

        Mobs[] troopsChoosedTJ = new Mobs[1];
        troopsChoosedTJ[0] = new Mobs();

        troopsChoosedTJ[0].slot1 = troopsChoosed[0].ToString();
        troopsChoosedTJ[0].slot2 = troopsChoosed[1].ToString();
        troopsChoosedTJ[0].slot3 = troopsChoosed[2].ToString();
        troopsChoosedTJ[0].slot4 = troopsChoosed[3].ToString();
        troopsChoosedTJ[0].slot5 = troopsChoosed[4].ToString();
        troopsChoosedTJ[0].slot6 = troopsChoosed[5].ToString();
        troopsChoosedTJ[0].slot7 = troopsChoosed[6].ToString();
        troopsChoosedTJ[0].slot8 = troopsChoosed[7].ToString();

        string json = JsonHelper.ArrayToJsonString<Mobs>(troopsChoosedTJ, true);

        string mobilePath = Application.persistentDataPath + "/Equipment.json";
        File.WriteAllText(mobilePath, json);
        print(json);
    }

    Mobs[] LoadMobs()
    {
        string mobilePath = Application.persistentDataPath + "/Equipment.json";
        string json = File.ReadAllText(mobilePath);
        //print(json);
        Mobs[] mobs = JsonHelper.FromJsonString<Mobs>(json);
        return mobs;
    }

    public Spawners.TroopsAvaiable[] LoadMobsToString()
    {
        Mobs[] mobs = LoadMobs();
        troopsChoosed[0] = (Spawners.TroopsAvaiable)System.Enum.Parse(typeof(Spawners.TroopsAvaiable), mobs[0].slot1);
        troopsChoosed[1] = (Spawners.TroopsAvaiable)System.Enum.Parse(typeof(Spawners.TroopsAvaiable), mobs[0].slot2);
        troopsChoosed[2] = (Spawners.TroopsAvaiable)System.Enum.Parse(typeof(Spawners.TroopsAvaiable), mobs[0].slot3);
        troopsChoosed[3] = (Spawners.TroopsAvaiable)System.Enum.Parse(typeof(Spawners.TroopsAvaiable), mobs[0].slot4);
        troopsChoosed[4] = (Spawners.TroopsAvaiable)System.Enum.Parse(typeof(Spawners.TroopsAvaiable), mobs[0].slot5);
        troopsChoosed[5] = (Spawners.TroopsAvaiable)System.Enum.Parse(typeof(Spawners.TroopsAvaiable), mobs[0].slot6);
        troopsChoosed[6] = (Spawners.TroopsAvaiable)System.Enum.Parse(typeof(Spawners.TroopsAvaiable), mobs[0].slot7);
        troopsChoosed[7] = (Spawners.TroopsAvaiable)System.Enum.Parse(typeof(Spawners.TroopsAvaiable), mobs[0].slot8);


        return troopsChoosed;
    }

    [Serializable]
    public class Mobs
    {
        public string slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8;
    }
}
