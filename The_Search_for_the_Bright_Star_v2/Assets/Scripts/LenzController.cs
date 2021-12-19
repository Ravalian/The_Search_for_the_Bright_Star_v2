using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Code adapted from https://learn.unity.com/tutorial/world-interactions-blocking-movement?uv=2020.3&projectId=5c6166dbedbc2a0021b1bc7c#5ce3cdabedbc2a3ce61754e8
 */
public class LenzController : MonoBehaviour
{
    // Player attack variables
    public GameObject projectilePrefabArrowUpAndDown;
    public GameObject projectilePrefabArrowLeftAndRigth;

    // Player health variables
    public const int MaxHealth = 5;
    public int Health { get; private set; }

    // Player mana variables
    public const int MaxMana = 5;
    public float Mana { get; private set; }

    // Player movement variables
    public float speed = 3.0f;
    private Rigidbody2D rigidbody2d;
    private float horizontal;
    private float vertical;
    Vector2 lookDirection = new Vector2(1, 0);

    // Player animator variables
    Animator animator;

    // Player audio variables
    private AudioSource _audioSource;
    public AudioClip ShotProjectile;

    //Player dialogue variables
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInterctable Interctable {get; set;}

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        Debug.Log("AppState health on Start(): " + AppState.Instance.Health);
        Debug.Log("AppState mana on Start(): " + AppState.Instance.Mana);

        Health = AppState.Instance.Health ?? MaxHealth;
        Mana = AppState.Instance.Mana ?? MaxMana;

        HealthBar.Instance.SetValue((float)Health / MaxHealth);
        ManaBar.Instance.SetValue(Mana / MaxMana);

        Debug.Log("Health in Start is now at: " + Health);
        Debug.Log("Mana in Start is now at: " + Mana);

        transform.position = AppState.Instance.Positions[SceneManager.GetActiveScene().name];
    }

    // Update is called every frame
    private void Update()
    {
        //Lenz cannot move while interacting with dialogue
        if(dialogueUI.isOpen == true) return;

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

        if (Input.GetKeyDown(KeyCode.V)) 
        {
            Debug.Log("Pressed down 'V'");
            if(Interctable != null){
                Interctable.Interact(this);
            }
        }
        if(Mana < MaxMana){
          ChangeMana(0.0001f);
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
            GameObject projectileObject;
            Projectile projectile;

            projectileObject = Instantiate(projectilePrefabArrowUpAndDown, rigidbody2d.position - Vector2.up * 0.15f, Quaternion.identity);
            projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);



            Mana -= 0.5f;
            //animator.SetTrigger("Launch");

            PlaySound(ShotProjectile);
            ManaBar.Instance.SetValue(Mana / MaxMana);
        }
    }

    //Clamp makes sure Lenz is never below 0 hp or above maxhealth hp
    public void ChangeHealth(int amount)
    {   
        Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
        //Debug.Log("Player health: " + Health + "/" + MaxHealth);
        if(Health <= 0){
          Die();
        }
        HealthBar.Instance.SetValue((float)Health/MaxHealth);
    }

    //Clamp makes sure Lenz is never below 0 hp or above maxhealth hp
    public void ChangeMana(float amount)
    {
        //Mana = Mathf.Clamp((int)(Mana + amount), 0, MaxMana);
        float tempmana = Mana + amount;
        Mana = tempmana > MaxMana ? MaxMana : tempmana;
        //Debug.Log("Player mana: " + Mana + "/" + MaxMana);
        ManaBar.Instance.SetValue(Mana / MaxMana);
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    private void Die()
    {
        //Reset main scene position
        if (SceneManager.GetActiveScene().name == SceneNames.MainScene)
        {
            AppState.Instance.Positions[SceneNames.MainScene] = new Vector2(-4.96f, -1.33f);
        }

        transform.position = AppState.Instance.Positions[SceneManager.GetActiveScene().name];
        ChangeHealth(MaxHealth);
        ChangeMana(MaxMana);
    }
    public void SaveLenzState()
    {
        AppState.Instance.Health = Health;
        AppState.Instance.Mana = Mana;

        Debug.Log("AppState health SaveLenzState(): " + AppState.Instance.Health);
        Debug.Log("AppState mana SaveLenzState(): " + AppState.Instance.Mana);
    }

    public void SaveLenzPosition(Vector2 vector, string scene)
    {
        AppState.Instance.Positions[scene] = vector;
    }
}
