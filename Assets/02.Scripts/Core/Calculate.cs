using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculate : MonoBehaviour
{
    [SerializeField] List<Fish> fishs;
    [SerializeField] float result = 0f;
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
}
