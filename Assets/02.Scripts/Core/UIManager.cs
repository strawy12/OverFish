using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private InteractionUI _interactionUITemp;
    [SerializeField]
    private RectTransform timePanelRectTrm;

    [SerializeField]
    private Text baitText;
    [SerializeField]
    private Text goldText;

    [SerializeField]
    private Text aquariumFishText;
    private void Start()
    {
        GameManager.Inst.GameStart += SetBaitText;
    }


    public InteractionUI GetInteractionUI()
    {
       return Instantiate(_interactionUITemp, _interactionUITemp.transform.parent);
    }

    public void ChangeTimePanelAmount(float amount)
    {
        timePanelRectTrm.localScale = new Vector3(amount,1,1);
    }


    public void SetBaitText()
    {
        int baitCnt = DataManager.Inst.FindUpgradeData(EUpgradeDataType.BaitCount).level;
        baitText.text = $"Bait: {baitCnt}"; 
    }    
    public void SetAquariumText(int fishCnt)
    {
        int cnt = DataManager.Inst.FindUpgradeData(EUpgradeDataType.AquariumFishCount).level;
        aquariumFishText.text = $"Fish: {fishCnt} / {cnt}"; 
    }    
    
    public void SetGoldText()
    {
        goldText.text = $"Gold: {DataManager.Inst.CurrentPlayer.gold}"; 
    }
}
