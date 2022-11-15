using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private InteractionObject _currentObject;

    public void Update()
    {
        if (_currentObject == null) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentObject?.TriggerInteraction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.INTERACTION_TAG))
        {
            InteractionObject interactionObj = other.GetComponent<InteractionObject>();

            _currentObject = interactionObj;
        }
    }


}
