using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Leftovers : Enemy
{
    [SerializeField] private int exploteTime = 5;
    private Animator enemyAnimator; 

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        enemyAnimator = GetComponent<Animator>();
        StartCoroutine(Explote());
    }

    IEnumerator Explote()
    {
        yield return new WaitForSeconds(exploteTime);
        enemyAnimator.SetTrigger("muerte");
        StartCoroutine(stats.Die());
    }
}
