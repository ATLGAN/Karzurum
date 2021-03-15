using UnityEngine;

public interface IInteractable
{
    Player ContactPlayer { get; set; }

    InteractableType InteractableType { get; set; }

    GameObject Object { get; set; }

    string TooltipMessage { get; set; }

    void InputHandle();
}