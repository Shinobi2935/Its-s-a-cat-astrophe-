using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouse : Enemy
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

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        currentPoint = position_1;
        //shadow.transform.position = new Vector2(transform.position.x, transform.position.y - .5f);
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        enemyAnimator.SetFloat("Horizontal", direction.x);
        enemyAnimator.SetFloat("Vertical", direction.y);
    }

    private void Patrol()
    {
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
