using System;
using System.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.Services;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : IService
{
    private CoinList _allCoinList;
    private CoinList _startCoinsList;
    private SpriteRenderer _platformSprite;
    
    private Coin _defaultCoin;
    public GameObject Platform;

    public Action<int> OnSplit = delegate(int i) {Debug.Log("AAAAA");  };

    public CoinSpawner()
    {
        _allCoinList = AssetProvider.GetObject<CoinList>(AssetPath.AllCoinPath);
        _startCoinsList = AssetProvider.GetObject<CoinList>(AssetPath.StartCoinPath);
        var platform = AssetProvider.Instantiate(AssetPath.PlatformPath, new Vector3(0, 3));
        Platform = platform;
        platform.GetComponent<PlatformController>().CoinSpawner = this;
        _platformSprite = platform.GetComponent<PlatformView>().PlatformSprite;
        _defaultCoin = _startCoinsList.CoinsList[0];
    }

    public void Spawn(Vector3 position)
    {
        var coin = GameObject.Instantiate(_defaultCoin.gameObject, position, Quaternion.identity).GetComponent<Coin>().CoinController;
        coin.OnTrigger += Split;
        _defaultCoin = _startCoinsList.CoinsList[Random.Range(0, _startCoinsList.CoinsList.Count)];
        _platformSprite.sprite = _defaultCoin.SpriteRenderer.sprite;
        _platformSprite.transform.localScale = _defaultCoin.SpriteRenderer.transform.localScale;
        _platformSprite.transform.parent.parent.localScale = _defaultCoin.transform.localScale;
    }
    

    public void Split(Coin firstCoin, Coin secondCoin)
    {
        if (!firstCoin.CoinController.IsTriggered)
        {
            if (firstCoin != null && secondCoin != null)
            {
                var coinObj = GameObject.Instantiate(_allCoinList.CoinsList[firstCoin.CurrentIndex + 1].gameObject, firstCoin.transform.position,
                    Quaternion.identity);
                coinObj.GetComponent<Coin>().CoinController.OnTrigger += Split;
                GameObject.Destroy(firstCoin.gameObject);
                GameObject.Destroy(secondCoin.gameObject);
                OnSplit(firstCoin.ScoreNumber);
            }
            
        }
    }
}