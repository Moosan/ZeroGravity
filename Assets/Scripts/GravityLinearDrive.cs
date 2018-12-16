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
    }
}
