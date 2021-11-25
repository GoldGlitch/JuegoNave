using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManiobre : MonoBehaviour
{
    public Vector2 startWait;
    public float dodge;
    private Rigidbody rb;
    public float smoothing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private float targetManeuver;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Evade());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(targetManeuver, 0.0f, rb.velocity.z);
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        targetManeuver = Random.Range(1, dodge); 
    }
}
