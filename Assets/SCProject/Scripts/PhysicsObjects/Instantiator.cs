using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour {
    private Vector3 StartPos;
    [SerializeField]
    private GameObject Prefab;
    private Rigidbody rigid;

    void Start()
    {
        StartPos = Prefab.transform.position;
        rigid = Prefab.GetComponent<Rigidbody>();
    }

    public void Instant()
    {
        rigid.velocity = new Vector3();
        Prefab.transform.SetPositionAndRotation(
            StartPos,
            new Quaternion()
            );
    }

}
