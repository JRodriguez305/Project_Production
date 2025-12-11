using UnityEngine;
using UnityEngine.SceneManagement;

public class PillBottleBehaviour : MonoBehaviour
{
    void OnMouseDown()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);

            SceneManager.GetSceneByBuildIndex(nextSceneIndex);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
