using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private InteractionObject _currentObject;

    public void Intraction()
    {
        if (_currentObject == null) return;
        _currentObject?.TriggerInteraction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.INTERACTION_TAG))
        {
            InteractionObject interactionObj = other.GetComponent<InteractionObject>();

            _currentObject = interactionObj;

            interactionObj.EnterInteraction();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.INTERACTION_TAG))
        {
            _currentObject?.ExitInteraction();
            _currentObject = null;
        }
    }

}
