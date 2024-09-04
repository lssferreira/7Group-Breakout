using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager InputManagerGM { get; private set; }

    [Header("Level Management")]
    [SerializeField]
    public int CurrentLevelIndex = 0;
    public LevelGenerator levelGenerator;


    [Header("UI")]
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LevelText;
    public GameObject[] LivesImage;
    public int Score { get; private set; }
    public int Lives { get; private set; }

    [Header("Extra")]
    [SerializeField] public bool GodMode = false;

    public Ball BallInfo;

    private AudioSource Sound;

    public int GetRandomIntNumber() => UnityEngine.Random.Range(0, 100);

    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;

        Sound = GetComponent<AudioSource>();

        InputManagerGM = new InputManager();
    }

    private void Start()
    {
        if (levelGenerator == null)
        {
            levelGenerator = FindObjectOfType<LevelGenerator>();
        }

        if (BallInfo == null)
        {
            BallInfo = FindObjectOfType<Ball>();
        }

        Score = 0;
        Lives = 3;

        LoadNextLevel();
    }

    // Avança para o próximo nível, se houver.
    private void LoadNextLevel()
    {
        Debug.Log($"Iniciando Novo Nivel");
        Handheld.Vibrate();

        if (levelGenerator != null)
        {
            if (CurrentLevelIndex != 0)
            {
                Sound.Play();
            }

            levelGenerator.LoadNewLevel();
            UpdateUi();
        }
    }

    private void UpdateUi()
    {
        LevelText.text = $"Nível {CurrentLevelIndex + 1}";
    }

    private void OnLevelCompleted()
    {
        Debug.Log($"Nivel Completado");

        CurrentLevelIndex++;

        LoadNextLevel();
        BallInfo.ResetBall();
    }

    private void AddScore(int amount)
    {
        Score += amount;
        ScoreText.text = Score.ToString("00000");
        Debug.Log($"Score Atual: {Score}");
    }

    private void AddScore()
    {
        // TODO: Alterar para pegar o valor do bloco
        AddScore(10);
    }

    public void RemoveLife()
    {
        if (GodMode)
            return;

        Lives--;
        Debug.Log($"Vidas Restantes: {Lives}");

        if (Lives < 0)
        {
            Debug.Log($"Game Over");
        }
        else
        {
            LivesImage[Lives].SetActive(false);
        }
    }

    internal void UpdateStateGame()
    {
        AddScore();
        var brickCount = GetBricksCount();

        Debug.Log($"Tijolos Restantes: {brickCount}");
        if (brickCount <= 0)
        {
            OnLevelCompleted();
        }
    }

    private int GetBricksCount()
    {
        return levelGenerator.transform.childCount - 1;
    }
}