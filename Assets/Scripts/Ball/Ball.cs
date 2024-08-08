using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float minY = -6f;
    [SerializeField] private float maxVelocity = 8f;
    [SerializeField] private Vector2 initialBallSpeed = Vector2.down * 10f;

    private Rigidbody2D _rigidbody;

    // Inicializa o Rigidbody2D e lança a bola.
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    // Verifica se a bola saiu dos limites e limita a velocidade da bola.
    private void Update()
    {
        CheckBallOutOfBounds();
        ClampBallSpeed();
    }

    // Se colidir com um tijolo, o tijolo é destruído.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            HandleBrickCollision(collision.gameObject);
        }
    }

    // Inicializa a velocidade da bola.
    private void LaunchBall()
    {
        _rigidbody.velocity = initialBallSpeed;
    }

    // Verifica se a bola saiu dos limites e reinicia o jogo se necessário.
    private void CheckBallOutOfBounds()
    {
        if (transform.position.y < minY)
        {
            GameManager.Instance.RemoveLife();
            ResetGame();
        }
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
        GameManager.Instance.AddScore();
        Debug.Log(brick.name);
        Destroy(brick);
    }

    // Reinicia a posição e velocidade da bola quando ela sai dos limites.
    private void ResetGame()
    {
        transform.position = Vector3.zero;
        LaunchBall();
    }
}
