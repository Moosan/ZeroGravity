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
    private float DelayTime;

    [SerializeField]
    private bool IsThrow;

    private void Start()
    {
        Rigidbody = this.GetComponent<Rigidbody>();
        if (IsThrow)
        {
            OnThrow();
        }
    }
    public IEnumerator loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(DelayTime);
            MakeUI();
        }
    }
    public void OnThrow()
    {
        MakeUI();
        StartCoroutine("loop");
    }

    private void MakeUI(){
        ThrowPos = this.transform.position;
        throwdata = Instantiate(ThrowDataUI, ThrowPos,Quaternion.identity);
        height = this.transform.position.y;
        mass = this.Rigidbody.mass;
        velocity = Rigidbody.velocity.magnitude;
        throwdata.GetComponent<ThrowDataUI>().SetParameter(mass, height, velocity);
        throwdata.SetActive(true);
        IsThrow = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            StopCoroutine("loop");
        }
    }
}
