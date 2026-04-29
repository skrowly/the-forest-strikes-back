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
        basicKnife?.gameObject.SetActive(false); // start with gun visible
        activeWeapon = basicGun;
    }
    void SetupWave2()
    {
        DisableAll();
        upgradedGun?.gameObject.SetActive(true);
        activeWeapon = upgradedGun;
    }
    void SetupWave3()
    {
        DisableAll();
        eliteGun?.gameObject.SetActive(true);
        activeWeapon = eliteGun;
    }

    void DisableAll()
    {
        // disable entire game object including visual
        basicGun?.gameObject.SetActive(false);
        basicKnife?.gameObject.SetActive(false);
        upgradedGun?.gameObject.SetActive(false);
        eliteGun?.gameObject.SetActive(false);
        powerSword?.gameObject.SetActive(false);
    }

    void Update()
    {
        //left click OR spacebar to fire -- will make it easier if you dont have mouse and just touch pad
        if (Mouse.current.leftButton.isPressed ||
            Keyboard.current.spaceKey.isPressed)
        {
            activeWeapon?.Fire();
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            SwitchToGun();
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            SwitchToMelee();

    }

    void SwitchToGun()
    {
        // hide all melee show only active gun
        basicKnife?.gameObject.SetActive(false);
        powerSword?.gameObject.SetActive(false);

        if (eliteGun != null && eliteGun.gameObject.activeSelf)
        {
            activeWeapon = eliteGun;
            eliteGun.gameObject.SetActive(true);
        }
        else if (upgradedGun != null && upgradedGun.gameObject.activeSelf)
        {
            activeWeapon = upgradedGun;
            upgradedGun.gameObject.SetActive(true);
        }
        else
        {
            activeWeapon = basicGun;
            basicGun?.gameObject.SetActive(true);
        }
        Debug.Log("Switched to gun: " + activeWeapon?.name);
    }

    void SwitchToMelee()
    {
        //hides gun show only active melee
        basicGun?.gameObject.SetActive(false);
        upgradedGun?.gameObject.SetActive(false);
        eliteGun?.gameObject.SetActive(false);

        if (powerSword != null && powerSword.gameObject.activeSelf)
        {
            activeWeapon = powerSword;
            powerSword.gameObject.SetActive(true);
        }
        else
        {
            activeWeapon = basicKnife;
            basicKnife?.gameObject.SetActive(true);
        }
        Debug.Log("Switched to melee: " + activeWeapon?.name);
    }
    public WeaponBase GetActiveWeapon()
    {
        return activeWeapon;
    }
}