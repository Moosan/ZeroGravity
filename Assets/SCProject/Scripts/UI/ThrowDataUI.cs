using UnityEngine;
using UnityEngine.UI;
public class ThrowDataUI : MonoBehaviour {
    public Transform StartTransform;
    public Transform EndTransform;
    public Text MassText;
    public Text HeightText;
    public Text VelocityText;
    private LineRenderer Line;
    private void Awake()
    {
        Line = GetComponent<LineRenderer>();
        Line.SetPositions(new Vector3[] { StartTransform.position, EndTransform.position });
    }
    public void SetParameter(float mass,float height,float velocity)
    {
        MassText.text = "質量: " + mass.ToString("f2") + " kg";
        HeightText.text = "高さ: " + height.ToString("f2") + " m";
        VelocityText.text = "速さ: " + velocity.ToString("f2") + " m/s";
    }
}
