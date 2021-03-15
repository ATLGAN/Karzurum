using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrippableObject : MonoBehaviour, IInteractable
{
    public KeyCode grapKey = KeyCode.E;
    public KeyCode breakeKey = KeyCode.E;
    public KeyCode throwKey = KeyCode.F;

    public float pushForceAmount = 2000;

    public Player ContactPlayer { get; set; }

    public InteractableType InteractableType { get; set; }
    public GameObject Object { get; set; }

    [SerializeField] string m_UIMessage;
    public string TooltipMessage { get => m_UIMessage; set => m_UIMessage = value; }

    int oldLayer;
    bool isGrapped;

    Transform handTransform;
    Rigidbody rigidbody;

    private void OnEnable()
    {
        oldLayer = gameObject.layer;

        rigidbody = GetComponent<Rigidbody>();

        Object = gameObject;
        InteractableType = InteractableType.Object;
    }
    public void InputHandle()
    {
        if (isGrapped)
        {
            if (ContactPlayer.isInteract)
                return;
        }
        if (Input.GetKeyDown(grapKey))
        {
            if (ContactPlayer != null)
            {
                handTransform = ContactPlayer.handTransform;
                if (handTransform != null)
                {
                    if (!isGrapped)
                        GrapHand();
                    else
                        BreakHand();
                }
            }
        }
        if (Input.GetKeyDown(throwKey))
        {
            Vector3 direction = handTransform.forward;
            BreakHand();
            rigidbody.AddForce(direction * pushForceAmount, ForceMode.Force);
            rigidbody.angularVelocity = Vector3.one;
        }
    }
    void GrapHand()
    {
        if(isGrapped) return;

        rigidbody.isKinematic = true;
        transform.SetParent(handTransform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        isGrapped = true;
        ContactPlayer.isCarryingObject = true;
        ContactPlayer.CarryingObject = this;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
    public void BreakHand(bool _destroy = false)
    {
        if(!isGrapped) return;

        rigidbody.isKinematic = false;
        transform.SetParent(null);

        isGrapped = false;
        ContactPlayer.isCarryingObject = false;
        ContactPlayer.CarryingObject = null;

        gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(oldLayer));

        if (_destroy)
        {
            Destroy(gameObject);
        }
    }
}
