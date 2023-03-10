using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FruitsGridConfig : MonoBehaviour
{
    //*************************************************************************
    //*************************************************************************
    // Atributes
    //*************************************************************************
    //*************************************************************************
    private GridLayoutGroup grid;
    private float screenWidth;
    private float gridPadding;
    private float gridSpacing;
    private float gridCellSize;
    //*************************************************************************
    //*************************************************************************

    //*************************************************************************
    //*************************************************************************
    // Unity Methods
    //*************************************************************************
    //*************************************************************************
    void Awake()
    {
        grid = this.gameObject.GetComponent<GridLayoutGroup>();
        screenWidth = 650;
        gridPadding = grid.padding.left;
        gridSpacing = grid.spacing.x;
    }

    //*************************************************************************
    //*************************************************************************

    //*************************************************************************
    //*************************************************************************
    // Class methods
    //*************************************************************************
    //*************************************************************************
    public void UpdateGridConfig(int numRow)
    {
        gridCellSize = (screenWidth - 2 * gridPadding - (numRow-1) * gridSpacing)/numRow;
        grid.cellSize = new Vector2(gridCellSize, gridCellSize);
    }

    //*************************************************************************
    //*************************************************************************
}
