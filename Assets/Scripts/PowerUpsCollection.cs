using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

[System.Serializable]
public class PowerUpsCollection
{
    private int stopRotating;
    private int stopTranslation;
    private int disableColor;
    private int stopScaling;
    private int stopFlickering;

    public int GetNumPowerUps(string powerUp){
        int amount = 0;
        switch(powerUp){
            case "StopRotating":
                amount = StopRotating;
            break;
            case "StopTranslation":
                amount = StopTranslation;
            break;
            case "DisableColor":
                amount = DisableColor;
            break;
            case "StopScaling":
                amount = StopScaling;
            break;
            case "StopFlickering":
                amount = StopFlickering;
            break;
        }

        return amount;
    }

    public void AddPowerUp(string powerUp, int amountOfItems){
        switch(powerUp){
            case "StopRotating":
                Debug.Log("comprado");
                StopRotating = StopRotating + amountOfItems;
            break;
            case "StopTranslation":
                StopTranslation = StopTranslation + amountOfItems;
            break;
            case "DisableColor":
                DisableColor = DisableColor + amountOfItems;
            break;
            case "StopScaling":
                StopScaling = StopScaling + amountOfItems;
            break;
            case "StopFlickering":
                StopFlickering = StopFlickering + amountOfItems;
            break;
        }
    }


    public int StopRotating
    {
        get{ return stopRotating;}
        set{ stopRotating = value;}
    }

    public int StopFlickering
    {
        get{ return stopFlickering;}
        set{ stopFlickering = value;}
    }

    public int StopTranslation
    {
        get{ return stopTranslation;}
        set{ stopTranslation = value;}
    }

    public int DisableColor
    {
        get{ return disableColor;}
        set{ disableColor = value;}
    }

    public int StopScaling
    {
        get{ return stopScaling;}
        set{ stopScaling = value;}
    }
}
