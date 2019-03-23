using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : VRObjectBase {
    public GameObject Bound;
    void OnCollisionEnter(Collision collision)
    {
        var contacts = collision.contacts;
        var obj = Instantiate(Bound, contacts[0].point, new Quaternion());
        obj.GetComponent<AudioSource>().volume = collision.relativeVelocity.magnitude/10;
        Destroy(obj, 0.7f);
    }

}
