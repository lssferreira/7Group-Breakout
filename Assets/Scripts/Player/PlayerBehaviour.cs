using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private float screenHalfWidthInWorldUnits;

    private void Start()
    {
        // Calcular metade da largura da tela em unidades do mundo
        float halfPaddleWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfPaddleWidth;
    }

    private void Update()
    {
        float horizontalInput = 0;

        // Controle por toque ou mouse
        if (Input.GetMouseButton(0))
        {
            var center = Screen.width / 2;
            var mousePosition = Input.mousePosition;

            if (mousePosition.x > center)
            {
                horizontalInput = 1;
            }
            else if (mousePosition.x < center)
            {
                horizontalInput = -1;
            }
        }
        else
        {
            // Controle por teclado
            horizontalInput = Input.GetAxis("Horizontal");
        }

        // Movimentar o jogador
        float moveDirection = horizontalInput * Time.deltaTime * moveSpeed;
        transform.Translate(moveDirection, 0, 0);

        // Limitar a posição do jogador dentro da tela
        float clampedX = Mathf.Clamp(transform.position.x, -screenHalfWidthInWorldUnits, screenHalfWidthInWorldUnits);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}