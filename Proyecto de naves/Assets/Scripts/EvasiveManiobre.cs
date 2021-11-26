using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManiobre : MonoBehaviour
{
    public Vector2 startWait;
    public float dodge;
    private Rigidbody rb;
    public float smoothing;
    public Vector2 ManeuverTime;
    public Vector2 ManeuverWait;
    public Boundary boundary;
    public float tilt;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private float targetManeuver;
    // Start is called before the first frame update
    void Start()
    {
        UpdateBoundary();
        StartCoroutine(Evade());
    }
    void UpdateBoundary()
    {
        Vector2 half = Utils.GetHalfDimensionsInWorldUnits();

        boundary.xMin = -half.x + 0.9f;
        boundary.xMax = half.x - 0.9f;
        boundary.zMin = -half.y + 8f;
        boundary.zMax = half.y - 2f;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(targetManeuver, 0.0f, rb.velocity.z);
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0f, rb.position.z);
        rb.rotation = Quaternion.Euler(0f, 0, rb.velocity.x * -tilt);
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {

            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);

            yield return new WaitForSeconds(Random.Range(ManeuverTime.x, ManeuverTime.y));

            targetManeuver = 0;

            yield return new WaitForSeconds(Random.Range(ManeuverWait.x, ManeuverWait.y));
        }
    }
}
