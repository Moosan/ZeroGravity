using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour {
    private Vector3 StartPos;
    [SerializeField]
    private GameObject Prefab;
    [SerializeField]
    private float LifeTime;
    private float TimeCount;
    private void Awake()
    {
        StartPos = transform.position;
        TimeCount = 0;
    }

    public void Instant()
    {
        var obj = (GameObject)Instantiate(Prefab, StartPos, new Quaternion());
        obj.transform.parent = this.transform;

    }

    private void Update()
    {
        TimeCount += Time.deltaTime;
        if(TimeCount >= LifeTime)
        {
            TimeCount = 0;
            if (!ExistChild())
            {
                Instant();
            }
        }
    }

    private bool ExistChild()
    {
        return transform.childCount >= 1;
    }
}
