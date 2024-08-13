using System;
using Unity.VisualScripting;
using UnityEngine;


public class SelectedCounterVisual : MonoBehaviour {

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObejct;

    private void Start(){
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if(e.selectedCounter == clearCounter) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show(){
        visualGameObejct.SetActive(true);
    }

    private void Hide(){
        visualGameObejct.SetActive(false);
    }
}
