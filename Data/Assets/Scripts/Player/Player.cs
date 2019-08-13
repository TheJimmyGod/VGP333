using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    public string DataSourceId = "PlayerData";

    public Vector3 jump;
    public Vector3 pos;
    public int mCount = 0;
    public bool isGrounded;
    public bool isActive = false;
    public bool _isDying = false;

    private UIManager _uiManager;
    private Rigidbody rb;
    private Guns _guns = null;
    private int jumpcount = 0;

    private DataLoader _dataLoader;
    private JsonDataSource _playerData;

    public string Name;
    public float CurrentHealth;
    public float MaxHealth;
    public float RunSpeed;
    public float JumpForce;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

        _uiManager = ServiceLocator.Get<UIManager>();
        _dataLoader = ServiceLocator.Get<DataLoader>();
        _guns = GetComponentInChildren<Guns>();
        _playerData = _dataLoader.GetDataByName(DataSourceId) as JsonDataSource;

        CurrentHealth = System.Convert.ToInt32(_playerData.DataDictionary["CurrentHealth"]);
        MaxHealth = System.Convert.ToSingle(_playerData.DataDictionary["MaxHealth"]);
        JumpForce = System.Convert.ToSingle(_playerData.DataDictionary["JumpForce"]);
        RunSpeed = System.Convert.ToSingle(_playerData.DataDictionary["RunSpeed"]);
        Name = System.Convert.ToString(_playerData.DataDictionary["Name"]);

        _uiManager.UpdatePlayerHP(CurrentHealth);
        CurrentHealth = MaxHealth;

    }

    // FixedUpdate is usually using for physics on the objects.
    // MonoBehaviour manages void Start(), void Update(), and private void Awake()
    void FixedUpdate()
    {
        float mHorizontal = Input.GetAxis("Horizontal");
        float mVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(mHorizontal, 0.0f, mVertical);
        
        rb.AddForce(movement * RunSpeed);

        pos = this.transform.position;
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpcount != 1)
        {
            rb.AddForce(jump * JumpForce, ForceMode.Impulse);
            jumpcount++;

        }

        if(Input.GetKeyDown(KeyCode.R)) // Reload
        {
            Reload();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)) // Shoot
        {
            if (_guns.GunTypeNumber == 0)
                _guns.PistolShoot();
            else if (_guns.GunTypeNumber == 1)
                _guns.ShotGunShoot();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && _guns.GunTypeNumber == 2) // Shoot
            _guns.MachineGunShoot();

        _uiManager.UpdatePlayerHP(CurrentHealth);

    }

    public void Reload()
    {
        _guns.Reload();
    }

    void OnCollisionStay()
    {
        isGrounded = true;
        jumpcount = 0;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
            _guns.M_MaxAmmo += 100;
            _guns.S_MaxAmmo += 15;
            _guns.P_MaxAmmo += 40;
        }
        if(other.gameObject.CompareTag("Objective"))
        {
            other.gameObject.SetActive(false);
            mCount++;
            if (mCount == 3)
            {
                Debug.Log("Excellent!");
            }
            else
            {
                Debug.Log("Get");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0.0f)
        {
            _isDying = true;
            DestroyMe();
        }
    }

    private void DestroyMe()
    {
        if (CurrentHealth <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
