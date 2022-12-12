using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Image maxHPBar;
    [SerializeField] Image curHPBar;

    private RectTransform maxHPTrs;
    private RectTransform curHPTrs;

    public void Awake()
    {
        maxHPTrs = maxHPBar.rectTransform;
        curHPTrs = curHPBar.rectTransform;

        curHPTrs.sizeDelta = maxHPTrs.sizeDelta;
    }
    public void Setsize(float maxValue, float curValue, float setValue, int type)
    {
        switch(type)
        {
            case 0:
                curValue = setValue;
                break;
            case 1:
                curValue += setValue;
                break;
            case 2:
                curValue -= setValue;
                break;
        }
        float size = curValue / maxValue;
        curHPTrs.sizeDelta = new Vector2(maxHPTrs.sizeDelta.x * size, maxHPTrs.sizeDelta.y);
    }
}
