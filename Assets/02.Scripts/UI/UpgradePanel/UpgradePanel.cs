using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private EUpgradeDataType dataType;
    protected UpgradeData currentData;

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

    [SerializeField]
    protected int maxLevel = 10;

    private Coroutine delayCo;

    private void Awake()
    {
        Init();
    }
    public virtual void Init()
    {
        UpgradeDataSO dataSO = Resources.Load<UpgradeDataSO>(string.Format("{0:00}_UpgradeData", (int)dataType));
        currentData = DataManager.Inst.FindUpgradeData(dataType);

        dataNameText.text = dataSO.dataName;
        dataImage.sprite = dataSO.dataSprite;
        effectText.text = "";

        upgradeBtn.onClick.AddListener(UpgradeData);
        ChangeValue();
    }

    public virtual void ChangeValue()
    {
        if (dataType == EUpgradeDataType.BaitCount)
        {
            levelText.text = $"{currentData.level}개";
        }
        else if(dataType == EUpgradeDataType.ChargeOil)
        {
            UpgradeData maxOilData = DataManager.Inst.FindUpgradeData(EUpgradeDataType.MaxOilAmount);
            levelText.text = $"{currentData.level % (Constant.DEFAULT_MAX_OIL_COUNT + maxOilData.level)}L";
        }
        else
        {
            levelText.text = $"Lv.{currentData.level}";
        }
        goldText.text = currentData.gold.ToString();
    }

    public virtual void UpgradeData()
    {
        if (maxLevel != 0 && maxLevel < currentData.level)
        {
            goldText.text = "최대레벨";
            Debug.Log("최대레벨");
            return;
        }
        if (DataManager.Inst.CurrentPlayer.gold < currentData.gold)
        {
            if (delayCo != null) return;
            delayCo =StartCoroutine(NotBuyDelay());
            Debug.Log("돈 부족");
            return;
        }

        DataManager.Inst.CurrentPlayer.UpgradeAmount(dataType);
        ChangeValue();
    }

    private IEnumerator NotBuyDelay(Vector2? v = null)
    {
        string text = goldText.text;
        Color color = goldText.color;
        goldText.color = Color.red;
        goldText.text = "돈 부족";
        yield return new WaitForSeconds(0.75f);
        goldText.text = text;
        goldText.color = color;
        delayCo = null;
    }
}
