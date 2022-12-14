using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodCreator : MonoBehaviour
{
    [SerializeField]
    private int maxFishingRodCount;

    private void Start()
    {
        // EventManager.StartListening("GameStart", (a) => CreateFishRod());
        ShowFishRod();
    }

    private void ShowFishRod()
    {
        HideAll();

        int count = DataManager.Inst.FindUpgradeData(EUpgradeDataType.FishRodCount).level;
        count = Mathf.Clamp(count, 1, maxFishingRodCount);

        for (int i = 0; i < count; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void HideAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
