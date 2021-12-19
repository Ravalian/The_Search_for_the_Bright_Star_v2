using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour
{

    public Transform target;

    public float speed = 100;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb2D;

    // Enemy awareness
    public float awarenessRange;
    public float distanceToTarget;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2D = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb2D.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check the ditstance to the player
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        //Debug.Log("What is the distanceToTarget: " + distanceToTarget);

        // Check to see if the enmey is aware of the player - if not then patrol
        if (distanceToTarget > awarenessRange)
        {
            //CancelInvoke("UpdatePath");
            
        }

        // Check the distance between enemy and player, to see if the player is within the awarenessRange and out of attackRange - chase
        if (distanceToTarget <= awarenessRange)
        {
            if (path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Debug.Log("What is currentWaypoint: " + currentWaypoint);
            Debug.Log("What is path.vectorPath[currentWaypoint]: " + path.vectorPath[currentWaypoint]);

            Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb2D.position).normalized;
            Vector2 force = dir * speed * Time.deltaTime;

            Debug.Log("What is the dir: " + dir);
            Debug.Log("What is the speed: " + speed);
            Debug.Log("What is the Time.deltaTime: " + Time.deltaTime);

            Debug.Log("What is the force: " + force);

            rb2D.AddForce(force);

            float dis = Vector2.Distance(rb2D.position, path.vectorPath[currentWaypoint]);

            if (dis < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (rb2D.velocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            if (rb2D.velocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    void Chase()
    {
        
    }
}
