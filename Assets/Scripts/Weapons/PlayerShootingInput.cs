using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingInput : MonoBehaviour
{
    [Header("Wave 1")]
    public Gun basicGun;
    public MeleeWeapon basicKnife;

    [Header("Wave 2")]
    public Gun upgradedGun;

    [Header("Wave 3")]
    public Gun eliteGun;
    public MeleeWeapon powerSword;

    private WeaponBase activeWeapon;

    void OnEnable()
    {
        WaveManager.OnWaveStarted += HandleWaveUpgrade;
    }

    void OnDisable()
    {
        WaveManager.OnWaveStarted -= HandleWaveUpgrade;
    }

    void Start()
    {
        SetupWave1();
    }

    void HandleWaveUpgrade(int wave)
    {
        if (wave == 2) SetupWave2();
        if (wave == 3) SetupWave3();
    }

    void SetupWave1()
    {
        DisableAll();
        basicGun?.gameObject.SetActive(true);
        basicKnife?.gameObject.SetActive(true);
        activeWeapon = basicGun;
        Debug.Log("Wave 1 loadout: Basic Gun + Basic Knife");
    }

    void SetupWave2()
    {
        DisableAll();
        upgradedGun?.gameObject.SetActive(true);
        basicKnife?.gameObject.SetActive(true);
        activeWeapon = upgradedGun;
        Debug.Log("Wave 2 loadout: Upgraded Gun + Basic Knife");
    }

    void SetupWave3()
    {
        DisableAll();
        eliteGun?.gameObject.SetActive(true);
        powerSword?.gameObject.SetActive(true);
        activeWeapon = eliteGun;
        Debug.Log("Wave 3 loadout: Elite Gun + Power Sword");
    }

    void DisableAll()
    {
        basicGun?.gameObject.SetActive(false);
        basicKnife?.gameObject.SetActive(false);
        upgradedGun?.gameObject.SetActive(false);
        eliteGun?.gameObject.SetActive(false);
        powerSword?.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
            activeWeapon?.Fire();

        // 1 = gun, 2 = melee
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            SwitchToGun();
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            SwitchToMelee();
    }

    void SwitchToGun()
    {
        // picks whichever gun is currently active for this wave
        if (eliteGun != null && eliteGun.gameObject.activeSelf)
            activeWeapon = eliteGun;
        else if (upgradedGun != null && upgradedGun.gameObject.activeSelf)
            activeWeapon = upgradedGun;
        else
            activeWeapon = basicGun;
    }

    void SwitchToMelee()
    {
        // picks whichever melee is active for this wave
        if (powerSword != null && powerSword.gameObject.activeSelf)
            activeWeapon = powerSword;
        else
            activeWeapon = basicKnife;
    }

    public WeaponBase GetActiveWeapon()
    {
        return activeWeapon;
    }
}