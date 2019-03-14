using Valve.VR.InteractionSystem;
using UnityEngine;
using UnityEngine.UI;
public class GravityLinearDrive : LinearDrive {
    [SerializeField]
    private Slider slider;
    private float valueMax;

    public delegate void GravityValueHandler(float value);
    public event GravityValueHandler OnGravityValueChange = (value) => { };

    protected override void Awake()
    {
        base.Awake();
        valueMax = slider.maxValue;
    }
    protected override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
        ChangeGravity(linearMapping.value * valueMax);
    }
    protected override void HandAttachedUpdate(Hand hand)
    {
        base.HandAttachedUpdate(hand);
        ChangeGravity(linearMapping.value * valueMax);
    }
    public void ChangeValue(float value)
    {
        linearMapping.value = value / valueMax;
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, linearMapping.value);
        ChangeGravity(value);
    }
    private void ChangeGravity(float value)
    {
        slider.value = value;
        var gravityValue = value * 2.0f * -9.81f;
        Physics.gravity = new Vector3(0, gravityValue, 0);
        OnGravityValueChange(gravityValue);
    }
}
