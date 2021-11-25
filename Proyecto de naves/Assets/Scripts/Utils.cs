using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static Vector2 GetHalfDimensionsInWorldUnits()
    {
        float width, height;

        Camera cam = Camera.main;
        float ratio = cam.pixelWidth / (float)cam.pixelHeight;
        height = cam.orthographicSize * 2;
        width = height * ratio;



        return new Vector2(width, height) / 2f;
    }
   
}
