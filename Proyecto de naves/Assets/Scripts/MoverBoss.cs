using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBoss : MonoBehaviour
{
    private Rigidbody rig;
    public float speed;
    public float Timer;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        if (Timer <= 0)
        {
            rig.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rig.velocity = transform.forward * speed;
    }
}
