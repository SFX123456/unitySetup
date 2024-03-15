using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class ShootingLogic : MonoBehaviour
{
    [SerializeField] private bool isServer;
    // Start is called before the first frame update
    [SerializeField] private LayerMask mouseColliderLayerMask;

    [SerializeField] private Transform debugTra;
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform spawnPosBullets;
    [SerializeField] private Transform toRotate;
    [SerializeField] private float rotateYWhenAiming;
    
    

    public float ShootingTimeOut;

    private Transform _transform;

    private ThirdPersonController _thirdPersonController;
    private AudioSource _audioSource;

    private Transform hitTransform = null;

    private Vector3 aimdirection;
    


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = AudioController.Instance.ShootingSound;
        _transform = GetComponent<Transform>();

        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
      
        Vector3 mousePosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            debugTra.position = raycastHit.point;
            mousePosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (_thirdPersonController.IsAiming)
        {
            Vector3 tempV = toRotate.rotation.eulerAngles;
            tempV.y = rotateYWhenAiming;
            toRotate.localRotation = Quaternion.Euler(0f, 77f, 0f);
            
            Vector3 worldAimTarget = mousePosition;
            worldAimTarget.y = _transform.position.y;
            Vector3 aimDirection = (worldAimTarget - _transform.position).normalized;

            _transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 15f);
        }
        else
        {
            toRotate.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }  
        if (!isServer)
        {
                     //handleIsShooting();
            return;
        }
        aimdirection = (mousePosition - spawnPosBullets.position).normalized;
        if (_thirdPersonController.IsShooting)
        {
          handleIsShooting(); 
        }
        
        //Debug.DrawRay();

    }

    private void handleIsShooting()
    {
         _audioSource.Play(0);
                    
                    Instantiate(bullet, spawnPosBullets.position, Quaternion.LookRotation(aimdirection,Vector3.up));
                    _thirdPersonController.IsShooting = false;
                    if (!isServer)
                    {
                        return;
                    }           
                    if (!hitTransform) return;
                    if(!hitTransform.TryGetComponent(out BulletHitObject bulletHitObject)) return;
                    bulletHitObject.ApplyDamage(1);
                    Debug.Log("player hit soemthing ");
    }

}