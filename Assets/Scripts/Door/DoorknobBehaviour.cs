using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorknobBehaviour : MonoBehaviour
{
    public SceneFaderBehaviour sceneTransition;

    void OnMouseDown()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            System.Action loadSceneAction = () =>
            {
                SceneManager.LoadScene(nextSceneIndex);

                Scene nextScene = SceneManager.GetSceneByBuildIndex(nextSceneIndex);
                if (nextScene.name == "Win_Scene")
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            };

            sceneTransition.FadeOut(FadeType.Goop, loadSceneAction);
        }
    }
}
