using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerDataType
{
    None = -1,
    FishRodPower,
    FishRodCount,

    BucketPower,
    BucketCount,

    AquariumPower,
    AquariumFishCount,

    BaitPower,
    BaitCount,

    MaxOilAmount,
    End
}

[System.Serializable]
public class UpgradeData
{
    public EPlayerDataType dataType;
    public int level;

    public UpgradeData(EPlayerDataType type)
    {
        dataType = type;
        level = 0;
    }
}

[System.Serializable]
public class PlayerData
{
    public List<UpgradeData> upgradeDataList;

    public PlayerData()
    {
        upgradeDataList = new List<UpgradeData>();

        for (EPlayerDataType type = EPlayerDataType.FishRodPower; type < EPlayerDataType.End; type++)
        {
            upgradeDataList.Add(new UpgradeData(type));
        }
    }

    public void UpgradeAmount(EPlayerDataType type)
    {
        UpgradeData data = upgradeDataList.Find(x => x.dataType == type);
        data.level++;
    }
}
