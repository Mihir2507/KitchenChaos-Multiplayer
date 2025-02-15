using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class ClearCounter : MonoBehaviour {

    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public void Interact () {
        if( kitchenObject == null){
            Transform KitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            KitchenObjectTransform.localPosition = Vector3.zero;

            kitchenObject = KitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetClearCounter(this);
        }
        else {
            Debug.Log(kitchenObject.GetClearCounter());
        }
        
        
    }
}
