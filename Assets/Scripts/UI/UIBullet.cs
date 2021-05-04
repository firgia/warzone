using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weapon;
using Utils;

namespace UI
{
    public class UIBullet : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        private WeaponController weaponController;

        private Color32 enableColor = new Color32(255, 255, 255, 255);
        private Color32 disableColor = new Color32(100, 100, 100, 255);

        private Image[] bullets;

        void Start()
        {
            weaponController = GameObject.FindGameObjectWithTag(TagUtils.Weapon).GetComponent<WeaponController>();
            bullets = AddBullet(weaponController.TotalBullet);
        }

   
        void Update()
        {
            ValidateBullet();
        }

        /// <summary>
        /// di gunakan untuk memvalidasi total peluru player ke dalam ui
        /// </summary>
        private void ValidateBullet()
        {
        
            for(int i = 0; i < bullets.Length; i++)
            {
                if (i < weaponController.CurrentBullet) bullets[i].color = enableColor;
                else bullets[i].color = disableColor;
            }
        }

        /// <summary>
        /// menambahkan image peluru sesuai dengan total yang di inginkan
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        private Image[] AddBullet(int total)
        {
            Image[] result = new Image[weaponController.TotalBullet];
            for (int i = 0;i < total; i++)
            {
                GameObject newObj = Instantiate(bulletPrefab, transform);
                result[i] = newObj.GetComponentInChildren<Image>();
            }

            return result;
        }
    }
}