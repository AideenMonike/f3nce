using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

/*"SceneTransitionManager" script is responsible for managing 
 scene transitions with a fade effect using the SceneFade script.*/
public class SceneTransitionManager : MonoBehaviour
{
    public SceneFade fadeScreen;

    /*"GoToScene" function is used to initiate a scene transition to the specified scene index.
        Parameters:
            sceneIndex: The build index of the destination scene to transition to.*/
    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    /*"GoToSceneRoutine" Coroutine performs the scene transition with a fade effect.
        Parameters:
            sceneIndex: The build index of the destination scene to transition to.*/
    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        Scene current = SceneManager.GetActiveScene();
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration); 

        SceneManager.LoadScene(sceneIndex);
        
        /* yield return null;
        SceneManager.UnloadSceneAsync(current);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex)); */
    }
}
