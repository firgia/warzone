using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
    {
    [ExecuteInEditMode]
    public class PlayerController : MonoBehaviour
    {
        enum CharacterDirection{
            Left,
            Right
        }

        [Header("Character Setting")]
        [SerializeField] private GameObject hand;
        [SerializeField] private CharacterDirection direction = CharacterDirection.Right;

        private CharacterDirection prevRotSelected = CharacterDirection.Right;
        private bool _isInputValid;
        
        /// <summary>
        /// true jika user melakukan drag dari atas, depan dan bawah arah character
        /// false jika user melakukan drag ke arah belakang character
        /// </summary>
        public bool isInputValid => _isInputValid;

        void Start()
        {

        }

        void Update()
        {
            CharacterRotationController();
            HandController();
        }

        #region Hand
        private void HandController()
        {
            if (Input.touchSupported)
            {
                if(Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    SetRotationHand(touch.position);
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    SetRotationHand(Input.mousePosition);
                }
            }
        }

        /// <summary>
        /// di gunakan untuk menentukan rotasi dari tangan player
        /// </summary>
        /// <param name="inputPosition">set posisi berdasarkan input</param>
        private void SetRotationHand(Vector3 inputPosition)
        {
            _isInputValid = IsInputHandValid(Camera.main.ScreenToWorldPoint(inputPosition));
            if (!_isInputValid) return;

            Vector3 diff = Camera.main.ScreenToWorldPoint(inputPosition) - hand.transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            hand.transform.localRotation = Quaternion.Euler(0f, 0f, (direction == CharacterDirection.Right)? rot_z : -(rot_z + 180));
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
        private bool IsInputHandValid(Vector3 inputWorldPosition)
        {
            return (
                (direction == CharacterDirection.Right && inputWorldPosition.x > transform.position.x) ||
                (direction == CharacterDirection.Left && inputWorldPosition.x < transform.position.x)
                );
        }

        #endregion
    }
}