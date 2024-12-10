using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float shootCooldown = 1.0f;
    public float shootDuration = 0.2f;

    public GameObject bulletPrefab;

    public Transform firePoint;

    [Header("Audio")]
    public AudioClip shootSound; 

    private AudioSource audioSource;

    private float shootCooldownTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        shootCooldownTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && shootCooldownTimer <= 0)
        {
           
            shootCooldownTimer = shootCooldown;

            var mousePosition = Input.mousePosition;
            var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            var direction = (worldPosition - transform.position).normalized;

            var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().direction = direction;

            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }
}
