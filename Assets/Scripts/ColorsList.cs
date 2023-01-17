﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsList : MonoBehaviour
{

    public static Color Black = new Color(0.0f, 0.0f, 0.0f);
    public static Color Dark = new Color(0.25f, 0.25f, 0.25f);
    public static Color White = new Color(1.0f, 1.0f, 1.0f);
    public static Color Red = new Color(1.0f, 0.341f, 0.2f);
    public static Color Green = new Color(0.125f, 0.65f, 0.294f);
    public static Color Blue = new Color(0.2f, 0.4f, 1.0f,1f);
    public static Color Pink = new Color(1f, 0.41f, 0.7f); 
    // public Color Burgundy = new Color("#6D2E46");
    // public Color Brown = new Color("#7C6354");
    // public Color Purple = new Color("#6761A8");
    // public Color Olive = new Color("#3B3923");

    // Range of values for each RGB color is not from 0 to 225 but from 0 to 1
    public static Color [] colors = {
        Red, 
        Green,
        Blue,
        Pink,
        Dark
    };

}
