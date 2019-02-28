using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPoint : VRObjectBase
{
    private Rigidbody rb;
    private GameObject obj2;
    private float TotalTime;
    public GameObject Point;
    private bool create;
    private bool count;
    private float kyori;
    private Vector3 startPosi;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        obj2 = GameObject.Find("Cube");
        create = true;
        count = false;
        //rb.AddForce(Vector3.right*5f);
        //OnThrow();
    }

    // Update is called once per frame
    void Update()
    {
        if (count)
        {
            TotalTime += Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (create)
        {
           var obj =  Instantiate(Point, transform.position, Quaternion.identity);
            Debug.Log(TotalTime);
            Vector3 endPosi = this.transform.position;
            Debug.Log(endPosi);
            kyori = Mathf.Sqrt((endPosi.x - startPosi.x) * (endPosi.x - startPosi.x)+(endPosi.z - startPosi.z)* (endPosi.z - startPosi.z));
            obj.GetComponent<CollisionDataUI>().SetParameter(TotalTime, kyori);
            create = false;
            count = false;
            TotalTime = 0f;
        }
    }
    public void OnThrow(){
        startPosi = this.transform.position;
        Debug.Log(startPosi);
        count = true;
    }
}

