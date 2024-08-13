using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    #region Fields
    private Player player;

    private Animator playerAnimator;
    private const string IS_Walking = "IsWalking";

    #endregion

    private void Awake() {
        player = GetComponentInParent<Player>();
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool(IS_Walking, false);
    }

    private void Update () {
        playerAnimator.SetBool(IS_Walking, player.IsWalking());
    }
}
