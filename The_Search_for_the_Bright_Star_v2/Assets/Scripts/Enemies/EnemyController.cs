using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public float speed;
    //public bool vertical;
    public float changeTime = 3.0f;

    // Enemy health variables
    public int MaxHealth = 5;
    public int Health { get { return currentHealth; }}
    public int currentHealth;
    
    // Patrole variables
    public Transform[] patrolPoints;
    Transform currentPatrolPoint;
    int currentPatrolIndex;

    // Enemy chase variables
    public Transform target;
    public float chaseRange;

    // Enemy attack variables
    public int damage;
    public float attackDelay;
    public float attackRange;
    float lastAttackTime;

    // Enemy awareness
    public float awarenessRange;
    public float distanceToTarget;

    Animator animator;
    Rigidbody2D rb2D;

    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = MaxHealth;

        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
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
            Chase();
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
        
        Debug.Log(patrolPointDir.x);
        animator.SetFloat("MoveY", patrolPointDir.y);
        animator.SetFloat("MoveX", patrolPointDir.x);
    }

    void Chase()
    {
        // Chasing Player AI
        // Get the distance to the target and check to see if it is close enough to chase

        // Start chasing the target - turn and move towards the target
        Vector3 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);
        var vector = Vector3.back;
        var y = targetDir.y;
        var x = targetDir.x;
        Debug.Log("X="+x);
        if(y>x && y>1){
          vector = Vector3.up;
        }
        else if(y<x && y<-1) {
          vector = Vector3.down;
        } else if(x<y && x<-1) {
          vector = Vector3.left;
        } else if(x>y && x>1){
          vector = Vector3.right;
        }
        transform.Translate(speed * Time.deltaTime * vector);
        animator.SetFloat("MoveY", targetDir.y);
        animator.SetFloat("MoveX", targetDir.x);
        //transform.Translate(speed * Time.deltaTime);
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
}
