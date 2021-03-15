using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyIt : MonoBehaviour, IInteractable
{
    public GameObject uiObject;

    public Player ContactPlayer { get; set ; }
    public InteractableType InteractableType { get; set; }
    public GameObject Object { get; set; }

    [SerializeField] string m_UIMessage;
    public string TooltipMessage { get => m_UIMessage; set => m_UIMessage = value; }


    public KeyCode key = KeyCode.T;
	public UnityEvent onDead;

    public void InputHandle()
    {
        if (Input.GetKeyDown(key))
        {
			onDead.Invoke();
            Destroy(gameObject);
        }
    }

    public void ShowInteractUI()
    {
        //uiObject?.SetActive(true);
    }

    public void HideInteractUI()
    {
        //uiObject?.SetActive(false);
    }
}
