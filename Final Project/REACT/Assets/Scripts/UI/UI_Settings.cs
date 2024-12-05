using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer master;
    [SerializeField] private Scrollbar masterVolumeSlider;
    [SerializeField] private Scrollbar weaponVolumeSlider;

    private float defaultMasterVolume;
    private float defaultWeaponVolume;

    // Backlog
    // Save Settings

    void Start()
    {
        master.GetFloat("Master", out defaultMasterVolume);
        master.GetFloat("Weapon", out defaultWeaponVolume);

        defaultMasterVolume = (defaultMasterVolume + 80) / 100;
        defaultWeaponVolume = (defaultWeaponVolume + 80) / 100;

        masterVolumeSlider.value = defaultMasterVolume;
        weaponVolumeSlider.value = defaultWeaponVolume;
    }

    void Update()
    {
        
    }

    public void updateMasterVolume(float unFixedDB)
    {
        float db = (unFixedDB * 100) - 80;
        //Debug.Log($"Master Set to: {db}");
        master.SetFloat("Master", db);
    }

    public void updateWeaponVolume(float unFixedDB)
    {
        float db = (unFixedDB * 100) - 80;
        //Debug.Log($"Weapon Set to: {db}");
        master.SetFloat("Weapon", db);
    }

}
