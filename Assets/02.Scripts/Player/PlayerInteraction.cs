using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private InteractionObject _currentObject;
    private Dictionary<GameObject, InteractionObject> _enterInteractionList = new Dictionary<GameObject, InteractionObject>();
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

            _enterInteractionList[interactionObj.gameObject] = interactionObj;
            interactionObj.EnterInteraction();
        }
    }

    private void LateUpdate()
    {
        if (_enterInteractionList.Count == 0) return;
        float minAngle = 999f;
        InteractionObject interactionObject = null;

        foreach (var element in _enterInteractionList)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, 10f);
            Vector3 targetPos;

            if (hit.collider != null && hit.collider.gameObject == element.Key.gameObject)
            {
                targetPos = hit.point;
            }
            else
            {
                targetPos = element.Value.transform.position;

            }

            float angle = Vector3.Angle(transform.forward, targetPos - transform.position);
            if (angle < minAngle)
            {
                minAngle = angle;
                interactionObject = _enterInteractionList[element.Key.gameObject];
            }
        }

        if (interactionObject != null)
        {
            _currentObject = interactionObject;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (_currentObject == null) return;
        if (other.gameObject.CompareTag(Constant.INTERACTION_TAG))
        {
            _enterInteractionList.Remove(other.gameObject);
            _currentObject?.ExitInteraction();
            _currentObject = null;
        }
    }

}
