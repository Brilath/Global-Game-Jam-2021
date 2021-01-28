using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGrabbyCrabby()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGrabbyCrabby()
    {
        Debug.Log("Sad to see you go :(");

        Application.Quit();
    }
}
