using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private InteractionUI _interactionUITemp;   
    public InteractionUI GetInteractionUI()
    {
       return Instantiate(_interactionUITemp, _interactionUITemp.transform.parent);
    }
}
