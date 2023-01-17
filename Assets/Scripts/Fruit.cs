using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public enum FruitType
    {
        Apple,
        Banana,
        Watermelon,
        Pumpkin
    }
    //*************************************************************************
    //*************************************************************************
    // Atributes
    //*************************************************************************
    //*************************************************************************
    public FruitType fruitType;
    public bool isApple;
    private Vector3 horizontalDirection;
    private Vector3 verticalDirection;
    private bool rotateClockwise;
    private bool grow;
    private float speedH;
    private float speedV;
    private float speedR;
    private float speedS;
    private bool alreadyFlickering;
    //*************************************************************************
    //*************************************************************************

    //*************************************************************************
    //*************************************************************************
    // Unity Methods
    //*************************************************************************
    //*************************************************************************
    void Start()
    {
        horizontalDirection = Random.Range(0,2) == 0 ? Vector3.right : Vector3.left;
        verticalDirection = Random.Range(0,2) == 0 ? Vector3.up : Vector3.down;
        rotateClockwise = Random.Range(0,2) == 0 ? true : false;
        alreadyFlickering = false;

        speedH = Random.Range(1.5f, 2.5f);
        speedV = Random.Range(1.5f, 2.5f);
        speedR = 7f;
        speedS = 3f;

        SetNewColor();
        StopAllCoroutines(); 
    }

    void FixedUpdate()
    {
        if (DifficultyManager.Instance.CurrentStatus.IsMovingHorizontally)
        {
            HorizontalMoveMode();
        }
        if (DifficultyManager.Instance.CurrentStatus.IsMovingVertically)
        {
            VerticalMoveMode();
        }
        if (DifficultyManager.Instance.CurrentStatus.IsRotating)
        {
            RotateMode();
        }
        if (DifficultyManager.Instance.CurrentStatus.IsScaling)
        {
            ScaleMode();
        }
        if (DifficultyManager.Instance.CurrentStatus.IsFlickering && !alreadyFlickering)
        {
            alreadyFlickering = true;
            StartCoroutine("FlickerMode");
        }
        else if(!DifficultyManager.Instance.CurrentStatus.IsFlickering && alreadyFlickering){
            alreadyFlickering = false;
            StopCoroutine("FlickerMode");
            ShowApple();
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "VerticalWall")
        {
            horizontalDirection *= (-1.0f);
        }

        if(col.tag == "HorizontalWall")
        {
            verticalDirection *= (-1.0f);
        }
    }

    //*************************************************************************
    //*************************************************************************

    //*************************************************************************
    //*************************************************************************
    // Mouse events
    //*************************************************************************
    //*************************************************************************

    public void OnMouseDown()
    {
        if(isApple)
        {
            GameManager.Instance.ClickOnApple();
        }
        else
        {
            GameManager.Instance.ClickOnFruits();
        }
        
    }

    //*************************************************************************
    //*************************************************************************

    //*************************************************************************
    //*************************************************************************
    // Class methods
    //*************************************************************************
    //*************************************************************************

    private void ShowFruit()
    {
        GetComponent<CanvasRenderer>().SetAlpha(1f);
    }

    private void HideFruit()
    {
        GetComponent<CanvasRenderer>().SetAlpha(0f);
    }

    private void ShowApple(){
        if(isApple)
            GetComponent<CanvasRenderer>().SetAlpha(1f);
    }

    //*************************************************************************
    //*************************************************************************

    //*************************************************************************
    //*************************************************************************
    // Difficulty methods
    //*************************************************************************
    //*************************************************************************

    public IEnumerator FlickerMode()
    {
        for(;;)
        {
            float rand = Random.Range(0.0f, 1.0f);
            yield return new WaitForSeconds(rand);
            HideFruit();
            rand = Random.Range(0.0f, 1.0f);
            yield return new WaitForSeconds(rand);
            ShowFruit();
        }
    }

    public void HorizontalMoveMode()
    {
        Vector3 position = transform.position;
        position += horizontalDirection * speedH * Time.deltaTime;
        transform.position = position;
    }

    public void VerticalMoveMode()
    {
        Vector3 position = transform.position;
        position += verticalDirection * speedV * Time.deltaTime;
        transform.position = position;
    }

    public void RotateMode()
    {
        if (rotateClockwise)
        {
            transform.Rotate(Vector3.back * speedR);
        }
        else
        {
            transform.Rotate(Vector3.forward * speedR);
        }
    }

    public void SetNewColor()
    {
        gameObject.GetComponent<CanvasRenderer>().SetColor(DifficultyManager.Instance.CurrentStatus.CurrentColor);
    }

    public void SetColorWhite()
    {
        gameObject.GetComponent<CanvasRenderer>().SetColor(Color.white);
    }

    public void ScaleMode()
    {
        Vector3 scaleChange = new Vector3(0.01f, 0.01f, 0f);
        if (grow)
        {
            scaleChange *= speedS;
            Vector3 target = new Vector3(1f, 1f, transform.localScale.z);

            // move sprite towards the target location
            transform.localScale += scaleChange;
            if (transform.localScale.x >= target.x)
            {
                grow = false;
            }
        }
        else
        {
            scaleChange *= speedS * -1f;
            Vector3 target = new Vector3(0.2f, 0.2f, transform.localScale.z);

            // move sprite towards the target location
            transform.localScale += scaleChange;
            if (transform.localScale.x <= target.x)
            {
                grow = true;
            }
        }
    }
    //*************************************************************************
    //*************************************************************************
}
