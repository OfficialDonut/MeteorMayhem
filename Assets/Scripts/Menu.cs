using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool IsLoaded { get; private set; }

    private void Start()
    {
        IsLoaded = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(LoadGame());
    }

    private static IEnumerator LoadGame()
    {
        yield return SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        SceneManager.UnloadSceneAsync("Menu");
        IsLoaded = false;
    }
}
