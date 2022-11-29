using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : InteractionObject
{
    public override void TriggerInteraction()
    {
        if(Define.CurrentPlayer.currentBucket != null)
        {
            Bucket bucket = Define.CurrentPlayer.currentBucket;
            switch (bucket.contain)
            {
                case Bucket.CONTAIN.DIRTYWATER:
                case Bucket.CONTAIN.FISH:
                    bucket.SetContain(Bucket.CONTAIN.NONE, null);
                    break;

                case Bucket.CONTAIN.NONE:
                    bucket.SetContain(Bucket.CONTAIN.CLEANWATER, null);
                    break;

            }
        }
    } 
}
