using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitSceen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdateBoundary();
    }
    void UpdateBoundary()
    {
        Vector2 half = Utils.GetHalfDimensionsInWorldUnits() / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
