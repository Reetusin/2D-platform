using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float shootCooldown = 1.0f;
    public float ShootDuration = 0.2f;

    public GameObject bulletPrefab;

    public Transform firePoint;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            var direction = (worldPosition - transform.position).normalized;


            var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().direction = direction;
        }
    }
}
