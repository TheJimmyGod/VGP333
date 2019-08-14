using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//{
//  "Damage": 20,
//  "Range": 100,
//  "Speed": 8,
//  "Ammo": 8,
//  "MaxAmmo": 40,
//  "Name": "Pistol"
//}

public class Guns : MonoBehaviour
{
    enum GunType
    {
        Pistol = 0,
        ShotGun,
        MachineGun
    };

    public string P_Name;
    public float P_Range;
    public float P_Damage;
    public float P_MaxAmmo;
    public float P_Ammo;
    public float P_Speed;

    public string S_Name;
    public float S_Range;
    public float S_Damage;
    public float S_MaxAmmo;
    public float S_Ammo;
    public float S_Speed;

    public string M_Name;
    public float M_Range;
    public float M_Damage;
    public float M_MaxAmmo;
    public float M_Ammo;
    public float M_Speed;

    public string pistolId = "PistolData";
    public string shotGunId = "ShotGunData";
    public string machineGunId = "MachineGunData";
    private DataLoader _GundataLoader;
    private JsonDataSource _gunData;
    private JsonDataSource _SgunData;
    private JsonDataSource _MgunData;

    public GameObject _gunObject;
    public Transform BulletSpawnPoint;
    public GameObject bulletPrefab;
    private UIManager _uiManager;
    public float bulletSpeed = 0.0f;

    public int GunTypeNumber = 0;
    private GunType _gunType;
    private Bullets _bullets = null;
    private void Awake()
    {
        _gunObject = GameObject.FindGameObjectWithTag("PlayerGun");
        _uiManager = ServiceLocator.Get<UIManager>();
        _bullets = gameObject.GetComponent<Bullets>();
        _GundataLoader = ServiceLocator.Get<DataLoader>();

        _gunData = _GundataLoader.GetDataByName(pistolId) as JsonDataSource;

        P_Damage = System.Convert.ToSingle(_gunData.DataDictionary["Damage"]);
        P_Range = System.Convert.ToSingle(_gunData.DataDictionary["Range"]);
        P_Speed = System.Convert.ToSingle(_gunData.DataDictionary["Speed"]);
        P_Ammo = System.Convert.ToSingle(_gunData.DataDictionary["Ammo"]);
        P_MaxAmmo = System.Convert.ToSingle(_gunData.DataDictionary["MaxAmmo"]);
        P_Name = System.Convert.ToString(_gunData.DataDictionary["Name"]);

        _SgunData = _GundataLoader.GetDataByName(shotGunId) as JsonDataSource;

        S_Name = System.Convert.ToString(_SgunData.DataDictionary["Name"]);
        S_Range = System.Convert.ToSingle(_SgunData.DataDictionary["Range"]);
        S_Speed = System.Convert.ToSingle(_SgunData.DataDictionary["Speed"]);
        S_Damage = System.Convert.ToInt32(_SgunData.DataDictionary["Damage"]);
        S_MaxAmmo = System.Convert.ToInt32(_SgunData.DataDictionary["MaxAmmo"]);
        S_Ammo = System.Convert.ToInt32(_SgunData.DataDictionary["Ammo"]);

        _MgunData = _GundataLoader.GetDataByName(machineGunId) as JsonDataSource;

        M_Name = System.Convert.ToString(_MgunData.DataDictionary["Name"]);
        M_Range = System.Convert.ToSingle(_MgunData.DataDictionary["Range"]);
        M_Speed = System.Convert.ToSingle(_MgunData.DataDictionary["Speed"]);
        M_Damage = System.Convert.ToInt32(_MgunData.DataDictionary["Damage"]);
        M_MaxAmmo = System.Convert.ToInt32(_MgunData.DataDictionary["MaxAmmo"]);
        M_Ammo = System.Convert.ToInt32(_MgunData.DataDictionary["Ammo"]);
        _uiManager.UpdateCurrentBulletCount(P_Ammo);
        _uiManager.UpdateTotalBulletCount(P_MaxAmmo);
        _gunType = GunType.Pistol;
    }

    private GunType GetGunType()
    {
        return _gunType;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GunTypeNumber == 2)
                GunTypeNumber = 0;
            else
                GunTypeNumber++;
        }



        if (GunTypeNumber == 0)
        {
            _gunType = GunType.Pistol;
            bulletSpeed = P_Speed;
            _uiManager.UpdateCurrentBulletCount(P_Ammo);
            _uiManager.UpdateTotalBulletCount(P_MaxAmmo);
            _uiManager.UpdateGunText("Pistol");

        }
        else if (GunTypeNumber == 1)
        {
            _gunType = GunType.ShotGun;
            bulletSpeed = S_Speed;
            _uiManager.UpdateCurrentBulletCount(S_Ammo);
            _uiManager.UpdateTotalBulletCount(S_MaxAmmo);
            _uiManager.UpdateGunText("ShotGun");
        }
        else if (GunTypeNumber == 2)
        {
            _gunType = GunType.MachineGun;
            bulletSpeed = M_Speed;
            _uiManager.UpdateCurrentBulletCount(M_Ammo);
            _uiManager.UpdateTotalBulletCount(M_MaxAmmo);
            _uiManager.UpdateGunText("MachineGun");
        }
        else
            GunTypeNumber = 0;
    }

    public void PistolShoot()
    {

        if (P_Ammo > 0)
        {
            --P_Ammo;
            GameObject bullet = Instantiate(bulletPrefab, BulletSpawnPoint.position, Quaternion.identity) as GameObject;
            var rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(BulletSpawnPoint.up * bulletSpeed, ForceMode.Force);
            _uiManager.UpdateCurrentBulletCount(P_Ammo);
            _uiManager.UpdateTotalBulletCount(P_MaxAmmo);
        }
        else
        {
            if (P_MaxAmmo > 0 && P_MaxAmmo >= 8)
            {
                P_Ammo += 8;
                P_MaxAmmo -= 8;
            }
            else if (P_MaxAmmo > 0 && P_MaxAmmo < 8)
            {
                P_Ammo += P_MaxAmmo;
                P_MaxAmmo = 0;
            }
        }
    }

    public void ShotGunShoot()
    {
        if (S_Ammo > 0)
        {
            --S_Ammo;
            float increasement = -50.0f;
            for (int i = 0; i < 4; i++)
            {
                float angle = transform.eulerAngles.y + Random.value;
                increasement += i * 5;
                Quaternion rotation = Quaternion.Euler(new Vector3(0, angle + increasement, 0));
                GameObject bullet = Instantiate(bulletPrefab, BulletSpawnPoint.position, Quaternion.identity) as GameObject;
                var rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(BulletSpawnPoint.up * bulletSpeed, ForceMode.Force);
            }
            _uiManager.UpdateCurrentBulletCount(S_Ammo);
            _uiManager.UpdateTotalBulletCount(S_MaxAmmo);
        }
        else
        {
            if (S_MaxAmmo > 0 && S_MaxAmmo >= 8)
            {
                S_Ammo += 8;
                S_MaxAmmo -= 8;
            }
            else if (S_MaxAmmo > 0 && S_MaxAmmo < 8)
            {
                S_Ammo += S_MaxAmmo;
                S_MaxAmmo = 0;
            }
        }
    }

    public void MachineGunShoot()
    {
        if (M_Ammo > 0)
        {
            --M_Ammo;
            GameObject bullet = Instantiate(bulletPrefab, BulletSpawnPoint.position, Quaternion.identity) as GameObject;
            var rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(BulletSpawnPoint.up * bulletSpeed, ForceMode.Force);
            _uiManager.UpdateCurrentBulletCount(M_Ammo);
            _uiManager.UpdateTotalBulletCount(M_MaxAmmo);
        }
        else
        {
            if (M_MaxAmmo > 0 && M_MaxAmmo >= 40)
            {
                M_Ammo += 40;
                M_MaxAmmo -= 40;
            }
            else if (M_MaxAmmo > 0 && M_MaxAmmo < 40)
            {
                M_Ammo += M_MaxAmmo;
                M_MaxAmmo = 0;
            }
        }
    }

    public void Reload()
    {
        float remainedBullet = 0.0f;
        if(_gunType == GunType.Pistol)
        {
            remainedBullet = P_Ammo;
            remainedBullet = 8 - remainedBullet;
            for (int i = 0; i < remainedBullet; i++)
            {
                if(P_MaxAmmo != 0)
                {
                    ++P_Ammo;
                    --P_MaxAmmo;
                }
                else
                {
                    break;
                }
            }
        }
        else if(_gunType == GunType.ShotGun)
        {
            remainedBullet = S_Ammo;
            remainedBullet = 8 - remainedBullet;
            for (int i = 0; i < remainedBullet; i++)
            {
                if(S_MaxAmmo != 0)
                {
                    ++S_Ammo;
                    --S_MaxAmmo;
                }
                else
                {
                    break;
                }
            }
        }
        else if(_gunType == GunType.MachineGun)
        {
            remainedBullet = M_Ammo;
            remainedBullet = 40 - remainedBullet;
            for (int i = 0; i < remainedBullet; i++)
            {
                if(M_MaxAmmo != 0)
                {
                    ++M_Ammo;
                    --M_MaxAmmo;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
