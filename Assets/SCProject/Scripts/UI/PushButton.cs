using UnityEngine;
using Valve.VR.InteractionSystem;
public class PushButton : MonoBehaviour {
    private float StartPosition;
    public float EndPosition;

    private void Start()
    {
        StartPosition = transform.localPosition.z;
    }

    public void OnPush()
    {
        transform.localPosition = GetPos(EndPosition);
    }

    public void OnPull()
    {
        transform.localPosition = GetPos(StartPosition);
    }

    private Vector3 GetPos(float zvalue)
    {
        return new Vector3(transform.localPosition.x, transform.localPosition.y, zvalue);
    }
}
