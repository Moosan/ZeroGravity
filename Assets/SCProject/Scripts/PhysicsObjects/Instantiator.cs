using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour {
    private Vector3 StartPos;
    [SerializeField]
    private GameObject Prefab;
    private Rigidbody rigid;

    private CollisonDataObject data1;
    private ThrowDataObject data2;
    void Start()
    {
        StartPos = Prefab.transform.position;
        rigid = Prefab.GetComponent<Rigidbody>();
        data1 = Prefab.GetComponent<CollisonDataObject>();
        data2 = Prefab.GetComponent<ThrowDataObject>();
    }

    public void Instant()
    {
        rigid.velocity = new Vector3();
        rigid.angularVelocity = new Vector3();
        Prefab.transform.SetPositionAndRotation(
            StartPos,
            new Quaternion()
            );
        data1.OfThrow();
        data2.OfThrow();
    }

}
