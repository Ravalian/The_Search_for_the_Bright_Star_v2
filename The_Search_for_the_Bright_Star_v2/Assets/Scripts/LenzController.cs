using UnityEngine;

/*
 * Code adapted from https://learn.unity.com/tutorial/world-interactions-blocking-movement?uv=2020.3&projectId=5c6166dbedbc2a0021b1bc7c#5ce3cdabedbc2a3ce61754e8
 */
public class LenzController : MonoBehaviour
{
    public float speed = 3.0f;

    public int Health { get; private set; }
    public int MaxHealth = 5;

    private Rigidbody2D rigidbody2D;
    private float horizontal;
    private float vertical;
    Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        //Gets RigidBody2D attached to the gameobject the script is attached to
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Health = MaxHealth;
    }

    // Update is called every frame
    private void Update()
    {
        //Get horizontal and vertical movement from pressing left or right keys
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        animator.SetFloat("MoveX", horizontal);
        animator.SetFloat("MoveY", vertical);
    }

    private void FixedUpdate()
    {
        var position = rigidbody2D.position;

        //Time.deltaTime - make movement not dependent on frames
        position.x += speed * horizontal * Time.deltaTime; 
        position.y += speed * vertical * Time.deltaTime;

        rigidbody2D.MovePosition(position);
    }

    //Clamp makes sure Lenz is never below 0 hp or above maxhealth hp
    public void ChangeHealth(int amount)
    {
        Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
        Debug.Log(Health + "/" + MaxHealth);
    }
}
