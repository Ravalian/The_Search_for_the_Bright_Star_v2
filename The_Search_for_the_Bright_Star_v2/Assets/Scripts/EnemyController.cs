using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    // Enemy health variables
    public int MaxHealth = 5;
    public int Health { get { return currentHealth; }}
    public int currentHealth;
    
    // Patrole variables
    public Transform[] patrolPoints;
    private Transform currentPatrolPoint;
    private int currentPatrolIndex;

    // Enemy chase variables
    public Transform target;
    public float chaseRange;

    private float timer;
    private int direction = 1;

    Animator animator;
    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timer = changeTime;
        currentHealth = MaxHealth;

        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        // Working moving script in ether the vertical or horizontal direction witch animations
        //Vector2 position = rigidbody2d.position;

        //if (vertical)
        //{
        //    position.y = position.y + Time.deltaTime * speed * direction;
        //    animator.SetFloat("MoveY", direction);
        //}
        //else
        //{
        //    position.x = position.x + Time.deltaTime * speed * direction;
        //    animator.SetFloat("MoveX", direction);
        //}

        //rigidbody2d.MovePosition(position);


        // Work in progress patroling script
        transform.Translate (Vector3.up * Time.deltaTime * speed);
        // Check to see if we have reached the patrole point
        if(Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
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

        //if (patrolPointDir.y < 1)
        //{
        //    animator.SetFloat("MoveY", patrolPointDir.y);
        //}
        //if (patrolPointDir.x > 1)
        //{
        //    animator.SetFloat("MoveX", patrolPointDir.x);
        //}

        // Get the angle in degrees that we need to turn towrds
        float angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg - 90f;
        // Made the rotation that we need to face
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        // Apply the rotation to our transform
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);

        //// Work ing progress chasing script
        //// Get the distance to the target and check to see if it is close enough to chase
        //float distanceToTarget = Vector3.Distance(transform.position, target.position);
        //if (distanceToTarget < chaseRange)
        //{
        //    // Start chasing the target - turn and move towards the target
        //    Vector3 targetDir = target.position - transform.position;
        //    float angle1 = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        //    Quaternion q1 = Quaternion.AngleAxis(angle1, Vector3.forward);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, q1, 180f);

        //    transform.Translate(Vector3.up * Time.deltaTime * speed);
        //}
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        LenzController player = other.gameObject.GetComponent<LenzController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
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
}
