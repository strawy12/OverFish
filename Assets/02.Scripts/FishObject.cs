using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishObject : MonoBehaviour
{
    [SerializeField]
    private float moveSeed; 
    [SerializeField]
    private float rotateSeed;

    [SerializeField]
    private BoxCollider waterCol;

    private Vector3 maxPos;
    private Vector3 minPos;

    private void Start()
    {
        minPos = waterCol.bounds.min;
        maxPos = waterCol.bounds.max;

        StartCoroutine(MoveCoroutine());
    }
     
    private IEnumerator MoveCoroutine()
    {
        while(true)
        {
            Vector3 targetPos = Vector3.zero;
            targetPos.x = Random.Range(minPos.x, maxPos.x);
            targetPos.y = Random.Range(minPos.y, maxPos.y);
            targetPos.z = Random.Range(minPos.z, maxPos.z);

            Vector3 targetDir = targetPos - transform.position;

            transform.DOLookAt(targetDir.normalized, rotateSeed);
            yield return new WaitForSeconds(rotateSeed);
            transform.DOMove(targetPos, moveSeed);
            yield return new WaitForSeconds(moveSeed );
        }
    }

}
