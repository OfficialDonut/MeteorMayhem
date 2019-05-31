using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
