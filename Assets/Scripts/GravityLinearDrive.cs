using Valve.VR.InteractionSystem;
using UnityEngine;
using UnityEngine.UI;
public class GravityLinearDrive : LinearDrive {
    [SerializeField]
    private Slider slider;
    protected override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
        slider.value = linearMapping.value;
        Physics.gravity = new Vector3(0, slider.value * 2.0f * -9.81f, 0); 
    }
}
