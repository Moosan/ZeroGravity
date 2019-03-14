using UnityEngine;
using UnityEngine.UI;
public class GravityValueUI : MonoBehaviour {
    public Text Text;
    public GravityLinearDrive Driver;
    private void Start()
    {
        ChangeText(Physics.gravity.y);
        Driver.OnGravityValueChange += (value) =>
        {
            ChangeText(value);
        };
    }

    private void ChangeText(float value)
    {
        Text.text = "現在の重力 " + value.ToString("f2") + " m/s^2";
    }
}
