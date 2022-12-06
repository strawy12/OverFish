using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquarium : InteractionObject
{

    [SerializeField] private Material waterMaterial;
    [SerializeField] Color currentWaterColor;
    [SerializeField] private Renderer waterRenderer;
    [SerializeField] Color[] waterColors = new Color[4];

    [SerializeField] private bool isPollution = false;

    [SerializeField] private float MaxCleanness = 100f;
    [SerializeField] private float curCleannessAmount;
    [SerializeField] private float pollution = 1f;
    [SerializeField] private float bucketCleannessAmount = 10f;
    [SerializeField] WaitForSeconds waitPolluteTime = new WaitForSeconds(1f);

    [SerializeField] private List<Fish> containFish;

    protected override void Awake()
    {
        base.Awake();
        waterMaterial = waterRenderer.material;
        waterColors[0] = waterMaterial.color;
        currentWaterColor = waterColors[0];

        curCleannessAmount = MaxCleanness;
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
                        fish.SetFreshness(pollution, 2);
                }
            }
            yield return waitPolluteTime;
        }
    }
    void IncreasePollution(float value)
    {
        SetCleanness(value, 2);
        CheckPollution();
        SetColor();
    }

    void SetColor()
    {
        currentWaterColor = ((int)MaxCleanness / (int)curCleannessAmount) switch
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
        if (Mathf.Abs(MaxCleanness / 2) >= curCleannessAmount)
        {
            isPollution = true;
        }
        else
        {
            isPollution = false;
        }
    }
    /// <summary>
    /// type 0 are Set Cleanness to Value /
    /// type 1 are Increase Claeness to Value /
    /// type 2 are Discrease Cleanness to Value
    /// </summary>
    public void SetCleanness(float value, int type)
    {
        switch (type)
        {
            case 0:
                curCleannessAmount = value;
                break;
            case 1:
                curCleannessAmount += value;
                break;
            case 2:
                curCleannessAmount -= value;
                break;
            default:
                break;
        }
    }

    public Fish AddFish()
    {
        Fish fish = new Fish();
        
        return fish;
    }
    public override void TriggerInteraction()
    {
        if (Define.CurrentPlayer.currentBucket == null) 
            return;
        if (Define.CurrentPlayer.currentBucket.state == Bucket.STATE.GRAB)
        {
            switch (Define.CurrentPlayer.currentBucket.contain)
            {
                case Bucket.CONTAIN.CLEANWATER:
                    SetCleanness(bucketCleannessAmount, 1);
                    Define.CurrentPlayer.currentBucket.SetContain(Bucket.CONTAIN.DIRTYWATER, null);
                    break;
                case Bucket.CONTAIN.FISH:
                    containFish.Add(AddFish());
                    break;
                default:
                    break;
            }
        }
    }
}