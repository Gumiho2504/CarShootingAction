using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button leaveButton;
    public void Start()
    {
        playButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        leaveButton.onClick.AddListener(Application.Quit);
    }
}
