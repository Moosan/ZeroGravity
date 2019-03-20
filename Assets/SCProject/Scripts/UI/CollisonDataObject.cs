using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VRObjectBase))]
public class CollisonDataObject : MonoBehaviour
{
    private Rigidbody rb;
    private float TotalTime;
    public GameObject CollisionDataUI;
    private float kyori;
    private Vector3 startPosi;
    private GameObject UIObj;
    [SerializeField]
    private bool isThrow;
    [SerializeField]
    private GameObject Parent;

    public static bool isActive = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (UIObj == null)
        {
            StartCoroutine(Seisei());
        }
        if (isThrow)
        {
            OnThrow();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != "Floor") return;
        if (isThrow)
        {
            UIRender(other.contacts[0].point);
            isThrow = false;
        }
    }
    public void OnThrow(){
        if (!isActive) return;
        startPosi = this.transform.position;
        TotalTime = Time.time;
    }
    public void OfThrow()
    {
        isThrow = false;
    }
    private IEnumerator Seisei(){
        UIObj = Instantiate(CollisionDataUI, transform.position, Quaternion.identity);
        UIObj.transform.parent = Parent.transform;
        UIObj.SetActive(false);
        yield return null;
    }

    private void UIRender(Vector3 pos)
    {
        var time = Time.time - TotalTime;
        Vector3 endPosi = this.transform.position;
        kyori = Mathf.Sqrt((endPosi.x - startPosi.x) * (endPosi.x - startPosi.x) + (endPosi.z - startPosi.z) * (endPosi.z - startPosi.z));
        UIObj.GetComponent<CollisionDataUI>().SetParameter(time, kyori);
        UIObj.transform.position = pos;
        UIObj.SetActive(true);
        StartCoroutine(Seisei());
        TotalTime = 0f;
    }
}

