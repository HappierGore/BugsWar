using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    //Nivel 1
    Spawners spawners;
    Transform enemyCastle;
    bool Fase1Cooldown = false;
    bool runFase1 = true;
    [SerializeField] float fase1Mob1Cooldown = 12.0f;
    private void Start()
    {
        enemyCastle = GameObject.Find("EnemyCastle").transform;
        spawners = GameObject.Find("Manager").GetComponent<Spawners>();
    }
    void Update()
    {
        //Si el castillo enemigo esta activo (Si esta desactivado quiere decir que ha sido destruido y por lo tanto, el jugador gana)
        if(enemyCastle.gameObject.activeSelf)
        {
            //Mientras la fase 1 esté en juego, iniciará la corutina de la fase 1
            if (runFase1)
                StartCoroutine(Fase1Mob1());
        }
    }

    //Fase 1, al igual que con los ataques y otros similares, se hace corutina para evitar múltiples ejecuciones de golpe
    private IEnumerator Fase1Mob1()
    {
        //Fase1Cooldown es el tiempo de espera para spawnear cada tropa (Bandera)
        if (!Fase1Cooldown)
        {
            //Activamos bandera para evitar múltiples ejecuciones
            Fase1Cooldown = true;

            //Ejecutamos función definida dentro del script "Spawners" para spawnear una tropa
            spawners.SpawnTroop(Spawners.TroopsAvaiable.AntSoldier, enemyCastle);

            //Esperamos el tiempo definido de spawn por mob
            yield return new WaitForSecondsRealtime(fase1Mob1Cooldown);

            //Reiniciamos la bandera a su valor original
            Fase1Cooldown = false;
        }
    }
}
