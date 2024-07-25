using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableProp : MonoBehaviour
{
    [SerializeField] private GameObject item;

    private static System.Random ran = new System.Random();
    private int result;

    public void SpawnItem()
    {
        result = ran.Next(0,5);
        if (result > 2) Instantiate(item, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }
}
