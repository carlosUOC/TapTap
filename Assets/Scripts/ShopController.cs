using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[Serializable]
public class ShopController : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup itemsList;
    private static ShopController instance = null;
    public static ShopController Instance {get{ return instance ;}}
    [SerializeField]
    //private ShopItem [] items;
    private GameObject [] items;
    public Transform gridTransform;
    private int amountOfItems;
    private int initialProductPrice;
    private int lastProductPrice;
    public Button buyItemButton;
    private string powerUp;

    void Awake()
    {
        CreateSingleton();
        //foreach(ShopItem item in items){
        foreach(GameObject item in items){
            Instantiate(item, gridTransform);
        }
        itemsList = gridTransform.gameObject.GetComponent<GridLayoutGroup>();
        amountOfItems = 1;
        buyItemButton.onClick.AddListener(BuyItem);

    }

    private void OnEnable() {
        GameObject.Find("playerCollectedApples/amountText").gameObject.GetComponent<TextMeshProUGUI>().text = PlayerDataManager.Instance.PlayerData.ApplesCollected.ToString();
    }

    private void CreateSingleton()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetPopUpdata(){
        amountOfItems = 1;
        GameObject.Find("buyItemPopUp/amount").gameObject.GetComponent<TextMeshProUGUI>().text = amountOfItems.ToString();
        initialProductPrice = Int32.Parse(EventSystem.current.currentSelectedGameObject.transform.Find("price/priceText").gameObject.GetComponent<TextMeshProUGUI>().text);
        GameObject.Find("buyItemPopUp/price/priceText").gameObject.GetComponent<TextMeshProUGUI>().text = initialProductPrice.ToString();
        GameObject.Find("buyItemPopUp/PUDescription").gameObject.GetComponent<TextMeshProUGUI>().text = EventSystem.current.currentSelectedGameObject.transform.Find("Text").gameObject.GetComponent<Text>().text;
        lastProductPrice = initialProductPrice;
        Sprite itemImage = EventSystem.current.currentSelectedGameObject.transform.Find("PUImage").gameObject.GetComponent<Image>().sprite;
        GameObject.Find("buyItemPopUp/ItemToBuyImage").gameObject.GetComponent<Image>().sprite = itemImage;

        if(EventSystem.current.currentSelectedGameObject.transform.ToString().Contains("Flickering")){
            powerUp = "StopFlickering";
        }
        else if(EventSystem.current.currentSelectedGameObject.transform.ToString().Contains("Color")){
            powerUp = "DisableColor";
        }
        else if(EventSystem.current.currentSelectedGameObject.transform.ToString().Contains("Rotation")){
            powerUp = "StopRotating";
        }
        else if(EventSystem.current.currentSelectedGameObject.transform.ToString().Contains("Scaling")){
            powerUp = "StopScaling";
        }
        else if(EventSystem.current.currentSelectedGameObject.transform.ToString().Contains("Traslation")){
            powerUp = "StopTranslation";
        }
    }

    public void IncreaseAmount(){
        if(PlayerDataManager.Instance.PlayerData.ApplesCollected < (lastProductPrice+initialProductPrice)){
            StartCoroutine("CantBuyMoreItemsAnimation");
        }
        else{
            amountOfItems++;
            GameObject.Find("buyItemPopUp/amount").gameObject.GetComponent<TextMeshProUGUI>().text = amountOfItems.ToString();
            lastProductPrice = lastProductPrice + initialProductPrice;
            GameObject.Find("buyItemPopUp/price/priceText").gameObject.GetComponent<TextMeshProUGUI>().text = lastProductPrice.ToString();
        }
        
    }

    public void DecreaseAmount(){
        if(amountOfItems == 1){
            StartCoroutine("CantBuyMoreItemsAnimation");
        }
        else{
            amountOfItems--;
            GameObject.Find("buyItemPopUp/amount").gameObject.GetComponent<TextMeshProUGUI>().text = amountOfItems.ToString();
            lastProductPrice = lastProductPrice - initialProductPrice;
            GameObject.Find("buyItemPopUp/price/priceText").gameObject.GetComponent<TextMeshProUGUI>().text = lastProductPrice.ToString();
        }
    }

    public void BuyItem(){
        if(PlayerDataManager.Instance.PlayerData.ApplesCollected < lastProductPrice){
            StartCoroutine("CantBuyMoreItemsAnimation");
        }
        else{
            PlayerDataManager.Instance.PlayerData.PowerUpsCollection.AddPowerUp(powerUp, amountOfItems);
            PlayerDataManager.Instance.PlayerData.ApplesCollected -= lastProductPrice;
            GameObject.Find("playerCollectedApples/amountText").gameObject.GetComponent<TextMeshProUGUI>().text = PlayerDataManager.Instance.PlayerData.ApplesCollected.ToString();
            // passing null argument because apples are updated in this method by using directly PlayerDataManager
            SaveSystem.SaveGame(null);
        }

    }

    private IEnumerator CantBuyMoreItemsAnimation(){
        GameObject.Find("buyItemPopUp/price/priceText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
        GameObject.Find("buyItemPopUp/amount").gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
        GameObject.Find("playerCollectedApples/amountText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("buyItemPopUp/price/priceText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        GameObject.Find("buyItemPopUp/amount").gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        GameObject.Find("playerCollectedApples/amountText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
    }

}
