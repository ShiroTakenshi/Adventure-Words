using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] float speed; // speed platform
    [SerializeField] int startingPoint; // starting index position platform
    [SerializeField] Transform[] points; // array tranform points

    private int i; //index of array

    void Start()
    {
        // setting the posititon of the platform to         
        // the position of the points using index "startingPoint"
        transform.position = points[startingPoint].position;
    }

    void Update()
    {
        // checking the distance of the platform and the point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.transform.SetParent(null);
    }
}
