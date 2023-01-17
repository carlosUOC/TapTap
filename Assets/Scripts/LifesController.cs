using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifesController : MonoBehaviour
{
    private const int MAX_NUM_LIFES = 10;

    private int currentLifes;

    public Text lifesText;
    // Start is called before the first frame update
    void Start()
    {
        currentLifes = MAX_NUM_LIFES;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void IncreaseLifes(int n)
    {
        currentLifes = Mathf.Clamp(currentLifes+n, 0, MAX_NUM_LIFES);
        UpdateLifesText();
    }

    public void DecreaseLifes(int n)
    {
        currentLifes = Mathf.Clamp(currentLifes-n, 0, MAX_NUM_LIFES);
        UpdateLifesText();
    }

    public void UpdateLifesText()
    {
        lifesText.text = currentLifes.ToString();
    }
}
