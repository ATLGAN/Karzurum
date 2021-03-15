using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Status")]
    public int healt = 100;
    public int maxHealt = 100;
    public int energy = 100;
    public int bodyTemp = 36;

    public Transform handTransform;

    public float detectableSphereRadius = 0.5f;
    public float interactDistance = 3;
    public float speed { get; private set; }

    public LayerMask interactableLayers;

    [Tooltip("Types that allow interaction when carrying an object.")]
    public InteractableType[] allowTypesWhenCarrying;

    public bool isCarryingObject { get; set; }
    public bool isInteract { get; private set; }

    RaycastHit[] hits;
    RaycastHit raycastHit;
    Camera mainCamera;
    Vector3 lastPosition;

    public IInteractable InteractableObject { get; private set; }
    public IInteractable CarryingObject { get; set; }

    private void OnEnable()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (isInteract)
        {
            ReferenceKeeper.Instance.ref_UIManager.ChangeCrosshairColor(Color.green);
            InteractableObject.InputHandle();
        }
        else
        {
            ReferenceKeeper.Instance.ref_UIManager.SetDefaultCrosshairColor();
            ReferenceKeeper.Instance.ref_UIManager.UpdateTooltipMessage("");

            if (CarryingObject != null)
            {
                CarryingObject.InputHandle();
            }
        }
    }
    Vector3 rayPoint;
    private void FixedUpdate()
    {
        speed = (transform.position - lastPosition).magnitude / Time.fixedDeltaTime;
        lastPosition = transform.position;

#if UNITY_EDITOR
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * interactDistance, Color.red);
#endif
        isInteract = false;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out raycastHit, interactDistance, interactableLayers, QueryTriggerInteraction.Ignore))
        {
            rayPoint = raycastHit.point;
            hits = Physics.SphereCastAll(raycastHit.point, detectableSphereRadius, Vector3.up, interactableLayers);
            if (hits != null)
            {
                GameObject nearObject = UtilitiesMethods.GetNearestObject(raycastHit.point, hits.Select(x => x.collider.gameObject).ToArray());
                if (nearObject)
                {
                    InteractableObject = nearObject.GetComponent<IInteractable>();
                    if (InteractableObject != null)
                    {
                        if (isCarryingObject)
                        {
                            if (allowTypesWhenCarrying == null || !allowTypesWhenCarrying.Contains(InteractableObject.InteractableType))
                            {
                                return;
                            }
                        }
                        ReferenceKeeper.Instance.ref_UIManager.UpdateTooltipMessage(InteractableObject.TooltipMessage);
                        InteractableObject.ContactPlayer = this;
                        isInteract = true;
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(rayPoint, detectableSphereRadius);
    }
}
