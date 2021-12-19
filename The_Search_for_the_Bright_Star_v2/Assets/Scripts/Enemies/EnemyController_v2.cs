using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController_v2 : MonoBehaviour
{
    public float speed;
    public float changeTime = 3.0f;

    // Enemy health variables
    public int MaxHealth = 5;
    public int Health { get { return currentHealth; } }
    public int currentHealth;

    // Patrole variables
    public Transform[] patrolPoints;
    Transform currentPatrolPoint;
    private int currentPatrolIndex;

    // Enemy chase variables
    public Transform target;
    public float chaseRange;

    // Enemy attack variables
    public int damage;
    public float attackDelay;
    public float attackRange;
    private float lastAttackTime;

    // Enemy awareness
    public float awarenessRange;
    public float distanceToTarget;

    private Animator animator;
    private Rigidbody2D rb2D;

    // AStar Pathfinding
    public float nextWaypointDistance = 3f;

    private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = MaxHealth;

        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];


        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        // Check the ditstance to the player
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Check to see if the enmey is aware of the player - if not then patrol
        if (distanceToTarget > awarenessRange)
        {
            Patrol();
        }

        // Check the distance between enemy and player, to see if the player is within the awarenessRange and out of attackRange - chase
        if (distanceToTarget <= awarenessRange && distanceToTarget > attackRange)
        {
            Chase_AStarPathfinding();
        }

        // Check the distance between enemy and player to see if the player is close enough to attack
        if (distanceToTarget <= attackRange)
        {
            Attack();
        }
    }

    public void EnemyIsDead()
    {
        Destroy(gameObject);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, MaxHealth);
        Debug.Log("Enemy health: " + currentHealth + "/" + MaxHealth);

        if (currentHealth <= 0)
        {
            EnemyIsDead();
        }
    }

    void Patrol()
    {
        // Patroling AI
        transform.Translate(speed * Time.deltaTime * Vector3.up);
        // Check to see if we have reached the patrole point
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
        {
            // We have reached the patrol point - get the next one
            // Check to see if we have anymore patrol points left - if not go back to the beginning
            if (currentPatrolIndex + 1 < patrolPoints.Length)
            {
                currentPatrolIndex++;
            }
            else
            {
                currentPatrolIndex = 0;
            }
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }

        // Turn to face the current patrol point
        // Finding the direction Vector that points to the patrolpoint
        Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;

        // Get the angle in degrees that we need to turn towrds
        float angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg - 90f;
        // Made the rotation that we need to face
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        // Apply the rotation to our transform
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);

        animator.SetFloat("MoveY", patrolPointDir.y);
        animator.SetFloat("MoveX", patrolPointDir.x);
    }

    void Chase()
    {
        // Chasing Player AI
        // Get the distance to the target and check to see if it is close enough to chase

        // Start chasing the target - turn and move towards the target
        /*Vector3 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);

        transform.Translate(speed * Time.deltaTime * Vector3.up);*/
    }

    void Attack()
    {
        // Attacking AI - Melee

        // Check to see if enough time has passed since the last attack
        if (Time.time > lastAttackTime + attackDelay)
        {
            target.SendMessage("ChangeHealth", -damage);

            // Record the time of the attacked
            lastAttackTime = Time.time;
        }
    }

    // AStare Pathfinding

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb2D.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Chase_AStarPathfinding()
    {

        // Chasing Player AI
        // Get the distance to the target and check to see if it is close enough to chase

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

        //Debug.Log("What is currentWaypoint: " + currentWaypoint);
        //Debug.Log("What is path.vectorPath[currentWaypoint]: " + path.vectorPath[currentWaypoint]);
        //Debug.Log("What is reachedEndOfPath: " + reachedEndOfPath);

        var AStarSpeed = speed * 100f;

        //Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb2D.position).normalized;
        //Vector2 force = dir * AStarSpeed * Time.deltaTime;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Vector3 force = dir * AStarSpeed * Time.deltaTime;

        //Debug.Log("What is the dir: " + dir);
        //Debug.Log("What is the speed: " + AStarSpeed);
        //Debug.Log("What is the Time.deltaTime: " + Time.deltaTime);

        //Debug.Log("What is the force: " + force);

        rb2D.AddForce(force);

        float dis = Vector2.Distance(rb2D.position, path.vectorPath[currentWaypoint]);

        //Debug.Log("What is the dis: " + dis);

        if (dis < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //Debug.Log("What is currentWaypoint: " + currentWaypoint);
        //Debug.Log("What is rb2D.velocity.x: " + rb2D.velocity.x);

        if (rb2D.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (rb2D.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //Debug.Log("What is transform.localScale: " + transform.localScale);
        animator.SetFloat("MoveY", rb2D.velocity.y);
        animator.SetFloat("MoveX", rb2D.velocity.x);
    }
}
