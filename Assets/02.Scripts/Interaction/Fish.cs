using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float freshness = 100f;
    public float Freshness
    {
        get
        {
            return freshness;
        }
        set
        {
            freshness = value;
            if (Mathf.Abs(freshness) <= Mathf.Epsilon)
            {
                Dead();
            }
        }
    }
    void Dead()
    {

    }
}
