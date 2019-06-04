using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
}
