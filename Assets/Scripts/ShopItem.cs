using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Sprite itemImage;
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite backroundImage;
    [SerializeField]
    private int price;
    [SerializeField]
    private Sprite currencyImage;

    
    // Start is called before the first frame update
    void Awake()
    {
        GameObject.Find("powerUpImage").GetComponent<Image>().sprite = itemImage;
        GameObject.Find("currencyImage").GetComponent<Image>().sprite = currencyImage;
        GameObject.Find("priceText").GetComponent<TextMeshPro>().text = price.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
