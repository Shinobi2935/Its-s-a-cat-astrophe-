using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhitchStats : CharacterStats
{
    public override IEnumerator Die()
    {
        Debug.Log(transform.name + " died.");
        enemyAnimator.SetTrigger("muerte");

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Final");
        Destroy(gameObject);
    }
}
