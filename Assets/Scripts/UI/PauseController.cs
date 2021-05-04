using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        private void Start()
        {
            panel.SetActive(false);
        }

        public void Paused()
        {
            Time.timeScale = 0;
            panel.SetActive(true);
        }

        public void Continue() 
        {
            Time.timeScale = 1;
            panel.SetActive(false);
        }

        public void GoToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}