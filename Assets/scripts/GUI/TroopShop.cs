using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopShop : MonoBehaviour
{
    bool moneyAmountCooldown = false;
    private double food = 0;
    Transform ownCastle;
    Spawners spawners;
    CastleUpgrades castleUpgrades;
    public Text foodAmountText;
    public Text foodMultiplierCost;

    private void Start()
    {
        spawners = GetComponent<Spawners>();
        ownCastle = GameObject.Find("OwnCastle").transform;
        castleUpgrades = GetComponent<CastleUpgrades>();
    }

    private void Update()
    {
        StartCoroutine(TimeMoney());
        foodAmountText.text = food.ToString();
        foodMultiplierCost.text = castleUpgrades.foodMultiplierCost.ToString();
        if (UltimateButton.GetButtonDown("BuyAntSoldier") && food >= 10)
        {
            food -= 10;
            spawners.SpawnTroop(Spawners.TroopsAvaiable.AntSoldier, ownCastle);
        }
    }

    private IEnumerator TimeMoney()
    {
        if(!moneyAmountCooldown)
        {
            moneyAmountCooldown = true;
            food++;
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
