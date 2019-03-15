using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VRObjectBase))]
public class ThrowDataObject :MonoBehaviour{
    public GameObject ThrowDataUI;
    private float mass;
    private float height;
    private float velocity;
    private Rigidbody Rigidbody;
    private GameObject throwdata;
    private Vector3 ThrowPos;

    [SerializeField]
    private bool IsThrow;

	// Use this for initialization
	void Awake () {
        Rigidbody = this.GetComponent<Rigidbody>();
        
    }
    private void Start()
    {
        if (IsThrow)
        {
            OnThrow();
        }
    }

    public void OnThrow(){
        ThrowPos = this.transform.position;
        throwdata = Instantiate(ThrowDataUI, ThrowPos,Quaternion.identity);
        height = this.transform.position.y;
        mass = this.Rigidbody.mass;
        velocity = Rigidbody.velocity.magnitude;
        throwdata.GetComponent<ThrowDataUI>().SetParameter(mass, height, velocity);
        throwdata.SetActive(true);
        IsThrow = false;
    }
}
