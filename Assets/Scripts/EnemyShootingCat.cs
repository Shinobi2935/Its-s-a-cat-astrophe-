using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class EnemyShootingCat : Enemy
{
    [SerializeField] private Transform position_1;
    [SerializeField] private Transform position_2;
    [SerializeField] private Transform position_3;
    [SerializeField] private Transform position_4;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float ildeTime = 4.0f;
    private Rigidbody2D enemyRigidbody;
    private Animator enemyAnimator; 
    private Transform currentPoint;
    private float distance;
    private Vector2 direction = Vector2.zero;
    private Transform player;
    private bool isFollowingPlayer;
    private float currentTime;

    public GameObject bulletPrefab;
    public float rotationSpeed = 0.0025f;
    public float distanceToShoot = 3.5f;
    public float distanceToStop = 3f;
    public float distanceToFollow = 5f;
    public float fireRate;
    public float timeToFire;
    public Transform firepoint;

    public override void Start()
    {
        base.Start();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        Debug.Log("player " + player);
        currentPoint = position_1;
        isFollowingPlayer = false;
        currentTime = ildeTime;
        enemyAnimator.SetBool("IsMove", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && Vector2.Distance(player.position, transform.position) <= distanceToFollow) {
            RotateTowardsPlayer();
            if (player != null && Vector2.Distance(player.position, transform.position) <= distanceToShoot)
            {
                Shoot();
            }

            if (Vector2.Distance(player.position, transform.position) >= distanceToStop)
            {
                enemyRigidbody.velocity = transform.up * moveSpeed;
            }
            else
            {
                StartCoroutine(IdleState());
                enemyRigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            StartCoroutine(IdleState());
            enemyRigidbody.velocity = Vector2.zero;
        }
        if(player != null)
        {
            distance = Vector2.Distance(transform.position, player.position);
            if(distance < 4.0f) { FollowPlayer(); }
            else if(currentTime > 0.0f) { Patrol(); }
            else { StartCoroutine(IdleState()); }
        }
        enemyAnimator.SetFloat("Horizontal", direction.x);
        enemyAnimator.SetFloat("Vertical", direction.y);
    }

    private void Shoot()
    {
        if (GameManager.gameIsPaused) return;
        //Debug.Log(timeToFire);
        if(timeToFire <= 0f)
        {
            ////Debug.Log("Shoot " + timeToFire);
            //Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            timeToFire = fireRate;
            var projectileShoot = Instantiate(bulletPrefab, firepoint.position, Quaternion.identity);
            Vector3 direction = player.position - firepoint.position;
            projectileShoot.GetComponent<Rigidbody2D>().AddRelativeForce(direction.normalized * projectileShoot.GetComponent<bullet>().speed, ForceMode2D.Force);
            enemyAnimator.SetTrigger("shoot");
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    private void FollowPlayer()
    {
        enemyAnimator.SetBool("IsMove", true);
        isFollowingPlayer = true;
        currentPoint = player;
        direction = currentPoint.position - transform.position;
        enemyRigidbody.AddRelativeForce(direction.normalized * moveSpeed, ForceMode2D.Force);
        transform.position = Vector2.MoveTowards(this.transform.position, currentPoint.position, moveSpeed * Time.deltaTime);
    }

    void RotateTowardsPlayer()
    {
        Vector2 targetDirection = player.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        //transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotationSpeed);
    }

    void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    IEnumerator IdleState()
    {
        enemyAnimator.SetBool("IsMove", false);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        currentTime = ildeTime;
        //enemyAnimator.SetBool("IsMove", true);
    }

    private void Patrol()
    {
        enemyAnimator.SetBool("IsMove", true);
        if (isFollowingPlayer)
        {
            //currentPoint = this.transform;
            currentPoint = position_1;
            isFollowingPlayer = false;
        }
        distance = Vector2.Distance(transform.position, currentPoint.position);
        direction = currentPoint.position - transform.position;

        if(distance > 0.5f)
        {
            enemyRigidbody.AddRelativeForce(direction.normalized * moveSpeed, ForceMode2D.Force);
            transform.position = Vector2.MoveTowards(this.transform.position, currentPoint.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if(currentPoint == position_1) { currentPoint = position_2; }
            else if(currentPoint == position_2) { currentPoint = position_3; }
            else if(currentPoint == position_3) { currentPoint = position_4; }
            else { currentPoint = position_1; }
        }
        currentTime -= Time.deltaTime;
    }
}
