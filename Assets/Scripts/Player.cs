using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Player : MonoBehaviour {

    #region Fields

    //Making Single Instance - singleton Pattern
    public static Player Instance { get; private set; } 

    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private LayerMask countersLayer;

    private bool isWalking;

    private float playerHeight = 2f;
    private float playerRadius = 0.7f;
    private float moveDistance;
    private Vector3 lastInteractionDir;

    private ClearCounter selectedCounter;

    #endregion

    #region Custom Events

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    #endregion

    private void Awake () {
        if(Instance != null) {
            Debug.LogError(" There is more than 1 Instance of the player!! ");
        }
        Instance = this;
    }

    private void Start () {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if(selectedCounter != null) {
            selectedCounter.Interact();
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction () {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        //getting the last dir player was moving
        if(moveDir != Vector3.zero) {
            lastInteractionDir = moveDir;
        }
        
        float interactionDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactionDistance, countersLayer)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                // Has ClearCounter
                if(clearCounter != selectedCounter) {
                    SetSelectedCounter(clearCounter);
                }
            }
            else {
                SetSelectedCounter(null);
            }
        }
        else {
            SetSelectedCounter(null);
        }
        Debug.Log(selectedCounter);
        
    }

    private void HandleMovement () {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        
        if(!canMove) {
            //Cannot move in moveDir

            //Attempt for only X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if(canMove) {
                moveDir = moveDirX;
            }
            else {
                //Cannot move in moveDirX

                //Attempt for only Z
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if(canMove) {
                    moveDir = moveDirZ;
                }
                else {
                    //Cannot Move   
                }
            }
        }
        
        if(canMove) {
            transform.position += moveDir * moveDistance;
        }

        // transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
    }

    public bool IsWalking () {
        return isWalking;
    }

    private void SetSelectedCounter (ClearCounter selectedCounter){
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { 
            selectedCounter = selectedCounter
        });
    }
}
