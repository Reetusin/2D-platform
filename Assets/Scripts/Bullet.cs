using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject particles;
    public Vector3 direction;
    public float speed = 20;
    public Vector2 damageRange = new Vector2(10, 20);

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        float damage = Random.Range(damageRange.x, damageRange.y);

        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage((int)damage);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(1);  // Dealing 1 damage to the zombie
            Destroy(gameObject);  // Destroy the bullet after it hits
        }

        if (particles != null)
        {
            Instantiate(particles, other.contacts[0].point, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
