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

    [Header("References")]
    public GameObject IntroUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;
    public Player PlayerScript;
    


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

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            IntroUI.SetActive(false);
            State = GameState.Playing;
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
        }

        if (State == GameState.Playing && lives == 0)
        {
            State = GameState.Dead;
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
        }

        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
}
