using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 3.5f;
    [SerializeField] GameObject muzzelFlash;
    [SerializeField] GameObject __hitMarker;
    [SerializeField] AudioSource _weponSound;
    [SerializeField] private int currentAmmo;
    [SerializeField] private GameObject _WeaponOnOff;
    public bool hasCoin;
    private UIManager _uiManager;
    private bool _isReloading = false;
    private CharacterController _controller;
    private float _gravity = 9.81f;
    private int maxAmmo = 50;

    // Start is called before the first frame update
    void Start()
    {
        _WeaponOnOff.SetActive(false);
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentAmmo = maxAmmo;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.AmmoCount(currentAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKey(KeyCode.R) && _isReloading == false)
        {
            StartCoroutine(Relode());
            _isReloading = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButton(0) && currentAmmo >0)
        {
            
             Shoot();
            
        }
        else
        {
            muzzelFlash.SetActive(false);
            _weponSound.Stop();
        }
    }

    void CalculateMovement()
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        Vector3 _direction = new Vector3(_horizontal, 0, _vertical);
        Vector3 _velocity = _direction * _speed;
        _velocity.y -= _gravity;
        _velocity = transform.transform.TransformDirection(_velocity);
        _controller.Move(_velocity * Time.deltaTime);
    }

    void Shoot()
    {
        currentAmmo--;
        _uiManager.AmmoCount(currentAmmo);
        muzzelFlash.SetActive(true);
        if (_weponSound.isPlaying == false)
        {
            _weponSound.Play();
        }


        Ray origin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(origin, out hitInfo))
        {
            
            Instantiate(__hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destructible crate = hitInfo.transform.GetComponent<Destructible>();
            if(crate != null)
            {
                crate.WoodenCrateDestroy();
            }

        }
    }

    IEnumerator Relode()
    {
        yield return new WaitForSeconds(1.5f);
       
        currentAmmo = maxAmmo;
        _uiManager.AmmoCount(currentAmmo);
        _isReloading = false;

        
    }

    public void WeaponOnOff()
    {
        _WeaponOnOff.SetActive(true);
    }

    
}
