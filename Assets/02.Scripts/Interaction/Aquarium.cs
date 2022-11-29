using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquarium : InteractionObject
{

    [SerializeField] private Material waterMaterial;
    [SerializeField] private bool isPollution = false;
    [SerializeField] private float MaxCleaness = 100f;
    [SerializeField] private float curCleanessAmount;
    [SerializeField] private float pollution = 1f;
    [SerializeField] WaitForSeconds waitPolluteTime = new WaitForSeconds(0.3f);
    [SerializeField] Color currentWaterColor;
    [SerializeField] private Renderer waterRenderer;
    [SerializeField] private Fish[] containFish;
    [SerializeField] Color[] waterColors = new Color[4];

    protected override void Awake()
    {
        base.Awake();
        waterMaterial = waterRenderer.material;
        waterColors[0] = waterMaterial.color;
        currentWaterColor = waterColors[0];

        curCleanessAmount = MaxCleaness;
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Pollute());
    }
    public IEnumerator Pollute()
    {
        while (true)
        {
            IncreasePollution(pollution);
            if (isPollution)
            {
                foreach (Fish fish in containFish)
                {
                    if (fish != null) 
                        fish.Freshness -= pollution;
                }
            }
            yield return waitPolluteTime;
        }
    }
    void IncreasePollution(float value)
    {
        curCleanessAmount -= pollution;
        CheckPollution();
        SetColor();
    }

    void SetColor()
    {
        currentWaterColor = ((int)MaxCleaness / (int)curCleanessAmount) switch
        {
            1 => waterColors[0],
            2 => waterColors[1],
            3 => waterColors[2],
            _ => waterColors[3]
        };
        waterMaterial.color = currentWaterColor;
    }

    public void CheckPollution()
    {
        if (Mathf.Abs(MaxCleaness / 2) >= curCleanessAmount)
        {
            isPollution = true;
        }
        else
        {
            isPollution = false;
        }
    }

    public override void TriggerInteraction()
    {

    }
}
