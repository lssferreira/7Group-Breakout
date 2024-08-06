using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7;
    private float screenHalfWidthInWorldUnits;

    private void Start()
    {
        // Calcular metade da largura da tela em unidades do mundo
        float halfPaddleWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfPaddleWidth;
    }

    private void Update()
    {
        float moveDirection = GameManager.Instance.inputManager.Movement * Time.deltaTime * moveSpeed;
        transform.Translate(moveDirection, 0, 0);

        float clampedX = Mathf.Clamp(transform.position.x, -screenHalfWidthInWorldUnits, screenHalfWidthInWorldUnits);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}