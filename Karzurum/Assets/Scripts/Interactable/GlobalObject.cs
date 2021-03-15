using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObject : MonoBehaviour, IInteractable
{
    public Player ContactPlayer { get; set; }

    [SerializeField] InteractableType type;
    public InteractableType InteractableType { get => type; set => type = value; }
    
    public GameObject Object { get; set; }

    [SerializeField] string m_UIMessage;
    public string TooltipMessage { get => m_UIMessage; set => m_UIMessage = value; }

    private void Start()
    {
        Object = gameObject;
    }

    public void InputHandle()
    {
        
    }
}
