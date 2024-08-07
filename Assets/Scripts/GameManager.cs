using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager inputManager { get; private set; }

    internal int GetRandomIntNumber()
    {
        var number = Random.Range(0, 100);
        return number;
    }


    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;

        inputManager = new InputManager();
    }
}