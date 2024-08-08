using System;
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
    public int TotalBricks { get; private set; }
    public int GetRandomIntNumber() => UnityEngine.Random.Range(0, 100);

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
    }

    // Avança para o próximo nível, se houver.
    private void LoadNextLevel()
    {
        if (levelGenerator != null)
        {
            levelGenerator.LoadNextLevel();
        }
    }

    private void OnLevelCompleted()
    {
        LoadNextLevel();
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

    internal void UpdateStateGame()
    {
        AddScore();

        TotalBricks--;

        var brickCount = TotalBricks;

        if (brickCount <= 0)
        {
            OnLevelCompleted();
        }
    }

    internal void UpdateNextLevelTotalBricks(int childCount)
    {
        TotalBricks = childCount;

        Debug.Log($"Count Bricks: {TotalBricks}");
    }
}