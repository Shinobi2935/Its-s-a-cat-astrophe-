using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlocker : MonoBehaviour
{
    [SerializeField] private Letrero Letrero_1;

    private bool todosLosLetreros = false;

    // Start is called before the first frame update
    void Start()
    {
        if(Letrero_1 != null) { todosLosLetreros = true; }
    }

    // Update is called once per frame
    void Update()
    {
        if(todosLosLetreros && Letrero_1.leido) { Destroy(gameObject); }
    }
}
