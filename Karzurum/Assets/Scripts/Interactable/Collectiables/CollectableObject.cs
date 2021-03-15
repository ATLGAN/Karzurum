using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableObject : MonoBehaviour, IInteractable
{
    public int coinAmount;

    public bool randomCoin;
    public int minCoin;
    public int maxCoin;

    public bool destroyOnCollect;

    public UnityEvent OnCollected;

    public Player ContactPlayer { get; set ; }
    public InteractableType InteractableType { get; set; }
    public GameObject Object { get; set; }

    [SerializeField] string m_UIMessage;
    public string TooltipMessage { get => m_UIMessage; set => m_UIMessage = value; }

    private void OnEnable()
    {
        InteractableType = InteractableType.Collectable;

        if (minCoin < 0)
            minCoin = 0;
        if (maxCoin < 0)
            maxCoin = 0;
    }
    public void InputHandle()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnCollected.Invoke();

            if (destroyOnCollect)
                Destroy(gameObject);
        }
    }
}
