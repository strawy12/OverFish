using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculate : MonoBehaviour
{
    [SerializeField] List<Fish> fishs;

    private Aquarium aquarium = null;
    void SearchFishs()
    {
        aquarium ??= FindObjectOfType<Aquarium>();
        fishs = aquarium.containFish;
    }
    public float CountFishPrice()
    {
        SearchFishs();
        float result = 0f;
        foreach (Fish fish in fishs)
        {
            result += fish.price * (fish.Freshness / 100);
        }

        DataManager.Inst.CurrentPlayer.gold += (int)result;

        return result;
    }
}
