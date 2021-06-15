using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    Spawners spawners;
    Transform enemyCastle;
    bool Fase1Cooldown = false;
    bool RunFase1 = true;
    private void Start()
    {
        enemyCastle = GameObject.Find("EnemyCastle").transform;
        spawners = GameObject.Find("Manager").GetComponent<Spawners>();
    }
    void Update()
    {
        if(enemyCastle.gameObject.activeSelf)
        {
            if (RunFase1)
                StartCoroutine(Fase1());
        }
    }

    private IEnumerator Fase1()
    {
        if (!Fase1Cooldown)
        {
            Fase1Cooldown = true;
            spawners.SpawnTroop(Spawners.TroopsAvaiable.AntSoldier, enemyCastle);
            yield return new WaitForSecondsRealtime(12.0f);
            Fase1Cooldown = false;
        }
    }
}
