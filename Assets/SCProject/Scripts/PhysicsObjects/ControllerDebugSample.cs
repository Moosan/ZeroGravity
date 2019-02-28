using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class ControllerDebugSample : VRObjectBase
{
    [SerializeField]//なんのボタンを受け取るかをインスペクターで決めれる
    private SteamVR_Action_Boolean actionBool;
    public override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
        var _handtype = hand.handType;
        if (actionBool.GetStateDown(_handtype)) {
            Debug.Log(_handtype.ToString() + "の手でぼたんが押された");
        }
        if (actionBool.GetState(_handtype))
        {
            Debug.Log(_handtype.ToString() + "の手でぼたんが押されている");
        }
        if (actionBool.GetStateUp(_handtype))
        {
            Debug.Log(_handtype.ToString() + "の手でぼたんが押し終わった");
        }
    }
}
