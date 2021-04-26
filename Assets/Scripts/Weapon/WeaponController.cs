using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

namespace Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform projectilePos;
        [SerializeField] private Transform targetDirectionShoot;
        [SerializeField] private BulletController bulletPrefab;
        [SerializeField] private int totalBullet = 5;


        private int _currentBullet;

        public int CurrentBullet => _currentBullet;
        public int MaxBullet => totalBullet;

        void Start()
        {
            _currentBullet = totalBullet;
        }

 
        void Update()
        {
            InputController();
        }

        void InputController()
        {
            if (Input.touchSupported)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        OnInputDragWeapon(touch.position);
                    }
                    else if(touch.phase == TouchPhase.Ended)
                    {
                        if (_currentBullet > 0)
                        {
                            Shoot();
                        }
                    }
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    OnInputDragWeapon(Input.mousePosition);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    if (_currentBullet > 0)
                    {
                        Shoot();
                    }
                }
            }
        }

        /// <summary>
        /// menembakan peluru sesuai arah atau target yang di tentukan
        /// (melakukan instantiate objeck baru)
        /// </summary>
        /// <param name="inputPosition">tentukan target dari peluru yang akan di tembakkan</param>
        void Shoot()
        {
            Vector2 _startPos = new Vector2(projectilePos.transform.position.x, projectilePos.transform.position.y);
            Vector2 _endPos = new Vector2(targetDirectionShoot.transform.position.x, targetDirectionShoot.transform.position.y);
            Vector2 dir = (_endPos - _startPos).normalized;

            BulletController newBullet = Instantiate(bulletPrefab);
            newBullet.transform.position = projectilePos.position;
            newBullet.Shoot(dir);
            OnShoot();
        }
        #region handler 
       
        void OnInputDragWeapon(Vector2 position)
        {
           
        }

        void OnShoot()
        {
            _currentBullet--;
        }
        #endregion
    }

}
