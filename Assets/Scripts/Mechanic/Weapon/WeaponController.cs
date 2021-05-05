using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using UnityEngine.EventSystems;

namespace Weapon
{
    [RequireComponent(typeof(LineRenderer))]
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform projectilePos;
        [SerializeField] private Transform targetDirectionShoot;
        [SerializeField] private BulletController bulletPrefab;
        [SerializeField] private int totalBullet = 3;
        [SerializeField] private AudioSource audioSource;

        private int currentBullet;
        private LineRenderer lineRenderer;
        private Ray2D ray;
        private RaycastHit2D hit;

        /// <summary>
        /// sisa peluru saat ini
        /// </summary>
        public int CurrentBullet => currentBullet;
        
        /// <summary>
        /// total maksimal peluru
        /// </summary>
        public int TotalBullet => totalBullet;

        bool inputValid;

        private void Awake()
        {
            currentBullet = totalBullet;
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
             lineRenderer.widthMultiplier = 0.2f;
        }


        void Update()
        {
            InputController();
        }

        /// <summary>
        /// penembakan akan di jalankan ketika pengguna melakukan UP input (melepas jari dari layar / melepas tekanan input kiri mouse)
        /// </summary>
        void InputController()
        {
            bool isHoverUI = EventSystem.current.IsPointerOverGameObject();
            if (Input.touchSupported)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);


                    if(touch.phase == TouchPhase.Began) inputValid = !isHoverUI;
                    else if (inputValid)
                    {
                        if(touch.phase == TouchPhase.Moved)
                        {
                            OnInputDragWeapon(touch.position);
                        }
                        else if(touch.phase == TouchPhase.Ended)
                        {
                            if (currentBullet > 0)
                            {
                                Shoot();
                            }
                        }
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))  inputValid = !isHoverUI;
                else if (inputValid)
                {
                    if (Input.GetMouseButton(0))
                    {
                        OnInputDragWeapon(Input.mousePosition);
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        if (currentBullet > 0)
                        {
                            Shoot();
                        }
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
            OnShoot();
            Vector2 _startPos = new Vector2(projectilePos.transform.position.x, projectilePos.transform.position.y);
            Vector2 _endPos = new Vector2(targetDirectionShoot.transform.position.x, targetDirectionShoot.transform.position.y);
            Vector2 dir = (_endPos - _startPos).normalized;

            BulletController newBullet = Instantiate(bulletPrefab,projectilePos.position,transform.rotation);
            newBullet.Shoot(dir);
        }

        /// <summary>
        /// membuat garis untuk memprediksi arah peluru
        /// </summary>
        void DrawLine()
        {
            ray = new Ray2D(projectilePos.position, projectilePos.right);

            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, projectilePos.position);

            float lenght = 5;

            for (int i = 0; i < 2; i++)
            {
                hit = Physics2D.Raycast(ray.origin, ray.direction, lenght);
                if (hit)
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                    ray = new Ray2D(hit.point, Vector2.Reflect(ray.direction, hit.normal));
                    lenght -= Vector2.Distance(ray.origin, hit.point);
                }
                else
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * lenght);
                }
            }
        }

        /// <summary>
        /// menghapus garis prediksi arah peluru
        /// </summary>
        void RemoveLine()
        {
            lineRenderer.positionCount = 0;
        }
        #region handler 
        /// <summary>
        /// ketika pengguna sedang melakukan drag
        /// </summary>
        /// <param name="position"></param>
        void OnInputDragWeapon(Vector2 position)
        {
            DrawLine();
        }

        /// <summary>
        /// ketika terjadi penembakan
        /// </summary>
        void OnShoot()
        {
            audioSource.Play();
            currentBullet--;
            RemoveLine();
        }
        #endregion
    }

}
