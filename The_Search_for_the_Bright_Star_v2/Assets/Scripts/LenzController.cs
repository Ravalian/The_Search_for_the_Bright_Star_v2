using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code adapted from https://learn.unity.com/tutorial/world-interactions-blocking-movement?uv=2020.3&projectId=5c6166dbedbc2a0021b1bc7c#5ce3cdabedbc2a3ce61754e8
 */
public class LenzController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private float horizontal;
    private float vertical;

    Vector2 lookDirection = new Vector2(1, 0);

    public float speed = 3.0f;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Gets RigidBody2D attached to the gameobject the script is attached to
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called every frame
    //Write anything you want to happen continuously in the game
    //(reading input from the player, moving GameObjects, or counting time passing)
    void Update()
    {
        //Get horizontal and vertical movement from pressing left or right keys
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            Launch();
        }
    }

    void FixedUpdate()
    {
        var position = rigidbody2d.position;

        //Time.deltaTime - make movement not dependent on frames
        position.x += speed * horizontal * Time.deltaTime; 
        position.y += speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    // function to use range attack
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        //animator.SetTrigger("Launch");
    }
}
