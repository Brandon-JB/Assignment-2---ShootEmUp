using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainButtons;
    public GameObject Instructions;
    public GameObject BackButton;

    private void Start()
    {
        MainButtons.SetActive(true);
        Instructions.SetActive(false);
        BackButton.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level");
    }

    public void Instruction()
    {
        MainButtons.SetActive(false);
        Instructions.SetActive(true);
        BackButton.SetActive(true);
    }

    public void Quit()
    {
       Application.Quit();
    }

    public void Back()
    {
        MainButtons.SetActive(true);
        Instructions.SetActive(false);
        BackButton.SetActive(false);
    }
}
