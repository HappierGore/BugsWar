using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script es para permitir ser reutilizado en diferentes niveles.
public class Spawners : MonoBehaviour
{
    //En este arreglo se tienen que colocar TODOS los mobs nuevos que se hayan creado
    public GameObject[] troopsList;

    //Y aquí enlistarlos en un ENUM para posteriormente utilizarlos al crear niveles
    public enum TroopsAvaiable { AntSoldier, AntArcher };

    //Función que permitirá spawnear un mob, recibiendo como parámetro la tropa a spawnear y el castillo donde aparecerá
    //Esto se utiliza tanto para crear niveles como en el juego para el jugador
    public void SpawnTroop(TroopsAvaiable troops, Transform castle)
    {
        //Instancía el mob (Crea uno) utilizando la función FindTroppInTroopList (visualizar abajo de esta función)
        GameObject clone = Instantiate(FindTroopInTroopList(troops), castle) as GameObject;

        //Si el castillo en donde spawnea es "Enemigo"
        if(castle.name == "EnemyCastle")
        {
            //Obten el componente "MobStats" para hacer que sea un enemigo
            MobStats cloneStats = clone.GetComponent<MobStats>();

            //Definimos al mob creado como un enemigo
            cloneStats.IsEnemy();

            //Le añadimos el tag "enemies" al mob enemigo, por defecto, ya tienen el valor aliado y aliado
            clone.tag = "enemies";
        }
    }

    //Permite encontrar la tropa seleccionada desde el ENUM dentro del arreglo de unidades disponibles, utilizado en la creación de niveles
    public GameObject FindTroopInTroopList(TroopsAvaiable troops)
    {
        //Variable temporal
        GameObject result = null;
        //Creamos un ciclo for para buscar dentro de todo el arreglo la tropa deseada (Deben tener el mismo nombre, tanto en el prefab como en el Enum
        for (int i = 0; i < troopsList.Length; i++)
        {
            //Si el objeto en la lista troppsList en la iteración i tiene el mismo nombre que el seleccionado en el enum
            if (troopsList[i].name == troops.ToString())
            {
                //el resultado será dicha tropa
                result = troopsList[i];
            }
        }
        //y retornamos el resultado para posteriormente, spawnearlo
        return result;
    }
}
