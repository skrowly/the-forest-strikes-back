using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PlayGame() { SceneManager.LoadScene("GameScene"); }
    public void LoadMainMenu() { SceneManager.LoadScene("IntroScreen"); }
    public void RestartGame() { SceneManager.LoadScene("GameScene"); }
    public void LoadLegend() { SceneManager.LoadScene("Legend"); }
}