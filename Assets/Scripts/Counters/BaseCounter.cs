using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        // This method can be overridden by derived classes to implement specific interaction logic
        Debug.Log("BaseCounter.Interact(); ");
    }

    public virtual void InteractAlternate(Player player)
    {
        // This method can be overridden by derived classes to implement specific interaction logic
        Debug.Log("BaseCounter.InteractAlternate(); ");
    }



    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;

    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
