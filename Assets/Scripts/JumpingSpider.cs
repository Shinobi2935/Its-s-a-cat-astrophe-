using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(CharacterStats))]
public class JumpingSpider : Enemy
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

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        currentPoint = position_1;
        isFollowingPlayer = false;
        currentTime = ildeTime;
        enemyAnimator.SetBool("IsMove", true);
        //shadow.transform.position = new Vector2(transform.position.x, transform.position.y - .5f);
    }

    // Update is called once per frame
    void Update()
    {
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
    private void FollowPlayer()
    {
        isFollowingPlayer = true;
        currentPoint = player;
        direction = currentPoint.position - transform.position;
        // Multiplica la fuerza por Time.deltaTime para tener en cuenta el paso del tiempo
        enemyRigidbody.AddRelativeForce(direction.normalized * moveSpeed * Time.deltaTime, ForceMode2D.Force);
    }

    IEnumerator IdleState()
    {
        enemyAnimator.SetBool("IsMove", false);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        currentTime = ildeTime;
        enemyAnimator.SetBool("IsMove", true);
    }

    private void Patrol()
    {
        if(isFollowingPlayer)
        {
            currentPoint = position_1;
            isFollowingPlayer = false;
        }
        distance = Vector2.Distance(transform.position, currentPoint.position);
        direction = currentPoint.position - transform.position;

        if(distance > 0.5f)
        {
            // Multiplica la fuerza por Time.deltaTime para suavizar el movimiento
            enemyRigidbody.AddRelativeForce(direction.normalized * moveSpeed * Time.deltaTime, ForceMode2D.Force);
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
