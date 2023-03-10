using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
public class LevelController : MonoBehaviour
{
    //*************************************************************************
    //*************************************************************************
    // Atributes
    //*************************************************************************
    //*************************************************************************
    private static LevelController instance = null;
    public static LevelController Instance {get{ return instance ;}}

    public int level = 0;
    public TextMeshProUGUI levelText;
    public GameObject apple;
    public GameObject [] fruits;
    public Transform grid;
    private FruitsGridConfig gridConfig;
    private int numRow;
    private DifficultyManager diff;
    
    //*************************************************************************
    //*************************************************************************

    //*************************************************************************
    //*************************************************************************
    // Unity Methods
    //*************************************************************************
    //*************************************************************************

    void Awake() {
        CreateSingleton();
        diff = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>();
        gridConfig = grid.gameObject.GetComponent<FruitsGridConfig>();
    }

    void Start()
    {
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
    //*************************************************************************
    //*************************************************************************

    //*************************************************************************
    //*************************************************************************
    // Class methods
    //*************************************************************************
    //*************************************************************************
    public void StartGame()
    {
        level = 0;
        diff.RestartDifficultySettings();
        NextLevel();
    }
    
    public void NextLevel()
    {
        PowerUpsManager.Instance.DisablePowerUps();
        level += 1;
        levelText.text = level.ToString();
        CalculateNumRow();
        gridConfig.UpdateGridConfig(numRow);
        DifficultyLevelSettings();
        diff.SaveCurrentStatus();
        GenerateFruits();
    }

    void GenerateFruits()
    {
        // Delete all childs in grid
        foreach (Transform child in grid) {
            GameObject.Destroy(child.gameObject);
        }

        // Set a position for apple
        int applePosition = Random.Range(0, numRow*numRow);

        // Generate instances of all fruits (including apple)
        for(int i = 0; i < numRow*numRow; i++)
        {
            if(i != applePosition)
            {
                Instantiate(GetAFruit(), grid);
            }
            else
            {
                Instantiate(GetApple(), grid);
            }
        }
    }

    private void CalculateNumRow()
    {
        if (level > 0 && level <= 5)
        {
            numRow = 2;
        }
        else if (level > 5 && level <= 10)
        {
            numRow = 3;
        }
        else 
        {
            numRow = level/10 + 2;
        }
    }

    private void ResetDifficulties(){
        diff.CurrentStatus.IsSameRandomColor = false;
        diff.CurrentStatus.IsRandomColor = false;
        diff.CurrentStatus.IsScaling = false;
        diff.CurrentStatus.IsFlickering = false;
        diff.CurrentStatus.IsMovingHorizontally = false;
        diff.CurrentStatus.IsMovingVertically = false;
        diff.CurrentStatus.IsRotating = false;
        diff.CurrentStatus.IsFlickering = false;
        diff.CurrentStatus.CurrentColor = Color.white;
    }

    private void DifficultyLevelSettings()
    {

        ArrayList difficultiesForNewLevel = new ArrayList();
        PropertyInfo[] listOfAvailableDifficulties = diff.CurrentStatus.GetType().GetProperties();
        int amountOfDifficulties = 0;

        if(level>5 && level<=30)
            amountOfDifficulties = 1;
        else if(level>30 && level<65)
            amountOfDifficulties = 2;
        else if(level>64)
            amountOfDifficulties = 3;

        int i = 0;
        while(i<amountOfDifficulties){
            int randomDifficultyIdex = Random.Range(0,listOfAvailableDifficulties.Length);
            string newDifficulty = listOfAvailableDifficulties[randomDifficultyIdex].Name;
            if(!difficultiesForNewLevel.Contains(newDifficulty)){
                if(!newDifficulty.Contains("Color") || (newDifficulty.Contains("Color") && !colorDifficultyAlreadySelected(ref difficultiesForNewLevel))){
                    difficultiesForNewLevel.Add(newDifficulty);
                    i++;
                }
            }
        }

        ResetDifficulties();

        foreach(string newDifficulty in difficultiesForNewLevel){
            switch(newDifficulty){
                case "IsRotating": diff.CurrentStatus.IsRotating = true;
                break;
                case "IsFlickering": diff.CurrentStatus.IsFlickering = true;
                break;
                case "IsSameRandomColor": diff.CurrentStatus.IsSameRandomColor = true;
                break;
                case "IsRandomColor": diff.CurrentStatus.IsRandomColor = true;
                break;
                case "IsScaling": diff.CurrentStatus.IsScaling = true;
                break;
                case "IsMovingHorizontally": diff.CurrentStatus.IsMovingHorizontally = true;
                break;
                case "IsMovingVertically": diff.CurrentStatus.IsMovingVertically = true;
                break;
                case "CurrentColor": diff.CurrentStatus.CurrentColor = Color.red;
                break;
            }
        }


        if(diff.CurrentStatus.IsSameRandomColor)
        {
            int colorIndex = Random.Range(0, ColorsList.colors.Length);
            diff.CurrentStatus.CurrentColor = ColorsList.colors[colorIndex];
        }
    }
    //*************************************************************************
    //*************************************************************************

    //*************************************************************************
    //*************************************************************************
    // Getter and setter
    //*************************************************************************
    //*************************************************************************

    private bool colorDifficultyAlreadySelected(ref ArrayList difficultiesAlreadySelected){
        bool alreadySelected = false;
        foreach(string difficulty in difficultiesAlreadySelected){
            if(difficulty.Contains("Color")){
                alreadySelected = true;
            }
        }
        return alreadySelected;
    }

    private GameObject GetAFruit()
    {
        int i = Random.Range(0, fruits.Length);
        return fruits[i];
    }
    
    private GameObject GetApple()
    {
        return apple;
    }

    // Esto está aquí porque es el único elemento que conoce a todas las frutas
    // (ya que son hijas del grid).
    public void ChangeColor(bool powerUpActivated)
    {
        if(powerUpActivated)
        {
            foreach(Transform fruit in grid)
            {
                fruit.gameObject.GetComponent<Fruit>().SetColorWhite();
            }
        }
        else
        {
            foreach(Transform fruit in grid)
            {
                fruit.gameObject.GetComponent<Fruit>().SetNewColor();
            }
        }
    }

    //*************************************************************************
    //*************************************************************************
}
