using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void moveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID); //append this script to an object or a trigger section then specify the scene ID (find scene IDs via Edit > Build Profiles)
    }

    public void quit()
    {
        Application.Quit();
    }
}