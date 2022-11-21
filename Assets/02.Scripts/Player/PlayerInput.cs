using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    Vector3 _currentDir = Vector3.zero;
    public UnityEvent OnTriggerInteraction;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnTriggerInteraction?.Invoke();
        }
    }

    public Vector3 MoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        _currentDir = new Vector3(x, 0, z).normalized;
        return _currentDir;
    }
}
