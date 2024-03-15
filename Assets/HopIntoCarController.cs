using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class HopIntoCarController : MonoBehaviour
{
    [SerializeField] private LayerMask layermask;
    private ThirdPersonController _thirdPersonController;
    private Transform _carTransform;

    private void Start()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
            Debug.DrawRay(transform.position + Vector3.up * 0.7f,transform.forward * 720,Color.green);
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            Debug.Log("checking if theres a car in front");
            RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up * 0.7f, transform.forward, 70f, layermask);
            if (hits.Length == 0) return;
            foreach (RaycastHit raycastHit in hits)
            {
                if(!raycastHit.transform.TryGetComponent(out CarPlayerController carPlayerController)) continue;
                Debug.Log("raycast hit a car");
                _thirdPersonController.InCar = true;
                CameraController.Instance.SetCarGameObjectToFollow(carPlayerController.gameObject);
                carPlayerController.Player = transform;
                _carTransform = carPlayerController.transform;
                _thirdPersonController.gameObject.SetActive(false);
                return;
            }    
        }

    }

    public void HandleSteppingOutofCar(Transform car)
    {
        transform.position = car.position + Vector3.up * 2;
        _carTransform = null;
    }
}
