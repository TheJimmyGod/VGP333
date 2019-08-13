using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : MonoBehaviour
{
    private float _currentPistolBullet = 0;
    private float _totalPistolBullet = 0;
    private float _currentShotgunBullet = 0;
    private float _totalShotGunBullet = 0;
    private float _currentMachineGunBullet = 0;
    private float _totalMachineGunBullet = 0;

    public const string CURRENT_PISTOL_BULLET = "CurrentPistolBullet";
    public const string TOTAL_PISTOL_BULLET = "TotalPistolBullet";
    public const string CURRENT_SHOTGUN_BULLET = "CurrentShotGunBullet";
    public const string TOTAL_SHOTGUN_BULLET = "TotalShotGunBullet";
    public const string CURRENT_MACHINEGUN_BULLET = "CurrentMachineGunBullet";
    public const string TOTAL_MACHINEGUN_BULLET = "TotalMachineGunBullet";
    private void Awake()
    {
        GameLoader.CallonComplete(Init);
    }
    private void Init()
    {
        ServiceLocator.Register<GunData>(this);
        _currentPistolBullet = PlayerPrefs.GetFloat(CURRENT_PISTOL_BULLET, 0.0f);
        _totalPistolBullet = PlayerPrefs.GetFloat(TOTAL_PISTOL_BULLET, 0.0f);
        _currentShotgunBullet = PlayerPrefs.GetFloat(CURRENT_SHOTGUN_BULLET, 0.0f);
        _totalShotGunBullet = PlayerPrefs.GetFloat(TOTAL_SHOTGUN_BULLET, 0.0f);
        _currentMachineGunBullet = PlayerPrefs.GetFloat(CURRENT_MACHINEGUN_BULLET, 0.0f);
        _totalMachineGunBullet = PlayerPrefs.GetFloat(TOTAL_MACHINEGUN_BULLET, 0.0f);
    }

    public void SaveGunData(float C_Pistol, float C_Shot, float C_Machine,
        float T_Pistol, float T_Shot, float T_Machine)
    {
        SaveCurrentPistolBullet(C_Pistol);
        SaveCurrentShotBullet(C_Shot);
        SaveCurrentMachineBullet(C_Machine);

        SaveTotalPistolBullet(T_Pistol);
        SaveTotalShotBullet(T_Shot);
        SaveTotalMachineBullet(T_Machine);
    }

    public void SaveTotalPistolBullet(float bullet)
    {
        PlayerPrefs.SetFloat(TOTAL_PISTOL_BULLET, bullet);
    }

    public void SaveCurrentPistolBullet(float bullet)
    {
        PlayerPrefs.SetFloat(CURRENT_PISTOL_BULLET, bullet);
    }

    public void SaveTotalShotBullet(float bullet)
    {
        PlayerPrefs.SetFloat(TOTAL_SHOTGUN_BULLET, bullet);
    }

    public void SaveCurrentShotBullet(float bullet)
    {
        PlayerPrefs.SetFloat(CURRENT_SHOTGUN_BULLET, bullet);
    }
    public void SaveTotalMachineBullet(float bullet)
    {
        PlayerPrefs.SetFloat(TOTAL_MACHINEGUN_BULLET, bullet);
    }

    public void SaveCurrentMachineBullet(float bullet)
    {
        PlayerPrefs.SetFloat(CURRENT_MACHINEGUN_BULLET, bullet);
    }
}
