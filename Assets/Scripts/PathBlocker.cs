using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlocker : MonoBehaviour
{
    [SerializeField] private Letrero Letrero_1;
    [SerializeField] private Letrero Letrero_2;
    [SerializeField] private Letrero Letrero_3;
    [SerializeField] private Letrero Letrero_4;
    [SerializeField] private Letrero Letrero_5;
    [SerializeField] private Letrero Letrero_6;
    [SerializeField] private Letrero Letrero_7;

    private bool todosLosLetreros = false;

    // Start is called before the first frame update
    void Start()
    {
        if(Letrero_1 != null && Letrero_2 != null && Letrero_3 != null && Letrero_4 != null && 
            Letrero_5 != null && Letrero_6 != null && Letrero_7 != null)
        {
            todosLosLetreros = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(todosLosLetreros)
        {
            if(Letrero_1.leido && Letrero_2.leido && Letrero_3.leido && Letrero_4.leido && 
                Letrero_5.leido && Letrero_6.leido && Letrero_7.leido)
            {
                Destroy(gameObject);
            }
        }
    }
}
