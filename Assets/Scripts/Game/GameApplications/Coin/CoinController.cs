using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    public bool IsTriggered = false;
    public bool TimerStart = false;
    public Action<Coin, Coin> OnTrigger = delegate(Coin coin, Coin coin1) {  };

    private void OnTriggerEnter2D(Collider2D other)
    {
        Coin coin;
        if (other.TryGetComponent<Coin>(out coin))
        {
            if (coin.CurrentIndex == _coin.CurrentIndex)
            {
                OnTrigger(coin, _coin);
                IsTriggered = true;
                TimerStart = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Coin coin;

        if (other.TryGetComponent<Coin>(out coin))
        {
            if (coin.CurrentIndex == _coin.CurrentIndex)
            {
                IsTriggered = false;
                TimerStart = false;
            }
        }
    }
}
