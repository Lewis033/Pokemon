using System;
using UnityEngine;

public class PCTerminal : MonoBehaviour, IInteractable
{
    public static event Action OnPCOpened;

    public void Interact(PlayerController player)
    {
        OnPCOpened?.Invoke();
    }
}