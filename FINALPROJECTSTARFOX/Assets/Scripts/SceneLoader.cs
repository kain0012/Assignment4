using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//Made this part on my own :D
//Each different method is used by a button on either the mainMenu
//or the corresponding sub-menus
public class SceneLoader : MonoBehaviour {

	// Starts game from main menu
    public void readyPlayerOne()
    {
        SceneManager.LoadScene("Level1");
    }

    // Shows Controls from Main Menu
    public void controls()
    {
        SceneManager.LoadScene("Controls");
    }

    // Takes Player Back to Main Menu
    public void backToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Shows Highscores from MainMenu
    public void highScore()
    {
        SceneManager.LoadScene("HighScores");
    }

    // Shows Credits from MainMenu
    public void credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
