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

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnEnable()
    {
        //This will be called when object is enabled.
        Debug.Log("OnEnabled()", gameObject);
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        var projectileShoot = Instantiate(projectiles, spawnPoint.position, Quaternion.identity);
        Vector3 direction = target.position - spawnPoint.position;
        projectileShoot.GetComponent<Rigidbody2D>().AddRelativeForce(direction.normalized * bulletSpeed, ForceMode2D.Force);
        yield return new WaitForSeconds(intervale);
        StartCoroutine(Shoot());
    }
}
