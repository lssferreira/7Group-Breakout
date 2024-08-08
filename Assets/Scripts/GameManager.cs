using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager InputManagerGM { get; private set; }
    public LevelGenerator levelGenerator;

    public TextMeshProUGUI ScoreText;
    public GameObject[] LivesImage;
    public int Score { get; private set; }
    public int Lives { get; private set; }
    public int GetRandomIntNumber() => Random.Range(0, 100);

    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;

        InputManagerGM = new InputManager();
    }

    private void Start()
    {
        if (levelGenerator == null)
        {
            levelGenerator = FindObjectOfType<LevelGenerator>();
        }

        Score = 0;
        Lives = 3;

        levelGenerator.GenerateLevel();
        levelGenerator.GenerateLevel();
    }

    // Avança para o próximo nível, se houver.
    public void LoadNextLevel()
    {
        if (levelGenerator != null)
        {
            levelGenerator.LoadNextLevel();
        }
    }

    private void AddScore(int amount)
    {
        Score += amount;
        ScoreText.text = Score.ToString("00000");
        Debug.Log($"Score Atual: {Score}");
    }

    public void AddScore()
    {
        // TODO: Alterar para pegar o valor do bloco
        AddScore(10);
    }

    public void RemoveLife()
    {
        Lives--;

        if (Lives < 0)
        {
            Debug.Log($"Game Over");
        }
        else
        {
            LivesImage[Lives].SetActive(false);
        }
        Debug.Log($"Vidas Restantes: {Lives}");
    }

}