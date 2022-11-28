using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : InteractionObject
{
    public enum STATE
    {
        DROP,
        GRAB,
        INTERACTING
    }
    public enum CONTAIN
    {
        NONE,
        CLEANWATER,
        DIRTYWATER,
        FISH
    }

    public STATE state = STATE.DROP;
    public CONTAIN contain = CONTAIN.NONE;

    public void Awake()
    {
        gameObject.tag = Constant.INTERACTION_TAG;  
    }
    
    public override void TriggerInteraction()
    {
        Debug.Log(state);
        if (state == STATE.INTERACTING)
        {
            return;
        }
        else if (state == STATE.DROP)
        {
            StateChange(STATE.GRAB);
            transform.SetParent(PlayerInput.Inst.BucketTransform);
            transform.position = transform.parent.position;
            transform.rotation = Quaternion.identity;
        }
        else if (state == STATE.GRAB)
        {
            StateChange(STATE.DROP);
            transform.SetParent(null);
        }
    }
    public void StateChange(STATE setValue)
    {
        state = setValue;
    }
}
