using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUpgradeDataType
{
    None = -1,
    FishRodPower, // 0
    FishRodCount, // 1

    BucketPower,
    BucketCount, // 3

    AquariumPower,
    AquariumFishCount,

    BaitPower,
    BaitCount,

    ChargeOil,
    MaxOilAmount,
    End
}

[System.Serializable]
public class UpgradeData
{
    public EUpgradeDataType dataType;
    public int level;
    public int gold;

    public UpgradeData(EUpgradeDataType type)
    {
        dataType = type;
        level = 1;
        gold = 100;
    }
}

[System.Serializable]
public class PlayerData
{
    public List<UpgradeData> upgradeDataList;
    public int gold;
    public int BaitCount
    {
        get
        {
            return upgradeDataList.Find(x => x.dataType == EUpgradeDataType.BaitCount).level;
        }
        set
        {
            upgradeDataList.Find(x => x.dataType == EUpgradeDataType.BaitCount).level = value;
            UIManager.Inst.SetBaitText();
        }
    }

    public PlayerData()
    {
        upgradeDataList = new List<UpgradeData>();

        for (EUpgradeDataType type = EUpgradeDataType.FishRodPower; type < EUpgradeDataType.End; type++)
        {
            upgradeDataList.Add(new UpgradeData(type));
        }

        gold = 100;
    }

    public void UpgradeAmount(EUpgradeDataType type)
    {
        UpgradeData data = upgradeDataList.Find(x => x.dataType == type);
        data.level++;
        gold -= data.gold;

        if (type == EUpgradeDataType.BaitCount || type == EUpgradeDataType.ChargeOil)
            return;

        data.gold = (int)(data.gold* 2f);
    }
}
