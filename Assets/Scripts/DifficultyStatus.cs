using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyStatus
{

    private bool isRotating;
    private bool isScaling;
    private bool isFlickering;
    private bool isSameRandomColor;
    private bool isRandomColor;
    private bool isMovingHorizontally;
    private bool isMovingVertically;
    private Color currentColor;

    public DifficultyStatus(){
        InitializeSettings();
    }

    public void InitializeSettings()
    {
        isRotating = false;
        isScaling = false;
        isFlickering = false;
        isSameRandomColor = false;
        isRandomColor = false;
        isMovingHorizontally = false;
        isMovingVertically = false;
        currentColor = Color.white;
    }
    
    public bool IsRotating
    {
        get{ return isRotating;}
        set{ isRotating = value;}
    }

    public bool IsScaling
    {
        get{ return isScaling;}
        set{ isScaling = value;}
    }

    public bool IsFlickering
    {
        get{ return isFlickering;}
        set{ isFlickering = value;}
    }

    public bool IsSameRandomColor
    {
        get{ return isSameRandomColor;}
        set{ isSameRandomColor = value;}
    }

    public bool IsRandomColor
    {
        get{ return isRandomColor;}
        set{ isRandomColor = value;}
    }

    public bool IsMovingHorizontally
    {
        get{ return isMovingHorizontally;}
        set{ isMovingHorizontally = value;}
    }

    public bool IsMovingVertically
    {
        get{ return isMovingVertically;}
        set{ isMovingVertically = value;}
    }

    public Color CurrentColor
    {
        get
        {
            if (IsRandomColor)
            {
                int i = Random.Range(0, ColorsList.colors.Length);
                return ColorsList.colors[i];
            }
            
            return currentColor;
        }
        set{ currentColor = value;}
    }
}
