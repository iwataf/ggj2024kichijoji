using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] private GameObject _faderCanvas;

    private void Start()
    {
        DontDestroyOnLoad(_faderCanvas);
        SceneManager.LoadScene("Title");
    }
}
