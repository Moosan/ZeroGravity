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
    [SerializeField]
    private GameObject Parent;

    private List<GameObject> ObjList;

    public static bool isActive = false;

    public Transform player;

    public float KyoriThre;

    private void Start()
    {
        Rigidbody = this.GetComponent<Rigidbody>();
        ObjList = new List<GameObject>();
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
        if (!isActive) return;
        StartCoroutine("Delete", ObjList.ToArray());
        ObjList.Clear();
        MakeUI();
        StartCoroutine("loop");
        IsThrow = false;
    }

    public void OfThrow()
    {
        StopCoroutine("loop");
    }

    private void MakeUI(){
        if ((ThrowPos - transform.position).magnitude < KyoriThre) return;
        ThrowPos = this.transform.position;
        throwdata = Instantiate(ThrowDataUI, ThrowPos,Quaternion.identity);
        ObjList.Add(throwdata);
        throwdata.transform.LookAt(player);
        throwdata.transform.parent = Parent.transform;
        height = this.transform.position.y;
        mass = this.Rigidbody.mass;
        velocity = Rigidbody.velocity.magnitude;
        throwdata.GetComponent<ThrowDataUI>().SetParameter(mass, height, velocity);
        throwdata.SetActive(true);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            OfThrow();
        }
    }
    public IEnumerator Delete(GameObject[] array) {
        foreach (var obj in array) {
            Destroy(obj);
        }
        yield return null;
    }
}
