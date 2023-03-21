using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform startMarker;
public Transform endMarker;

// Movement speed in units/sec.
public float speed = 1.0f;

// Time when the movement started.
private float startTime;

// Total distance between the markers.
private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
       journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Distance moved = time * speed.
          float distCovered = (Time.time - startTime) * speed;

     // Fraction of journey completed = current distance divided by total distance.
          float fracJourney = distCovered / journeyLength;

     // Set our position as a fraction of the distance between the markers and pingpong the movement.
          transform.position = Vector3.Lerp(startMarker.position, endMarker.position, Mathf.PingPong (fracJourney, 1));
    }
}
