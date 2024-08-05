using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableProp : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] [Range(1, 10)] private int probabilityMaxRange = 5;
    [SerializeField] [Range(0, 10)] private int probabilityMinRange = 0;
    [SerializeField] [Range(0, 9)] private int failureProbability = 2;

    private static System.Random ran = new System.Random();
    private int result;

    public void SpawnItem()
    {
        result = ran.Next(probabilityMinRange,probabilityMaxRange);
        if (result > failureProbability)
        {
            Instantiate(item, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
    }
}
