using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public SceneFade fadeScreen;

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        Scene current = SceneManager.GetActiveScene();
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration); 

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
        yield return null;
        SceneManager.UnloadSceneAsync(current);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
    }
}
