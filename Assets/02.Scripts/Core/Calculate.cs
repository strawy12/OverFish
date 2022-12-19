using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculate : MonoSingleton<Calculate>
{
    [SerializeField] List<Fish> fishs;
    public float result = 0f;
    void SearchFishs()
    {
        fishs = FindObjectOfType<Aquarium>().containFish;
    }
    public float CountFishPrice()
    {
        SearchFishs();
        foreach (Fish fish in fishs)
        {
            result += fish.price * (fish.Freshness / 100);
        }
        return result;
    }

    public void GameOver()
    {

    }
}
