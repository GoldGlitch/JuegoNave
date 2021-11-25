using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    // Start is called before the first frame update
    public float ScrollSpeed;

    private Vector3 startPosition;
    private float tileSize;

    void Start()
    {
        startPosition = transform.position;
        tileSize = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * ScrollSpeed,tileSize);
        transform.position = startPosition + Vector3.forward * newPosition;

    }
}
