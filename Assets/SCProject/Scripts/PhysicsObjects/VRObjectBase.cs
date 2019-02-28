﻿using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;
using Valve.VR;
/// <summary>
/// VR内で持つことの出来るオブジェクトの基底クラス
/// </summary>
public abstract class VRObjectBase : MonoBehaviour
{

    //抽象クラス

    [SerializeField]
    private VRObjectMode VRObjectMode;

    [SerializeField]
    private bool UseGravity;

    [SerializeField]
    private float Mass;

    public ReleaseStyle ReleaseStyle;

    [SerializeField]
    private string ObjectTag = "VRObject";
    
    private SteamVR_Action_Boolean attachActionBool= SteamVR_Input.__actions_default_in_GrabPinch;

    //掴んだら起こるイベント
    [SerializeField]
    private UnityEvent onPickUp;

    //離したら起こるイベント
    [SerializeField]
    private UnityEvent onThrowAway;

    //触れたら起こるイベント
    [SerializeField]
    private UnityEvent onHandHoverBegin;

    //触れるのをやめたら起こるイベント
    [SerializeField]
    private UnityEvent onHandHoverEnd;

    //アタッチしたら呼ばれるイベント
    [SerializeField]
    private UnityEvent onAttachedToHand;

    //ディタッチしたら呼ばれるイベント
    [SerializeField]
    private UnityEvent onDetachedFromHand;
    public Rigidbody rigidBody { get; set; }

    protected Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);

    public virtual void Awake()
    {
        var collider = GetComponent<Collider>();
        if (collider == null)
        {
            Debug.LogError(gameObject.name + "にColliderを付けてください。");
            return;
        }


        rigidBody = GetComponent<Rigidbody>();

        if (VRObjectMode != VRObjectMode.NeverMove)
        {
            if (ObjectTag == "") { transform.tag = "VRObject"; }
            else { transform.tag = ObjectTag; }

            if (rigidBody == null)
            {
                rigidBody = gameObject.AddComponent<Rigidbody>();
            }
            rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rigidBody.useGravity = UseGravity;
            if (Mass != 0)
            {
                rigidBody.mass = Mass;
            }

            //VelocityEstimat追加
            gameObject.AddComponent<VelocityEstimator>();

            //Interactable追加
            gameObject.AddComponent<Interactable>();

            if (VRObjectMode == VRObjectMode.Grabable)
            {
                //Throwable追加
                Throwable thro = gameObject.AddComponent<Throwable>();
                thro.onPickUp = onPickUp;
                thro.onDetachFromHand = onThrowAway;
                thro.releaseVelocityStyle = ReleaseStyle;

                //Attachイベント消去
                onAttachedToHand = new UnityEvent();
                onDetachedFromHand = new UnityEvent();
            }

            //InteractableHoverEvents追加
            InteractableHoverEvents ihe = gameObject.AddComponent<InteractableHoverEvents>();
            ihe.onHandHoverBegin = onHandHoverBegin;
            ihe.onHandHoverEnd = onHandHoverEnd;
            if (VRObjectMode != VRObjectMode.Attachable)
            {
                //Attachイベント消去
                onAttachedToHand = new UnityEvent();
                onDetachedFromHand = new UnityEvent();
            }
            ihe.onAttachedToHand = onAttachedToHand;
            ihe.onDetachedFromHand = onDetachedFromHand;
        }
        else
        {
            DestroyImmediate(rigidBody);
        }
    }

    public virtual void HandHoverUpdate(Hand hand)
    {
        if (VRObjectMode == VRObjectMode.Attachable)
        {
            if (attachActionBool.GetStateDown(hand.handType)/*|| ((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))*/)
            {
                if (hand.currentAttachedObject != gameObject)
                {
                    hand.HoverLock(GetComponent<Interactable>());
                    hand.AttachObject(gameObject,GrabTypes.Trigger, attachmentFlags);
                    rigidBody.useGravity = false;
                    rigidBody.isKinematic = true;
                }
                else
                {
                    hand.DetachObject(gameObject);
                    hand.HoverUnlock(GetComponent<Interactable>());
                    rigidBody.useGravity = UseGravity;
                    rigidBody.isKinematic = false;
                }
            }
        }
    }

    public VRObjectMode GetVRObjectMode()
    {
        return this.VRObjectMode;
    }

    public void SetVRObjectMode(VRObjectMode mode)
    {
        this.VRObjectMode = mode;

        if (VRObjectMode != VRObjectMode.NeverMove)
        {
            transform.tag = "VRObect";

            if (rigidBody == null)
            {
                rigidBody = gameObject.AddComponent<Rigidbody>();
            }
            rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rigidBody.useGravity = UseGravity;
            if (Mass != 0)
            {
                rigidBody.mass = Mass;
            }

            if (VRObjectMode == VRObjectMode.Grabable)
            {
                //Throwable追加
                Throwable thro = gameObject.GetComponent<Throwable>();
                if (!thro) thro = gameObject.AddComponent<Throwable>();
                thro.onPickUp = onPickUp;
                thro.onDetachFromHand = onThrowAway;

                //Attachイベント消去
                onAttachedToHand = new UnityEvent();
                onDetachedFromHand = new UnityEvent();
            }
            else
            {
                Throwable thro = gameObject.GetComponent<Throwable>();
                if (thro) Destroy(thro);
            }

            //InteractableHoverEvents追加
            InteractableHoverEvents ihe = gameObject.GetComponent<InteractableHoverEvents>();
            ihe.onHandHoverBegin = onHandHoverBegin;
            ihe.onHandHoverEnd = onHandHoverEnd;
            if (VRObjectMode != VRObjectMode.Attachable)
            {
                //Attachイベント消去
                onAttachedToHand = new UnityEvent();
                onDetachedFromHand = new UnityEvent();
            }
            ihe.onAttachedToHand = onAttachedToHand;
            ihe.onDetachedFromHand = onDetachedFromHand;
        }
        else
        {
            DestroyImmediate(rigidBody);
            Throwable thro = gameObject.GetComponent<Throwable>();
            if (thro) Destroy(thro);
        }
    }
    public virtual void OnDestroy()
    {
        if (transform.parent != null)
        {
            if (transform.parent.GetComponent<Hand>() != null)
            {
                transform.parent.GetComponent<Hand>().DetachObject(gameObject);
            }
        }
    }
}
