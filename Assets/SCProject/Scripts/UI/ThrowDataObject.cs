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
    private float seconds;

	// Use this for initialization
	void Start () {
        rigidbody = this.GetComponent<Rigidbody>();
        StartCoroutine("loop");

    }
	
	// Update is called once per frame
	void Update () {
        seconds += Time.deltaTime;
    }
    public IEnumerator loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            OnThrow();
        }
    }
    public void OnThrow(){
        ThrowPos = this.transform.position;
        throwdata = Instantiate(ThrowDataUI, ThrowPos,new Quaternion());
        height = this.transform.position.y;
        mass = this.rigidbody.mass;
        velocity = rigidbody.velocity.magnitude;
        throwdata.GetComponent<ThrowDataUI>().SetParameter(mass, height, velocity);
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            Debug.Log("Hit");
            StopCoroutine("loop");
        }
    }
}
