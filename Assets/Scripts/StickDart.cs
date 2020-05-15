using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickDart : MonoBehaviour
{
    public GameObject Dart;
    private Rigidbody rigid;
    private Vector3 origPos;

    // Start is called before the first frame update
    void Start()
    {
        origPos = Dart.transform.position;
        rigid = Dart.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3);
        Dart.transform.position = origPos;
        rigid.constraints &= ~RigidbodyConstraints.FreezePositionX;
        rigid.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rigid.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        rigid.constraints &= ~RigidbodyConstraints.FreezeRotationX;
        rigid.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        rigid.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
    }
}
