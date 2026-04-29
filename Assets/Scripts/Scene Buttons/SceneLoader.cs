using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PlayGame()
    {
        GameData.startingWave = 1;
        SceneManager.LoadScene("GameScene");
    }
    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }
    public void LevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("IntroScene");
    }
    public void RestartGame()
    {
        GameData.startingWave = 1;
        SceneManager.LoadScene("GameScene");
    }
    public void LoadLegend()
    {
        SceneManager.LoadScene("Legend");
    }
    //this is just for being able to load each level - wave
    public void TestWave1()
    {
        GameData.startingWave = 1;
        SceneManager.LoadScene("GameScene");
    }
    public void TestWave2()
    {
        GameData.startingWave = 2;
        SceneManager.LoadScene("GameScene");
    }

    public void TestWave3()
    {
        GameData.startingWave = 3;
        SceneManager.LoadScene("GameScene");
    }
}