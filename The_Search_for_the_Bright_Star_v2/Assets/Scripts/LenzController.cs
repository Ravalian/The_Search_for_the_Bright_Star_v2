using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenzController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called every frame
    //Write anything you want to happen continuously in the game
    //(reading input from the player, moving GameObjects, or counting time passing)
    void Update()
    {
        //Get horizontal and vertical movement from pressing left or right keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x = position.x + 0.01f * horizontal;
        position.y = position.y + 0.01f * vertical;

        transform.position = position;
    }
}
