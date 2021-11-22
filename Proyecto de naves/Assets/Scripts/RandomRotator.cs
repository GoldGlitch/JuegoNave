using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public float tumble;
    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //Vector3 angularVelocity = new Vector3(Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        
       rig.angularVelocity = Random.insideUnitSphere * tumble;
    }

   
}
