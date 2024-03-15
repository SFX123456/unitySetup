using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class CarPlayerController : MonoBehaviour
{
    public Transform Player = null;

    private bool shouldActivatePlayer = false;
    private void Update()
    {
        if (Player == null) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("activating gameobject again");
            shouldActivatePlayer = true;
        }
    }

    private void LateUpdate()
    {
        if (!shouldActivatePlayer)return;
        Player.gameObject.SetActive(true);
        var x = Player.GetComponent<HopIntoCarController>(); 
        x.HandleSteppingOutofCar(transform); 
        Player = null;
        shouldActivatePlayer = false;
        CameraController.Instance.ShouldFollowPlayer = true;
    }
}
