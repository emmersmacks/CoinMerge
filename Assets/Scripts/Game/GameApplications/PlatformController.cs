using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    public CoinSpawner CoinSpawner;

    [SerializeField] private float MaxXMoveCoord;
    [SerializeField] private float MinXMoveCoord;

    private Camera _camera;
    private float _currentTimerTime;
    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePos = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 0));
            if(mousePos.x < MaxXMoveCoord && mousePos.x > MinXMoveCoord)
                transform.position = new Vector3(mousePos.x, transform.position.y, 0);
            
            if (mousePos.x > MaxXMoveCoord)
                transform.position = new Vector3(MaxXMoveCoord, transform.position.y, 0);
            if (mousePos.x < MinXMoveCoord)
                transform.position = new Vector3(MinXMoveCoord, transform.position.y, 0);
        }

        if (_currentTimerTime <= 0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                CoinSpawner.Spawn(transform.position);
                _currentTimerTime = 0.5f;
            }
        }
        else
        {
            _currentTimerTime -= Time.deltaTime;
        }
        
    }
    
}
