using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/UpgradeData")]
public class UpgradeDataSO : ScriptableObject
{
    public EUpgradeDataType dataType;
    public string dataName;
    public Sprite dataSprite;
}
