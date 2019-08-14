using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    public string DataSourceId = "PlayerData";
    public Vector3 movement;
    public Vector3 jump;
    public Vector3 pos;
    public int mCount = 0;
    public bool isGrounded;
    public bool isActive = false;
    public bool _isDying = false;

    private UIManager _uiManager;
    private Guns _guns = null;
    
    private DataLoader _dataLoader;
    private JsonDataSource _playerData;

    public string Name;
    public float CurrentHealth;
    public float MaxHealth;
    public float RunSpeed;
    public float JumpForce;


    private void Awake()
    {
        ServiceLocator.Register<Player>(this);
        ServiceLocator.Get<GameManager>().requiredTowin = 0;
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
    void Update()
    {
        pos = this.transform.position;

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
            else if (_guns.GunTypeNumber == 2)
                _guns.MachineGunShoot();


        }

        _uiManager.UpdatePlayerHP(CurrentHealth);

    }

    public void Reload()
    {
        _guns.Reload();
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }
    public void GetAmmosByItem()
    {
        _guns.M_MaxAmmo += 100;
        _guns.S_MaxAmmo += 15;
        _guns.P_MaxAmmo += 40;
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
