using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using System.Threading.Tasks;

public class PowerUpsManager : MonoBehaviour
{
    private static PowerUpsManager instance = null;
    public static PowerUpsManager Instance {get{ return instance ;}}
	private DifficultyManager diff;
    private Button powerUpButton;
    private GridLayoutGroup powerUpsList;
    public Transform gridTransform;
    
    private Image backgroundFeedbackImage;

    private Queue<Image> enabledBackgroundFeedbackImages = new Queue<Image>();

    // Start is called before the first frame update
    void Awake()
    {
        CreateSingleton();
        diff = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>();
        powerUpsList = gridTransform.gameObject.GetComponent<GridLayoutGroup>();
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

    public void UpdatePowerUpsOnPlayGame(){
        foreach(var powerUp in PlayerDataManager.Instance.PlayerData.PowerUpsCollection.GetType().GetProperties()){
            int amount = Convert.ToInt32(powerUp.GetValue(PlayerDataManager.Instance.PlayerData.PowerUpsCollection,null));
            if(amount > 0){
                GameObject newPowerUp = GameObject.Find("PowerUpsButtons/"+powerUp.Name+"PowerUpButton");
                newPowerUp.gameObject.GetComponent<Button>().transform.Find("amountText").gameObject.GetComponent<TextMeshProUGUI>().text = amount.ToString();
                Instantiate(newPowerUp, gridTransform);
            }   
        }
    }

    public void ClearPowerUpsGridLayout(){
        foreach (Transform child in gridTransform.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void ApplyPowerUp(string powerUpName){
        Debug.Log(PlayerDataManager.Instance.PlayerData.PowerUpsCollection.GetNumPowerUps(powerUpName));
        if(PlayerDataManager.Instance.PlayerData.PowerUpsCollection.GetNumPowerUps(powerUpName) > 0)
        {
            powerUpButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            backgroundFeedbackImage = powerUpButton.GetComponent<Image>();
            powerUpButton.transform.Find("amountText").gameObject.GetComponent<TextMeshProUGUI>().text = (Int32.Parse(powerUpButton.transform.Find("amountText").gameObject.GetComponent<TextMeshProUGUI>().text)-1).ToString();
            StartCoroutine(powerUpName);
        }
    }

    private IEnumerator StopRotating(){
      PlayerDataManager.Instance.PlayerData.PowerUpsCollection.StopRotating--;

      if(diff.CurrentStatus.IsRotating){
          backgroundFeedbackImage.enabled = true;
          enabledBackgroundFeedbackImages.Enqueue(backgroundFeedbackImage);
          diff.CurrentStatus.IsRotating = false;
          yield return new WaitForSeconds(2.0f);
          diff.CurrentStatus.IsRotating = true;
          enabledBackgroundFeedbackImages.Dequeue().enabled = false;
      }
      else{
          Debug.Log("powerUp no aplicable en nivel actual");
          //realimentacion visual a usuario
      } 
    }

    private IEnumerator StopTranslation(){
        PlayerDataManager.Instance.PlayerData.PowerUpsCollection.StopTranslation--;
        Boolean movementWasHorizontal = false;
        Boolean movementWasVertical = false;
        if(diff.CurrentStatus.IsMovingHorizontally)
            movementWasHorizontal = true;
        if(diff.CurrentStatus.IsMovingVertically)
            movementWasVertical = true;

        if(movementWasHorizontal || movementWasVertical){
            backgroundFeedbackImage.enabled = true;
            enabledBackgroundFeedbackImages.Enqueue(backgroundFeedbackImage);
            diff.CurrentStatus.IsMovingHorizontally = false;
            diff.CurrentStatus.IsMovingVertically = false;
            yield return new WaitForSeconds(2.0f);
            if(movementWasHorizontal)
                diff.CurrentStatus.IsMovingHorizontally = true;
            if(movementWasVertical)
                diff.CurrentStatus.IsMovingVertically = true;
            enabledBackgroundFeedbackImages.Dequeue().enabled = false;
        }
        else{
          Debug.Log("powerUp no aplicable en nivel actual");
          //realimentacion visual a usuario
        } 
    }

    private IEnumerator StopFlickering(){
        PlayerDataManager.Instance.PlayerData.PowerUpsCollection.StopFlickering--;
        if(diff.CurrentStatus.IsFlickering){
          backgroundFeedbackImage.enabled = true;
          enabledBackgroundFeedbackImages.Enqueue(backgroundFeedbackImage);
          diff.CurrentStatus.IsFlickering = false;
          yield return new WaitForSeconds(2.0f);
          diff.CurrentStatus.IsFlickering = true;
          enabledBackgroundFeedbackImages.Dequeue().enabled = false;
      }
      else{
          Debug.Log("powerUp no aplicable en nivel actual");
          //realimentacion visual a usuario
      } 
    }

    private IEnumerator StopScaling(){
        PlayerDataManager.Instance.PlayerData.PowerUpsCollection.StopScaling--;
      if(diff.CurrentStatus.IsScaling){
          backgroundFeedbackImage.enabled = true;
          enabledBackgroundFeedbackImages.Enqueue(backgroundFeedbackImage);
          diff.CurrentStatus.IsScaling = false;
          yield return new WaitForSeconds(2.0f);
          diff.CurrentStatus.IsScaling = true;
          enabledBackgroundFeedbackImages.Dequeue().enabled = false;
      }
      else{
          Debug.Log("powerUp no aplicable en nivel actual");
          //realimentacion visual a usuario
      } 
    }

    private IEnumerator DisableColor(){
        PlayerDataManager.Instance.PlayerData.PowerUpsCollection.DisableColor--;
        if(diff.CurrentStatus.CurrentColor != Color.white){
            backgroundFeedbackImage.enabled = true;
            enabledBackgroundFeedbackImages.Enqueue(backgroundFeedbackImage);
            LevelController.Instance.ChangeColor(true);
            yield return new WaitForSeconds(2.0f);
            LevelController.Instance.ChangeColor(false);
            enabledBackgroundFeedbackImages.Dequeue().enabled = false;
        }
        else{
            Debug.Log("powerUp no aplicable en nivel actual");
          //realimentacion visual a usuario
        }
    }

    public void DisablePowerUps(){
        StopAllCoroutines();
       if(powerUpButton != null){
           while(enabledBackgroundFeedbackImages.Count > 0){
               enabledBackgroundFeedbackImages.Dequeue().enabled = false;
           }
       }
        DifficultyManager.Instance.SetStatusToPrevious();
    }

}
