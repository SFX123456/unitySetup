using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private bool testing = false;
    // Start is called before the first frame update
    [SerializeField] private ThirdPersonController _thirdPersonController;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCameraGeneral;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCameraAim;
    [SerializeField] private CinemachineVirtualCamera _autoCamera;

    private GameObject _objectToFollow;

    public bool ShouldFollowPlayer = true;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (testing)
        {
            return;
        }
        if (!ShouldFollowPlayer)
        {
            _autoCamera.VirtualCameraGameObject.SetActive(true);
            return;
        }
        
        
        _autoCamera.VirtualCameraGameObject.SetActive(false);
        
        if (_thirdPersonController.IsAiming)
        {
            _cinemachineVirtualCameraAim.VirtualCameraGameObject.SetActive(true);
            _cinemachineVirtualCameraGeneral.VirtualCameraGameObject.SetActive(false);
        }
        else
        {
            _cinemachineVirtualCameraGeneral.VirtualCameraGameObject.SetActive(true);
            _cinemachineVirtualCameraAim.VirtualCameraGameObject.SetActive(false);
        }
    }

    public void SetCarGameObjectToFollow(GameObject gameObject)
    {
        _objectToFollow = gameObject;
        _autoCamera.Follow = gameObject.GetComponent<Transform>();
        _autoCamera.VirtualCameraGameObject.SetActive(true); 
        _cinemachineVirtualCameraGeneral.VirtualCameraGameObject.SetActive(false);
        _cinemachineVirtualCameraAim.VirtualCameraGameObject.SetActive(false);
        ShouldFollowPlayer = false;
    }
}
