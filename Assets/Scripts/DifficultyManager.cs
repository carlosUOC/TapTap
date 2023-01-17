using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private static DifficultyManager instance = null;
    private DifficultyStatus currentStatus;
    private DifficultyStatus previousStatus;
    public static DifficultyManager Instance {get{ return instance ;}}

    public DifficultyStatus CurrentStatus
    {
        get{ return currentStatus;}
        set{ currentStatus = value;}
    }

    public DifficultyStatus PreviousStatus
    {
        get{ return previousStatus;}
        set{ previousStatus = value;}
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

    public void Awake()
    {
        currentStatus = new DifficultyStatus();
        previousStatus = new DifficultyStatus();
        CreateSingleton();
    }

    public void RestartDifficultySettings(){
        currentStatus.InitializeSettings();
        previousStatus.InitializeSettings();
    }

    public void SetStatusToPrevious(){
        currentStatus.IsRotating = previousStatus.IsRotating;
        currentStatus.IsScaling = previousStatus.IsScaling;
        currentStatus.IsFlickering = previousStatus.IsFlickering;
        currentStatus.IsSameRandomColor = previousStatus.IsSameRandomColor;
        currentStatus.IsRandomColor = previousStatus.IsRandomColor;
        currentStatus.IsMovingHorizontally = previousStatus.IsMovingHorizontally;
        currentStatus.IsMovingVertically = previousStatus.IsMovingVertically;
        currentStatus.CurrentColor = previousStatus.CurrentColor;
    }

    public void SaveCurrentStatus(){
        previousStatus.IsRotating = currentStatus.IsRotating;
        previousStatus.IsScaling = currentStatus.IsScaling;
        previousStatus.IsFlickering = currentStatus.IsFlickering;
        previousStatus.IsSameRandomColor = currentStatus.IsSameRandomColor;
        previousStatus.IsRandomColor = currentStatus.IsRandomColor;
        previousStatus.IsMovingHorizontally = currentStatus.IsMovingHorizontally;
        previousStatus.IsMovingVertically = currentStatus.IsMovingVertically;
        previousStatus.CurrentColor = currentStatus.CurrentColor;
    }
}
