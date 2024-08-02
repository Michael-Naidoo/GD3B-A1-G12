using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void HowToPlayScene()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }

    public void BackToMenuScene()
    {
        SceneManager.LoadScene("startScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
