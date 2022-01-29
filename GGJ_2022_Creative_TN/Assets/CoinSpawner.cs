using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Reference")] 
    public GameObject coinObject;

    [Header("Reference")] 
    public int amountToSpawn;
    public float spacing;

    private float _startPosition;

    [ContextMenu("Load Coins")]
    public void LoadCoins()
    {
        Debug.Log("Load Coins");
        _startPosition = transform.position.x;
        float startAmount = 0;
        float originalSpacing = spacing;

        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject go = Instantiate(coinObject,
                new Vector3(_startPosition + startAmount, transform.position.y, 0), Quaternion.identity);
            go.transform.SetParent(this.transform);
            startAmount += originalSpacing;
        }
    }
    
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
