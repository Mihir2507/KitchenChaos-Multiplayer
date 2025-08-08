using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPoint;
    [SerializeField] ClearCounter secondCounter;
    [SerializeField] bool testing;

    private KitchenObject kitchenObject;

    public void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetKitchenObjectParent(secondCounter);
            }
        }
    }

    public void Interact (Player player) {
        if( kitchenObject == null){
            Transform KitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            KitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else {
            //Debug.Log(kitchenObject.GetClearCounter());
            kitchenObject.SetKitchenObjectParent(player);
        }
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
        if (kitchenObject != null) return true;
        else return false;
        
    }
}
