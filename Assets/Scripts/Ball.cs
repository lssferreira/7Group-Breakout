using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball Instance { get; private set; }

    [SerializeField] private float maxVelocity = 8f;
    [SerializeField] private Vector2 initialBallSpeed = Vector2.down * 10f;
    [SerializeField] private Vector3 initialPosition = new(0, -1.65f, 0);
    [SerializeField] private float maxBounceAngle = 45f; //TODO

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Inicializa o Rigidbody2D e lança a bola.
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    // Verifica se a bola saiu dos limites e limita a velocidade da bola.
    private void Update()
    {
        ClampBallSpeed();
    }

    // Se colidir com um tijolo, o tijolo é destruído.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            HandleBrickCollision(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.RemoveLife();
            ResetBall();
        }

    }

    // Inicializa a velocidade da bola.
    private void LaunchBall()
    {
        _rigidbody.velocity = initialBallSpeed;
        transform.position = initialPosition;
    }

    // Limita a velocidade da bola ao valor máximo permitido.
    private void ClampBallSpeed()
    {
        if (_rigidbody.velocity.magnitude > maxVelocity)
        {
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxVelocity);
        }
    }

    // Lida com a colisão da bola com um tijolo.
    private void HandleBrickCollision(GameObject brick)
    {

        Debug.Log(brick.name);
        Destroy(brick);

        GameManager.Instance.UpdateStateGame();
    }

    // Reinicia a posição e velocidade da bola quando ela sai dos limites.
    public void ResetBall()
    {
        transform.position = initialPosition;
        LaunchBall();
    }
}
