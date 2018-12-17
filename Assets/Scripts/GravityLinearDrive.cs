using Valve.VR.InteractionSystem;
using UnityEngine;
using UnityEngine.UI;
public class GravityLinearDrive : LinearDrive {
    [SerializeField]
    private Slider slider;
    private float valueMax;
    protected override void Awake()
    {
        base.Awake();
        valueMax = slider.maxValue;
    }
    protected override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
        var value = linearMapping.value * valueMax;
        slider.value = value;
        Physics.gravity = new Vector3(0, value * 2.0f * -9.81f, 0);
    }
}
