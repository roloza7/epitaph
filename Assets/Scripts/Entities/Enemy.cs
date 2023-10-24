using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{   
    public GameObject coin;
    [SerializeField] CoinValueEnum enemyCoinValue;
    private const int minNumberOfCoins = 2;
    private const int maxNumberOfCoins = 4;
    private SpriteRenderer renderer;
    private bool hasDied = false;
    protected override void Start() {
        base.Start();
        renderer = GetComponent<SpriteRenderer>();
    }
    public override void Die() {
        if (!hasDied) {
            DropCoins();
        }
        hasDied = true;
        Destroy(gameObject);
    }
    
    public override void TakeDamage(float amount) {
        base.TakeDamage(amount);
        StartCoroutine(DamageFlash());

    }
    public IEnumerator DamageFlash() {
        renderer.color = new Color(1.0f, 0.32f, 0.28f);
        yield return new WaitForSeconds(0.12f);
        renderer.color = Color.white;
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
