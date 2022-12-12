using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private EUpgradeDataType dataType;
    private UpgradeData currentData;

    [SerializeField]
    private Text dataNameText;
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private Text effectText;
    [SerializeField]
    private Text goldText;
    [SerializeField]
    private Image dataImage;
    [SerializeField]
    private Button upgradeBtn;

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        UpgradeDataSO dataSO = Resources.Load<UpgradeDataSO>(string.Format("{0:00}_UpgradeData", (int)dataType));
        currentData = DataManager.Inst.FindUpgradeData(dataType);

        dataNameText.text = dataSO.dataName;
        dataImage.sprite = dataSO.dataSprite;
        effectText.text = "";

        upgradeBtn.onClick.AddListener(UpgradeData);
        ChangeValue();
    }

    public void ChangeValue()
    {
        if (dataType == EUpgradeDataType.BaitCount)
        {
            levelText.text = $"{currentData.level}°³";
        }
        else if(dataType == EUpgradeDataType.ChargeOil)
        {
            levelText.text = $"{currentData.level}L";
        }
        else
        {
            levelText.text = $"Lv.{currentData.level}";
        }
        goldText.text = currentData.gold.ToString();
    }

    public void UpgradeData()
    {
        if (DataManager.Inst.CurrentPlayer.gold < currentData.gold)
        {
            Debug.Log("µ· ºÎÁ·");
            return;
        }

        DataManager.Inst.CurrentPlayer.UpgradeAmount(dataType);
        ChangeValue();
    }


}
