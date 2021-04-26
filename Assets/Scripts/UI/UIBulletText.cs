using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weapon;
using Utils;

public class UIBulletText : MonoBehaviour
{
    [SerializeField] private Text text;

    private WeaponController weaponController;

    void Start()
    {
        weaponController = GameObject.FindGameObjectWithTag(TagUtils.Weapon).GetComponent<WeaponController>();    
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "total peluru x " + weaponController.CurrentBullet;
        Debug.Log("test");
    }
}
