using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    Vector3 _currentDir = Vector3.zero;
    public UnityEvent OnTriggerInteraction;
    public UnityEvent<Vector3> OnInputMovement;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnTriggerInteraction?.Invoke();
        }

        MoveInput();
    }

    public void MoveInput()
    {
        float x = Input.GetAxisRaw(Constant.HORIZONTAL);
        float z = Input.GetAxisRaw(Constant.VERTICAL);

        _currentDir = new Vector3(x, 0, z).normalized;
        OnInputMovement?.Invoke(_currentDir);
    }
}
