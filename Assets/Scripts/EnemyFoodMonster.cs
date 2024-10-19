using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class EnemyFoodMonster : Enemy
{
    [SerializeField] private GameObject leftovers;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float sR = 2.0f;
    [SerializeField] private float detectPlayerDistance = 2.0f;
    private Rigidbody2D enemyRigidbody;
    private Animator enemyAnimator; 
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
        isFollowingPlayer = false;
        enemyAnimator.SetBool("IsMove", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFollowingPlayer) { distance = Vector2.Distance(transform.position, player.position); }
        if(distance < detectPlayerDistance) { FollowPlayer(); }
        enemyAnimator.SetFloat("Horizontal", direction.x);
        enemyAnimator.SetFloat("Vertical", direction.y);
    }

    private void FollowPlayer()
    {
        isFollowingPlayer = true;
        direction = player.position - transform.position;
        // Multiplica la fuerza por Time.deltaTime para tener en cuenta el paso del tiempo
        enemyRigidbody.AddRelativeForce(direction.normalized * moveSpeed * Time.deltaTime, ForceMode2D.Force);
        enemyAnimator.SetBool("IsMove", true);
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        if(col.CompareTag("Player"))
        {
            enemyAnimator.SetTrigger("Explote");
            for (var i = 0; i < 4; i++)
            {
                Instantiate(leftovers, 
                    new Vector3(player.position.x + Random.Range(-sR, sR), player.position.y +  Random.Range(-sR, sR), 0), 
                    Quaternion.identity);
            }
            StartCoroutine(stats.Die());
        }
    }
}
