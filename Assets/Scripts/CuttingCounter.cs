using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchenobj here
            if (player.HasKitchenObject())
            {
                //player has a kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player does not have a kitchen object
            }
        }
        else
        {
            //there is a kitchenobj here
            if (player.HasKitchenObject())
            {
                //player is carrying something

            }
            else
            {
                //player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject())
        {
            //there is a kitchenonj obj

    }

}
