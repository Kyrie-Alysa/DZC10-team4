using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    //arraw of waypoints to walk from one to the next one
    [SerializeField]
    Transform[] waypoints;

    //Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 3f;

    //Indext of current waypoint from which enemy walks to the next one
    private int waypointIndex = 0;
    void Start()
    {
        //set position of enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //method to make the enemy walk
    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position,
                                                 waypoints[waypointIndex].transform.position,
                                                 moveSpeed * Time.deltaTime);
        if(transform.position == waypoints [waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }
        if(waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
