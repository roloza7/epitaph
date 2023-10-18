using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [SerializeField] private bool killable;
    private int _currencyTotal;
    public int CurrencyTotal => _currencyTotal;
    protected override void Start() {
        base.Start();
        _health.maxValue = 100;
        DontDestroyOnLoad(this);
    }
    public override void Die() {
        if (killable) {
            Destroy(gameObject);
            SceneManager.LoadScene("DeathScene");
        }
    }
    public void CollectCoin(int coinValue) {
        _currencyTotal += coinValue;
    }
    public void SpendCoin(int coinValue)
    {
        _currencyTotal -= coinValue;
    }
}
