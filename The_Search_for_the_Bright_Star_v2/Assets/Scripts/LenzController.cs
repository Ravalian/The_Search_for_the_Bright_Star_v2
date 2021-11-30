using UnityEngine;

/*
 * Code adapted from https://learn.unity.com/tutorial/world-interactions-blocking-movement?uv=2020.3&projectId=5c6166dbedbc2a0021b1bc7c#5ce3cdabedbc2a3ce61754e8
 */
public class LenzController : MonoBehaviour
{
    public float speed = 3.0f;

    public GameObject projectilePrefab;

    //public int Health { get; private set; }
    public int MaxHealth = 5;
    public int MaxMana = 5;
    public float Mana {get; private set;}
    public int Health { get { return currentHealth; }}

    public int currentHealth;
    private Rigidbody2D rigidbody2d;
    private float horizontal;
    private float vertical;
    Animator animator;

    Vector2 lookDirection = new Vector2(1, 0);

    private AudioSource _audioSource;
    public AudioClip ShotProjectile;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        Mana = MaxMana;
        currentHealth = MaxHealth;
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

        animator.SetFloat("MoveX", horizontal);
        animator.SetFloat("MoveY", vertical);

        if (Input.GetKeyDown(KeyCode.V)) {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1f, LayerMask.GetMask("InteractableObject"));
            if(hit.collider != null)
            {
                Debug.Log("Raycast has hit the obejct " + hit.collider.gameObject);
            }
        }
    }

    private void FixedUpdate()
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
      if(Mana > 0){
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        Mana = Mana - 0.5f;
        //animator.SetTrigger("Launch");

        PlaySound(ShotProjectile);
        ManaBar.Instance.SetValue(Mana/MaxMana);
      }
    }

    //Clamp makes sure Lenz is never below 0 hp or above maxhealth hp
    public void ChangeHealth(int amount)
    {   
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, MaxHealth);
        Debug.Log("Player health: " + currentHealth + "/" + MaxHealth);
        HealthBar.Instance.SetValue((float)Health/MaxHealth);
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
