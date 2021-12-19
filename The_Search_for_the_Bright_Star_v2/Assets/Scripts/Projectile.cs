using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    private float destroyTime = 5;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }

        WaitAndDestroy();
    }

    void WaitAndDestroy()
    {
        Destroy(gameObject, destroyTime);
    }

    public void Launch(Vector3 direction, float force)
    {
        //Debug.Log(direction);

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController_v2 enemy = other.collider.GetComponent<EnemyController_v2>();

        if (enemy != null)
        {
            enemy.ChangeHealth(-1);
        }

        //we also add a debug log to know what the projectile touch
        Debug.Log("Projectile Colision with: " + other.gameObject);
        Destroy(gameObject);
    }
}
