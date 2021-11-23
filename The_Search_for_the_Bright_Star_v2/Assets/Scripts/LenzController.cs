using UnityEngine;

/*
 * Code adapted from https://learn.unity.com/tutorial/world-interactions-blocking-movement?uv=2020.3&projectId=5c6166dbedbc2a0021b1bc7c#5ce3cdabedbc2a3ce61754e8
 */
public class LenzController : MonoBehaviour
{
    public float speed = 3.0f;

    public GameObject projectilePrefab;

    public int Health { get; private set; }
    public int MaxHealth = 5;

    private Rigidbody2D rigidbody2D;
    private float horizontal;
    private float vertical;

    Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        Health = MaxHealth;
    }

    // Update is called every frame
    private void Update()
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

    private void FixedUpdate()
    {
        var position = rigidbody2D.position;

        //Time.deltaTime - make movement not dependent on frames
        position.x += speed * horizontal * Time.deltaTime; 
        position.y += speed * vertical * Time.deltaTime;

        rigidbody2D.MovePosition(position);
    }

    // function to use range attack
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        //animator.SetTrigger("Launch");
    }

    //Clamp makes sure Lenz is never below 0 hp or above maxhealth hp
    public void ChangeHealth(int amount)
    {
        Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
        Debug.Log(Health + "/" + MaxHealth);
    }
}
