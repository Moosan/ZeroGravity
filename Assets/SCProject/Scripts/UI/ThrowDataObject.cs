using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDataObject : VRObjectBase {
    public GameObject ThrowDataUI;
    private float mass;
    private float height;
    private float velocity;
    private Rigidbody rigidbody;
    private GameObject throwdata;
    private Vector3 ThrowPos;

	// Use this for initialization
	void Start () {
        rigidbody = this.GetComponent<Rigidbody>();
        StartCoroutine("OnThrow");
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public IEnumerator OnThrow(){
        ThrowPos = this.transform.position;
        throwdata = Instantiate(ThrowDataUI, ThrowPos,new Quaternion());
        height = this.transform.position.y;
        mass = this.rigidbody.mass;
        velocity = rigidbody.velocity.magnitude;
        throwdata.GetComponent<ThrowDataUI>().SetParameter(mass, height, velocity);
        yield return new WaitForSeconds(0.0f);
    }
}
