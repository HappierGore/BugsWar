using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public GameObject[] troopsList;
    public enum TroopsAvaiable { AntSoldier, AntArcher };

    public void SpawnTroop(TroopsAvaiable troops, Transform castle)
    {
        GameObject clone = Instantiate(FindTroopInTroopList(troops), castle) as GameObject;
        if(castle.name == "EnemyCastle")
        {
            MobStats cloneStats = clone.GetComponent<MobStats>();
            cloneStats.IsEnemy();
            clone.tag = "enemies";
        }
    }

    private GameObject FindTroopInTroopList(TroopsAvaiable troops)
    {
        GameObject result = null;
        for (int i = 0; i < troopsList.Length; i++)
        {
            if (troopsList[i].name == troops.ToString())
            {
                result = troopsList[i];
            }
        }
        return result;
    }
}
