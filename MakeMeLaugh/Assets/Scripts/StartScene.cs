using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] private GameObject _faderCanvas;
    [SerializeField] private GameObject _sound;

    private void Start()
    {
        DontDestroyOnLoad(_faderCanvas);
        DontDestroyOnLoad(_sound);

        SceneManager.LoadScene("Title");
    }
}
