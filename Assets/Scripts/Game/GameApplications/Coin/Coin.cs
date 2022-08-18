using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] public int CurrentIndex;
    [SerializeField] public CoinController CoinController;
    [SerializeField] public SpriteRenderer SpriteRenderer;
    [SerializeField] public int ScoreNumber;

    private void OnCollisionEnter2D(Collision2D col)
    {
        CoinController.gameObject.SetActive(true);
    }
}
