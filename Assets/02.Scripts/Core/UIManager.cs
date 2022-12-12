using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private InteractionUI _interactionUITemp;
    [SerializeField]
    private RectTransform timePanelRectTrm;

    public InteractionUI GetInteractionUI()
    {
       return Instantiate(_interactionUITemp, _interactionUITemp.transform.parent);
    }

    public void ChangeTimePanelAmount(float amount)
    {
        timePanelRectTrm.localScale = new Vector3(amount,1,1);
    }

}
