using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        { //player is not carrying a kitchen object
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player); // Spawn the cut kitchen object on the counter
            OnPlayerGrabbedObject?.Invoke(this, System.EventArgs.Empty);
        }
    }


}
