using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class myScenesManager : MonoBehaviour
{
    [SerializeField] static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = GameObject.Find("SceneManager");
        }
        DontDestroyOnLoad(instance);
    }

    public enum Scenes
    {
        MainMenu,
        Game,
        Boss
    }

    public void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Scenes.Game.ToString());
    }

    public void StartBoss()
    {
        SceneManager.LoadScene(Scenes.Boss.ToString());
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
