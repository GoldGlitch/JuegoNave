using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{

    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float tilt;
    public Boundary boundary;

    [Header("Shot")]
    public GameObject shot;
    public Transform ShotSpawm;
    public float fireRate;
    private float nextFire;
   

    private Rigidbody rig;
   
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rig.velocity = movement * speed;
        rig.position = new Vector3(Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax), 0f, Mathf.Clamp(rig.position.z, boundary.zMin, boundary.zMax));
        rig.rotation = Quaternion.Euler(-90f, 0, rig.velocity.x * tilt);
    }

    void Start()
    {
        
    }

  
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, ShotSpawm.position, Quaternion.identity);

        }
    }
}
