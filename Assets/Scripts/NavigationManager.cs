
   using UnityEngine;
    using UnityEngine.SceneManagement;

    public class TitleScreenManager : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void OnLevelOne()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void OnLevelTwo()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void OnLevelThree()
    {
        SceneManager.LoadScene("Boss Level");
    }
    public void OnBossLevel()
    {
        SceneManager.LoadScene("Title Screen");
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }

    // Add other methods for options, etc.
}  

