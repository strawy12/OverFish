using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    [SerializeField] Calculate calculate;
    [SerializeField] Text priceText;

    private void Awake()
    {
        GameManager.Inst.GameEnd += SetText;
        SetText();
    }

    private void SetText()
    {
        priceText.text = calculate.CountFishPrice().ToString();
    }
}
