using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    [SerializeField] float speed; // speed platform
    [SerializeField] int startingPoint; // starting index position platform
    [SerializeField] Transform[] points; // array tranform points
    [SerializeField] Transform playerCheckPos; // player position
    [SerializeField] LayerMask playerLayer;


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


            Flip();
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        if (!Physics2D.OverlapCircle(playerCheckPos.position, 0.3f, playerLayer))
        {
            Debug.Log("ABC");
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
    }
}
