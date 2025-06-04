using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged; // Event to notify when cutting progress changes
    //public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray; // Scriptable Object that holds data about the cutting counter

    private int cuttingProgress; // Variable to track the cutting progress

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchenobj here
            if (player.HasKitchenObject())
            {
                //player has a kitchen object
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //plater carring something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0; // Reset cutting progress when a new object is placed on the counter

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    }); // Notify that cutting progress has changed
                }
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
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //there is a kitchenonj obj and can be cut
            cuttingProgress++; // Increment cutting progress
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax // Notify that cutting progress has changed
            });
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {


                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf(); // Destroy the kitchen object on the counter


                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this); // Spawn the cut kitchen object on the counter

            }
        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null; // Check if there is a recipe with the given input
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO); // Get the CuttingRecipeSO for the given input
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output; // Return the output kitchen object for the given input
        }
        else
        {

            return null; // Return null if no matching recipe is found

        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO; // Return the CuttingRecipeSO for the given input
            }
        }
        return null; // Return null if no matching recipe is found
    }
}

