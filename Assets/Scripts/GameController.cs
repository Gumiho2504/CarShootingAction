using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text lifeText;
    [SerializeField] public int score;
    public GameObject gameOverPanel;
    public GameObject mainCamera;
    public GameObject playerCamera;
    public float speed = 10;
    bool isSwitch;
    bool isScoreChanged = false;

    void Update()
    {

    }

    void Start()
    {
        score = 0;
        scoreText.text = score.ToString("D2");
        lifeText.text = "5";
    }


    public void OnSwitchCamera()
    {
        isSwitch = !isSwitch;
        mainCamera.SetActive(!isSwitch);
        playerCamera.SetActive(isSwitch);
    }


    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString("D2");
        speed = 10 + score / 100;
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
    }
    public void OnLifeChange(int life)
    {
        lifeText.text = life.ToString();
    }

    public void OnBackHome()
    {
        SceneManager.LoadScene(0);
    }

    public void OnReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
