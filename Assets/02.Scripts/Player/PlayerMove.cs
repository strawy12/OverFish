using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
    float speed = 10f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move(); 
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, 0, z).normalized;
        rigid.velocity = moveDir * speed;

        Vector3 rotatePos = new Vector3(transform.position.x, 0, transform.position.z);
        transform.Rotate(Vector3.Lerp(rotatePos, moveDir, 0.02f));
    }

}
