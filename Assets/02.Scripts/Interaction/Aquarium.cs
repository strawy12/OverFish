using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aquarium : InteractionObject
{
    private float cleannessAmount = 0f;
    [SerializeField] Material waterMaterial;
    [SerializeField] Color currentWaterColor;
    [SerializeField] Renderer waterRenderer;
    [SerializeField] Color[] waterColors = new Color[4];
    [SerializeField] HpBar HPBAR;

    [SerializeField] private bool isPollution = false;
    [SerializeField] private float MaxCleanness = 100f;
    [SerializeField] private float curCleanness { get { return cleannessAmount; } set { cleannessAmount = Mathf.Clamp(value, 0f, MaxCleanness); } }
    [SerializeField] private float pollution = 1f;
    [SerializeField] private float bucketCleannessAmount = 10f;

    [SerializeField] private GameObject fishPref;
    private List<GameObject> fishbjectList;
     private float polluteDelay = 1f;

    public List<Fish> containFish;

    protected override void Awake()
    {
        base.Awake();
        HPBAR = GetComponent<HpBar>();
        containFish = new List<Fish>();
        fishbjectList = new List<GameObject>();
    }
    protected override void Start()
    {
        base.Start();

        GameManager.Inst.GameStart += GameStart;
    }

    public void GameStart()
    {
        fishbjectList.ForEach(x => Destroy(x));
        fishbjectList.Clear();
        containFish.Clear();
        waterMaterial = waterRenderer.material;
        waterColors[0] = waterMaterial.color;
        currentWaterColor = waterColors[0];

        curCleanness = MaxCleanness;

        HPBAR.Init();
        UIManager.Inst.SetAquariumText(0);

        StartCoroutine(Pollute());
    }

    private void CreateFishObject()
    {
        GameObject fish = Instantiate(fishPref, fishPref.transform.parent);
        fish.transform.localPosition = Vector3.zero;
        fish.gameObject.SetActive(true);
        fishbjectList.Add(fish);
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
            SetUI();

            float delay = polluteDelay + DataManager.Inst.FindUpgradeData(EUpgradeDataType.AquariumPower).level * 0.5f;
            yield return new WaitForSeconds(delay);
        }
    }
    void IncreasePollution(float value)
    {
        SetCleanness(value, 2);
        CheckPollution();
        SetColor();
    }

    public void SetUI()
    {
        HPBAR.Setsize(MaxCleanness, curCleanness, pollution, 2);
    }

    public void SetColor()
    {
        int i = (int)(MaxCleanness / curCleanness / 25);
        Debug.Log(i);
        currentWaterColor = waterColors[i];
        waterMaterial.color = currentWaterColor;
    }

    public void CheckPollution()
    {
        if (Mathf.Abs(MaxCleanness / 2) >= curCleanness)
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
                curCleanness = value;
                break;
            case 1:
                curCleanness += value;
                break;
            case 2:
                curCleanness -= value;
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
                    SoundManager.Inst.TurnAudio(SoundManager.EFFECT.INCLUDEINGWATER);
                    Define.CurrentPlayer.currentBucket.SetContain(Bucket.CONTAIN.DIRTYWATER, null);
                    break;
                case Bucket.CONTAIN.FISH:
                    SoundManager.Inst.TurnAudio(SoundManager.EFFECT.DECLUDINGWATER);
                    
                    Define.CurrentPlayer.currentBucket.SetContain(Bucket.CONTAIN.NONE, null);
                    containFish.Add(AddFish());
                    CreateFishObject();

                    UIManager.Inst.SetAquariumText(containFish.Count);

                    if (DataManager.Inst.FindUpgradeData(EUpgradeDataType.AquariumFishCount).level <= containFish.Count)
                    {
                        GameManager.Inst.ImmediatelyStop();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}