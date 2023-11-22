using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, optionsMenu, classSelectionMenu;
    private bool isMageSelected = false, isArcherSelected = false, isKnightSelected = false;

    private void Awake()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        classSelectionMenu.SetActive(false);
    }

    public void SelectClass()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        classSelectionMenu.SetActive(true);
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        classSelectionMenu.SetActive(false);
    }

    public void BackFromOptions()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        classSelectionMenu.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void Play()
    {
        if (isMageSelected)
        {
            //Debug.Log("Mage");
            GameManager.Instance.SaveChosenClass("Mage");
            SceneManager.LoadScene(1);
        }
        else if (isArcherSelected)
        {
            //Debug.Log("Archer");
            GameManager.Instance.SaveChosenClass("Archer");
            SceneManager.LoadScene(1);
        }
        else if (isKnightSelected)
        {
            //Debug.Log("Knight");
            GameManager.Instance.SaveChosenClass("Knight");
            SceneManager.LoadScene(1);
        }

        //if (GameManager.chosenClass != null)
        //    SceneManager.LoadScene(1);
        //else
        //    Debug.Log("Error: no class chosen");
    }

    public void SelectMage()
    {
        isMageSelected = true;
        isArcherSelected = false;
        isKnightSelected = false;
    }

    public void SelectArcher()
    {
        isMageSelected = false;
        isArcherSelected = true;
        isKnightSelected = false;
    }

    public void SelectKnight()
    {
        isMageSelected = false;
        isArcherSelected = false;
        isKnightSelected = true;
    }
}
