using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCatMelee : Enemy
{
    [SerializeField] private Transform position_1;
    [SerializeField] private Transform position_2;
    [SerializeField] private Transform position_3;
    [SerializeField] private Transform position_4;
    [SerializeField] private float moveSpeed = 2.0f;
    private Rigidbody2D enemyRigidbody;
    private Animator enemyAnimator; 
    private Transform currentPoint;
    private float distance;
    private Vector2 direction = Vector2.zero;
    private Transform player;
    private bool isFollowingPlayer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        currentPoint = position_1;
        isFollowingPlayer = false;
        enemyAnimator.SetBool("IsMove", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            distance = Vector2.Distance(transform.position, player.position);
            if(distance < 4.0f) { FollowPlayer(); }
            else if (!isFollowingPlayer) { Patrol(); }
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
        if(distance < 1.5f) { AttackState(); }
        else
        {
            enemyAnimator.SetBool("IsMove", true);
            enemyRigidbody.AddRelativeForce(direction.normalized * moveSpeed, ForceMode2D.Force);
            //transform.position = Vector2.MoveTowards(this.transform.position, currentPoint.position, moveSpeed * Time.deltaTime);
        }

    }

    IEnumerator IdleState()
    {
        enemyAnimator.SetBool("IsMove", false);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
    }

    private void AttackState()
    {
        enemyAnimator.SetBool("IsMove", false);
        enemyAnimator.SetTrigger("Attack");
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
            enemyRigidbody.AddRelativeForce(direction.normalized * moveSpeed, ForceMode2D.Force);
            //transform.position = Vector2.MoveTowards(this.transform.position, currentPoint.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if(currentPoint == position_1) { currentPoint = position_2; }
            else if(currentPoint == position_2) { currentPoint = position_3; }
            else if(currentPoint == position_3) { currentPoint = position_4; }
            else { currentPoint = position_1; }
        }
    }
}
