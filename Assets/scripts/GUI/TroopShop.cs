using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopShop : MonoBehaviour
{
    //Bandera para aumentar el conteo del dinero
    bool moneyAmountCooldown = false;

    //Variable para la comida
    private double food = 0;

    //Castillo propio, sirve como referencia para spawnear los mobs
    Transform ownCastle;
    Spawners spawners;

    //Este nos permitirá saber las mejoras del castillo y su influencia en el spawneo de los mobs
    CastleUpgrades castleUpgrades;

    //Variables visibles para mostrar cuanto hay de comida
    public Text foodAmountText;

    //Variable del costo por subir de nivel
    public Text foodMultiplierCost;

    private void Start()
    {
        spawners = GetComponent<Spawners>();
        ownCastle = GameObject.Find("OwnCastle").transform;
        castleUpgrades = GetComponent<CastleUpgrades>();
    }

    private void Update()
    {
        //Iniciamos la corutina del dinero
        StartCoroutine(TimeMoney());

        //Actualizamos el contenido del texto
        foodAmountText.text = food.ToString();
        foodMultiplierCost.text = castleUpgrades.foodMultiplierCost.ToString();

        //Si se presiona el botón #1 aparecerá una hormiga soldada (Esto es de prueba, hay que cambiarlo para que pueda spawnear
        //cualquier unidad desde un manager de tropas
        if (UltimateButton.GetButtonDown("BuyAntSoldier"))
        {
            //Costo de la hormiga (Obtenido desde sus stats)
            int foodCost = spawners.FindTroopInTroopList(Spawners.TroopsAvaiable.AntSoldier).GetComponent<MobStats>().GetFoodBaseCost();
            if (food >= foodCost)
            {
                TakeFood(foodCost);
                //Spawnear dicha tropa (Falta implementar el cooldown de compra)
                spawners.SpawnTroop(Spawners.TroopsAvaiable.AntSoldier, ownCastle);
            }
        }
        if (UltimateButton.GetButtonDown("BuyAntArcher"))
        {
            int foodCost = spawners.FindTroopInTroopList(Spawners.TroopsAvaiable.AntArcher).GetComponent<MobStats>().GetFoodBaseCost();
            if(food >= foodCost)
            {
                TakeFood(foodCost);
                spawners.SpawnTroop(Spawners.TroopsAvaiable.AntArcher, ownCastle);
            }
        }
    }

    //corutina para incrementar dinero, es necesario ya que de lo contrario no podría haber un control de la generación del mismo
    private IEnumerator TimeMoney()
    {
        if(!moneyAmountCooldown)
        {
            //Actiavamos bandera para evitar múltiples ejecuciones de la corutina
            moneyAmountCooldown = true;
            //Añadimos 1 de comida 
            food++;
            //Esperamos el multiplicador de comida de las mejoras del castillo
            yield return new WaitForSecondsRealtime(castleUpgrades.foodMultiplierSpeed);
            moneyAmountCooldown = false;

        }
    }

    //Getteres
    public double GetFood()
    {
        return food;
    }
    //Modifiers
    public void TakeFood(double amountFood)
    {
        food -= amountFood;
    }
}
