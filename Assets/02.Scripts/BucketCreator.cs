using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class BucketCreator : MonoBehaviour
{
    [SerializeField]
    private Bucket bucketPrefab;
    private List<Bucket> bucketList = new List<Bucket>();

    private void Start()
    {
        GameManager.Inst.GameStart += CreateBucket;
    }

    private void CreateBucket()
    {
        DestroyBucket();
        int count = DataManager.Inst.FindUpgradeData(EUpgradeDataType.BucketCount).level;

        for(int i = 0; i < count; i++)
        {
            Bucket bucket = Instantiate(bucketPrefab);
            bucket.transform.SetParent(null);

            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(Define.MIN_POS.x, Define.MAX_POS.x);
            pos.y = Define.MAX_POS.y;
            pos.z = Random.Range(Define.MIN_POS.z, Define.MAX_POS.z);

            bucket.transform.position = pos;
            bucketList.Add(bucket);
        }
    }

    private void DestroyBucket()
    {
        foreach(var bucket in bucketList)
        {
            Destroy(bucket.interactionUI.gameObject);
            Destroy(bucket.gameObject);
        }

        bucketList.Clear();
    }
}
