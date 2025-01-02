using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState 
{
    Intro,
    Playing,
    Dead
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State = GameState.Intro;

    public int lives = 3;

    public float PlayStartTime;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;
    public Player PlayerScript;
    public TMP_Text scoreText;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        IntroUI.SetActive(true);
    }

    public float CalculateGameSpeed()
    {
        if(State != GameState.Playing)
        {
            return 1f;
        }
        float speed = 1f + (CalculateScore() / 10) * 0.3f;
        return Mathf.Min(speed, 20f);
    }

    float CalculateScore()
    {
        return Time.time - PlayStartTime;
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if(score > currentHighScore){
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
        
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.Playing){
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
        }
        else if (State == GameState.Dead){
            scoreText.text = "High Score: " + GetHighScore();
        }
        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            IntroUI.SetActive(false);
            State = GameState.Playing;
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
            PlayStartTime = Time.time;
        }

        if (State == GameState.Playing && lives == 0)
        {
            State = GameState.Dead;
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            DeadUI.SetActive(true);
            SaveHighScore();
        }

        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
}
