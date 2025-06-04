using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged; // Event to notify when the state changes
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state; // The current state of the stove counter
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }


    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;


    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;

    }
    private void Update()
    {
        if ((HasKitchenObject()))
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:

                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax // Calculate the normalized progress
                    }); // Notify that frying progress has changed

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //fried                                        
                        GetKitchenObject().DestroySelf(); // Destroy the input kitchen object
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this); // Spawn the output kitchen object

                        
                        state = State.Fried; // Set the state to fried
                        burningTimer = 0f; // Reset the burning timer
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO()); // Get the burning recipe for the output kitchen object
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });// Notify subscribers about the state change

                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax // Calculate the normalized progress
                    }); // Notify that frying progress has changed

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        //fried                                        
                        GetKitchenObject().DestroySelf(); // Destroy the input kitchen object
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this); // Spawn the output kitchen object

                        state = State.Burned; // Set the state to fried

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });// Notify subscribers about the state change

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f // Reset the progress to 0 when burned
                        }); // Notify that frying progress has changed
                    }
                        break;
                case State.Burned:

                    break;
            }
            Debug.Log(state);
        }
    }
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
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying; // Set the state to frying
                    fryingTimer = 0f; // Reset the frying timer

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });// Notify subscribers about the state change

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax // Calculate the normalized progress
                    }); // Notify that frying progress has changed
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
                state=State.Idle; // Reset the state to idle

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });// Notify subscribers about the state change

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f // Reset the progress to 0 when the kitchen object is removed
                }); // Notify that frying progress has changed
            }
        }

    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null; // Check if there is a recipe with the given input
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO); // Get the CuttingRecipeSO for the given input
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output; // Return the output kitchen object for the given input
        }
        else
        {

            return null; // Return null if no matching recipe is found

        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO; // Return the CuttingRecipeSO for the given input
            }
        }
        return null; // Return null if no matching recipe is found
    }


    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO; // Return the CuttingRecipeSO for the given input
            }
        }
        return null; // Return null if no matching recipe is found
    }
}
