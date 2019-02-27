using UnityEngine;
using UnityEngine.UI;
public class CollisionDataUI : MonoBehaviour {
    public Transform StartTransform;
    public Transform EndTransform;
    public Text TimeText;
    public Text DistanceText;
    private LineRenderer Line;
    private void Awake()
    {
        Line = GetComponent<LineRenderer>();
        Line.SetPositions(new Vector3[] { StartTransform.position, EndTransform.position });
    }
    public void SetParameter(float time,float distance)
    {
        TimeText.text = "滞空時間: " + time.ToString("f2") + " s";
        DistanceText.text = "飛距離　: " + distance.ToString("f2") + " m";
    }
}
