using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public Action OnTriggered = delegate {  };

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<CoinController>() != null)
        {
            OnTriggered();
        }
    }
}
