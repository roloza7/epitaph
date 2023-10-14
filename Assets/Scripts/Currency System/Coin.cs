using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Default coin value is SmallValue
    public int coinValue = (int) CoinValueEnum.SmallValue;

    // Colors of the different coin values
    public static readonly Color SMALL_VALUE_COIN_COLOR = Color.white;
    public static readonly Color MEDIUM_VALUE_COIN_COLOR = new Color(1f, 0.9f, 0f, 1f); // Yellow
    public static readonly Color LARGE_VALUE_COIN_COLOR = new Color(0.8f, 0.25f, 0.25f, 1f); // Red

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            var player = other.GetComponent<Player>();
            if (player != null) {
                player.CollectCoin(coinValue);
            }
             Destroy(this.gameObject);
        }
    }

    public void setCoinValue(int value) {
        coinValue = value;
    }
}

public enum CoinValueEnum
{
    SmallValue = 10,
    MediumValue = 20,
    LargeValue = 30
}