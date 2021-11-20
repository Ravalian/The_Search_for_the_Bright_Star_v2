using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code adapted from https://learn.unity.com/tutorial/world-interactions-blocking-movement?uv=2020.3&projectId=5c6166dbedbc2a0021b1bc7c#5ce3cdabedbc2a3ce61754e8
 */
public class LenzController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private float horizontal;
    private float vertical;

    // Start is called before the first frame update
    void Start()
    {
        //Gets RigidBody2D attached to the gameobject the script is attached to
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called every frame
    //Write anything you want to happen continuously in the game
    //(reading input from the player, moving GameObjects, or counting time passing)
    void Update()
    {
        //Get horizontal and vertical movement from pressing left or right keys
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        var position = rigidbody2D.position;

        //Time.deltaTime - make movement not dependent on frames
        position.x += 3.0f * horizontal * Time.deltaTime; 
        position.y += 3.0f * vertical * Time.deltaTime;

        rigidbody2D.MovePosition(position);
    }
}
