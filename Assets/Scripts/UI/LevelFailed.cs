using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.SceneManagement;

public class LevelFailed : MonoBehaviour
{
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject panel;
    private RulesGameplay rules;

    private bool isShowDialog = false;

    void Start()
    {
        rules = GameObject.FindGameObjectWithTag(TagUtils.Rules).GetComponent<RulesGameplay>();
        dialog.SetActive(false);
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rules.IsFinishGamePlay() && rules.LevelFailed() && !isShowDialog)
        {
            ShowDialog();
        }
    }

    void ShowDialog()
    {
        dialog.SetActive(true);
        panel.SetActive(true);
        isShowDialog = true;
    }

    public void Restart()
    {
        string thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
