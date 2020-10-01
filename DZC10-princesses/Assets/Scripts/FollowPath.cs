using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    //arraw of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;

    //Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 3f;

    //Indext of current waypoint from which enemy walks to the next one
    private int waypointIndex = 0;
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //method to make the enemy walk
    private void Move()
    {
        //
        if(waypointIndex == waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position,
                moveSpeed * Time.deltaTime);
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex = 0;
            }
        }
        else if(waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position, 
                moveSpeed*Time.deltaTime);
            if(transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
}
