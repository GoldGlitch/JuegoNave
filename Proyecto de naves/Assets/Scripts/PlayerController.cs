using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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
    private AudioSource audioSource;
    private Rigidbody rig;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateBoundary();
    }
    void UpdateBoundary()
    {
        Vector2 half = Utils.GetHalfDimensionsInWorldUnits();
      
        boundary.xMin = -half.x + 0.9f;
        boundary.xMax = half.x - 0.9f;
        boundary.zMin = -half.y + 8f;
        boundary.zMax = half.y - 2f;

        
    }
    private void FixedUpdate()
    {
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rig.velocity = movement * speed;
        rig.position = new Vector3(Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax), 0f, Mathf.Clamp(rig.position.z, boundary.zMin, boundary.zMax));
        rig.rotation = Quaternion.Euler(-90f, 0, rig.velocity.x * tilt);
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, ShotSpawm.position, Quaternion.identity);
            audioSource.Play();
        }
    }
}
