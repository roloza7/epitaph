using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{   
    public GameObject coin;
    [SerializeField] CoinValueEnum enemyCoinValue;
    private const int minNumberOfCoins = 2;
    private const int maxNumberOfCoins = 4;

    private bool hasDied = false;
    
    public override void Die() {
        if (!hasDied) {
            DropCoins();
        }
        hasDied = true;
        Destroy(gameObject);
    }

    public void DropCoins() {
        int numberOfCoins = (int) ((Random.value * (maxNumberOfCoins - minNumberOfCoins + 1)) + minNumberOfCoins);
        
        GameObject[] copyCoins = new GameObject[numberOfCoins];
        
        for (int i = 0; i < numberOfCoins; i++) {
            copyCoins[i] = Instantiate(coin) as GameObject;
            copyCoins[i].transform.position = (Vector2) this.transform.position + Random.insideUnitCircle / 2;
            copyCoins[i].GetComponent<Coin>().setCoinValue((int)enemyCoinValue);

            switch (enemyCoinValue) {
                default:
                case CoinValueEnum.SmallValue:
                    copyCoins[i].GetComponent<SpriteRenderer>().color = Coin.SMALL_VALUE_COIN_COLOR;
                    break;
                case CoinValueEnum.MediumValue:
                    copyCoins[i].GetComponent<SpriteRenderer>().color = Coin.MEDIUM_VALUE_COIN_COLOR;
                    break;
                case CoinValueEnum.LargeValue:
                    copyCoins[i].GetComponent<SpriteRenderer>().color = Coin.LARGE_VALUE_COIN_COLOR;
                    break;
            }
        }
    }

}
