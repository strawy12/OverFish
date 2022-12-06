using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fish : FishCalculate
{
    [SerializeField] private float freshness = 100f;
    public float fishHeight;
    public float fishWeight;
    [SerializeField] Sprite fishIcon;
    [SerializeField] float price;

    bool isDead = false;

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

    /// <summary>
    /// type 0 are Set Freshness to Value /
    /// type 1 are Increase Freshness to Value /
    /// type 2 are Discrease Freshness to Value
    /// </summary>
    public void SetFreshness(float value, int type)
    {
        switch (type)
        {
            case 0:
                Freshness = value;
                break;
            case 1:
                Freshness += value;
                break;
            case 2:
                Freshness -= value;
                break;
            default:
                break;
        }
    }

    public Fish()
    {
        fishHeight = height;
        fishWeight = weight;
        price = Calculate();
    }

    void Dead()
    {
        isDead = true;
        
    }
}
