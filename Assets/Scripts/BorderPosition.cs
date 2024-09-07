using UnityEngine;

public class BorderPositioner : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rightBorder;
    public GameObject topBorder;
    public GameObject bottomBorder;

    public void PositionBorders()
    {
        // Calcula os limites da tela vis�vel com base na posi��o da c�mera e no tamanho da viewport
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = camHalfHeight * Camera.main.aspect;

        Vector3 camPosition = Camera.main.transform.position;

        // Posiciona os objetos de acordo com as bordas vis�veis da tela

        // Borda Esquerda
        if (leftBorder != null)
        {
            leftBorder.transform.position = new Vector3(camPosition.x - camHalfWidth, camPosition.y, 0f);
        }

        // Borda Direita
        if (rightBorder != null)
        {
            rightBorder.transform.position = new Vector3(camPosition.x + camHalfWidth, camPosition.y, 0f);
        }

        // Borda Superior
        if (topBorder != null)
        {
            topBorder.transform.position = new Vector3(camPosition.x, camPosition.y + camHalfHeight, 0f);
        }

        // Borda Inferior
        if (bottomBorder != null)
        {
            bottomBorder.transform.position = new Vector3(camPosition.x, camPosition.y - camHalfHeight, 0f);
        }
    }
}
