using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Turret : MonoBehaviour
{
    public Transform turretBarrel;
    public GameObject bulletPrefab;
    public float reloadDelay = 1;
    private bool canShoot = true;
    private Collider2D[] tankColliders;
    private float currentDelay = 0;

    public UnityEvent OnShoot;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();

    }


    private void Update()
    {
        if (canShoot == false)
        { 
            currentDelay -= Time.deltaTime;
            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }
    }
    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            currentDelay = reloadDelay;

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = turretBarrel.position;
            bullet.transform.localRotation = turretBarrel.rotation;
            bullet.GetComponent<Bullet>().Initialize();

            OnShoot.Invoke();
        }
    }
}
