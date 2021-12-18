using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class CateractController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private float horizontal;
    private float vertical;
    Animator animator;
    private bool run = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //thread1 = new Thread(() => DoWork());
        

    }

    void DoWork()
    {
        System.Random rand = new System.Random();
        while (true)
        {
            horizontal = (float)rand.NextDouble()*2-1;
            vertical = (float)rand.NextDouble()*2-1;
            Thread.Sleep(500);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!run)
        {
            //Thread thread1 = new Thread(() => DoWork());
            //thread1.Start();
            //run = true;
        }

        animator.SetFloat("MoveX", horizontal);
        //Debug.Log(horizontal);

        animator.SetFloat("MoveY", vertical);
        //Debug.Log(vertical);



    }
}
