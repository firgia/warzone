using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
namespace Character
{
    [ExecuteInEditMode]
    public class Player : MonoBehaviour
    {
        enum CharacterDirection{
            Left,
            Right
        }

        [Header("Character Setting")]
        [SerializeField] private float offsetRotation = 0;
        [SerializeField] private GameObject hand;
        [SerializeField] private CharacterDirection direction = CharacterDirection.Right;
        [SerializeField] private Animator animatorHead;

        private CharacterDirection prevRotSelected = CharacterDirection.Right;
        private bool isRotationValid;
        
        /// <summary>
        /// true jika user melakukan drag dari atas, depan dan bawah arah character
        /// false jika user melakukan drag ke arah belakang character
        /// </summary>
        public bool IsRotationValid => isRotationValid;
        private bool isAiming;
        private bool inputValid = false;

        void Start()
        {

        }

        void Update()
        {
            CharacterRotationController();
            if (EditorApplication.isPlaying)
            {
                HandController();
                AnimationController();
            }
        }

        #region Hand
        /// <summary>
        /// mengatur posisi tangan berdasarkan input
        /// </summary>
        private void HandController()
        {
            isAiming = false;
            bool isHoverUI = EventSystem.current.IsPointerOverGameObject();
        

            if (Input.touchSupported)
            {

                if(Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began) inputValid = !isHoverUI;
                    else if(inputValid)
                    {
                        SetRotationHand(touch.position);
                        isAiming = true;
                    }
                  
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0)) inputValid = !isHoverUI;
                else if (inputValid)
                {
                    if (Input.GetMouseButton(0))
                    {
                        SetRotationHand(Input.mousePosition);
                        isAiming = true;
                    }
                }
            }
        }

        /// <summary>
        /// di gunakan untuk menentukan rotasi dari tangan player
        /// </summary>
        /// <param name="inputPosition">set posisi berdasarkan input</param>
        private void SetRotationHand(Vector3 inputPosition)
        {
            isRotationValid = IsTargetRotationValid(Camera.main.ScreenToWorldPoint(inputPosition));
            if (!isRotationValid) return;

            Vector3 diff = Camera.main.ScreenToWorldPoint(inputPosition) - hand.transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            hand.transform.localRotation = Quaternion.Euler(0f, 0f, ((direction == CharacterDirection.Right)? rot_z : -(rot_z + 180)) + offsetRotation);
        }
    
        #endregion

        #region Character Rotation

        private void CharacterRotationController()
        {
             if(prevRotSelected != direction)
             {
                if(direction == CharacterDirection.Left)
                { 
                    Debug.Log("mengubah posisi character ke arah kiri");
                    SetCharacterToLeft();
                }
                else
                {
                    Debug.Log("mengubah posisi character ke arah kanan");
                    SetCharacterToRight();
                }
          
                prevRotSelected = direction;
             }
        }

        private void SetCharacterToLeft() => transform.localRotation = Quaternion.Euler(0, 180, 0);
   
        private void SetCharacterToRight() => transform.localRotation = Quaternion.Euler(0, 0, 0);
        #endregion

        #region Validator Helper

        /// <summary>
        /// apakah posisi input untuk tangan sudah valid,
        /// 
        /// dinyatakan valid jika input dari tangan tidak lebih dari 180 derajat
        /// </summary>
        /// <param name="inputWorldPosition"></param>
        /// <returns></returns>
        private bool IsTargetRotationValid(Vector3 inputWorldPosition)
        {
            return (
                (direction == CharacterDirection.Right && inputWorldPosition.x > transform.position.x) ||
                (direction == CharacterDirection.Left && inputWorldPosition.x < transform.position.x)
                );
        }

        #endregion

        #region Animasi
        void AnimationController()
        {
            if (isAiming) AnimAngry();
            else AnimIdle();
        }

        void AnimAngry()=> animatorHead.SetBool("Angry", true);

        void AnimDeath() => animatorHead.SetBool("Death", true);

        void AnimIdle() => animatorHead.SetBool("Angry", false);
        #endregion
    }
}