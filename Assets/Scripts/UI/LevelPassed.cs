using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelPassed : MonoBehaviour
{
    [SerializeField] private Text textStar;
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
        if (rules.IsFinishGamePlay() && rules.LevelPassed() && !isShowDialog)
        {
            ShowDialog();
        }
    }

    void ShowDialog()
    {
        textStar.text = rules.GetStars().ToString() + " stars";
        dialog.SetActive(true);
        panel.SetActive(true);
        isShowDialog = true;
    }

    public void NextLevel()
    {

    }
    public void Restart()
    {
        string thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene);
    }
}
