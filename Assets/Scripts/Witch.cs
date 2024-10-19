using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy
{
    [SerializeField] private GameObject projectiles;
    [SerializeField] private Transform target;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int intervale = 3;
    [SerializeField] private int bulletSpeed = 3;

     private bool isShooting = false;
     
    public void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnEnable()
    {
        Debug.Log("OnEnabled()", gameObject);
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (target != null && !GameManager.gameIsPaused)
            {
                var projectileShoot = Instantiate(projectiles, spawnPoint.position, Quaternion.identity);
                Vector3 direction = target.position - spawnPoint.position;
                projectileShoot.GetComponent<Rigidbody2D>().AddRelativeForce(direction.normalized * bulletSpeed, ForceMode2D.Force);

                isShooting = true;

                yield return new WaitForSeconds(intervale);
            }
            else { yield return null; }
        }
    }

    void OnDisable() { StopCoroutine(Shoot()); }
}
