using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0; //0=pistol 1=riffle 2=grenade
    private Animator _animator;
    public GameObject pistolIcon, riffleIcon;
    
    void Start()
    {
        SelectedWeapon();
        _animator = GameObject.Find("Swat").GetComponent<Animator>();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeapon >= transform.childCount -1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount -1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (selectedWeapon == 0)
        {
            riffleIcon.SetActive(false);
            _animator.SetInteger("WeaponIndex", 0);
            pistolIcon.SetActive(true);
        }
        else if (selectedWeapon == 1)
        {
            pistolIcon.SetActive(false);
            _animator.SetInteger("WeaponIndex", 1);
            riffleIcon.SetActive(true);
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
            _animator.SetInteger("WeaponIndex", 0);
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
            _animator.SetInteger("WeaponIndex", 1);
        }
        
        // if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        // {
        //     selectedWeapon = 2;
        // }
        
        if(previousSelectedWeapon != selectedWeapon)
        {
            SelectedWeapon();
        }
    }

    void SelectedWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}