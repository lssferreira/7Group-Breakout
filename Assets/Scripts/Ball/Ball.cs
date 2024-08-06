using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float minY = -6f;
    [SerializeField] private float maxVelocity = 8f;
    [SerializeField] private Vector2 initialBallSpeed = Vector2.down * 10f;

    private Rigidbody2D _rigidbody;

    /// <summary>
    /// Método chamado ao iniciar o script. Inicializa o Rigidbody2D e lança a bola.
    /// </summary>
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    /// <summary>
    /// Método chamado a cada frame. Verifica se a bola saiu dos limites e limita a velocidade da bola.
    /// </summary>
    private void Update()
    {
        CheckBallOutOfBounds();
        ClampBallSpeed();
    }

    /// <summary>
    /// Método chamado quando a bola colide com outro objeto.
    /// Se colidir com um tijolo, o tijolo é destruído.
    /// </summary>
    /// <param name="collision">Informações sobre a colisão.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            HandleBrickCollision(collision.gameObject);
        }
    }

    /// <summary>
    /// Inicializa a velocidade da bola.
    /// </summary>
    private void LaunchBall()
    {
        _rigidbody.velocity = initialBallSpeed;
    }

    /// <summary>
    /// Verifica se a bola saiu dos limites e reinicia o jogo se necessário.
    /// </summary>
    private void CheckBallOutOfBounds()
    {
        if (transform.position.y < minY)
        {
            ResetGame();
        }
    }

    /// <summary>
    /// Limita a velocidade da bola ao valor máximo permitido.
    /// </summary>
    private void ClampBallSpeed()
    {
        if (_rigidbody.velocity.magnitude > maxVelocity)
        {
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxVelocity);
        }
    }

    /// <summary>
    /// Lida com a colisão da bola com um tijolo.
    /// </summary>
    /// <param name="brick">O objeto tijolo que foi colidido.</param>
    private void HandleBrickCollision(GameObject brick)
    {
        Debug.Log(brick.name);
        Destroy(brick);
    }

    /// <summary>
    /// Reinicia a posição e velocidade da bola quando ela sai dos limites.
    /// </summary>
    private void ResetGame()
    {
        transform.position = Vector3.zero;
        LaunchBall();
    }
}
