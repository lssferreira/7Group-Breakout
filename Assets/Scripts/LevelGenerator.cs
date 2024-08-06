using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Tamanho da grade de tijolos a ser gerada (largura x altura).
    [SerializeField] private Vector2Int gridSize;

    // Distância entre os tijolos na grade.
    [SerializeField] private Vector2 gridOffset;

    // Prefab do tijolo a ser instanciado.
    [SerializeField] private GameObject brickPrefab;

    // Gradiente de cores a ser aplicado aos tijolos com base na posição vertical.
    [SerializeField] private Gradient brickGradient;

    /// <summary>
    /// Método chamado ao inicializar o script. Gera a grade de tijolos.
    /// </summary>
    private void Awake()
    {
        GenerateLevel();
    }

    /// <summary>
    /// Gera a grade de tijolos com base no tamanho e deslocamento definidos.
    /// </summary>
    private void GenerateLevel()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                // Instancia o tijolo
                GameObject newBrick = Instantiate(brickPrefab, transform);
                
                // Calcula a posição do tijolo
                Vector3 brickPosition = CalculateBrickPosition(x, y);
                newBrick.transform.position = brickPosition;

                // Define a cor do tijolo com base na posição vertical
                SpriteRenderer brickRenderer = newBrick.GetComponent<SpriteRenderer>();
                brickRenderer.color = CalculateBrickColor(y);
            }
        }
    }

    /// <summary>
    /// Calcula a posição do tijolo na grade.
    /// </summary>
    /// <param name="x">Posição x na grade.</param>
    /// <param name="y">Posição y na grade.</param>
    /// <returns>A posição do tijolo em coordenadas do mundo.</returns>
    private Vector3 CalculateBrickPosition(int x, int y)
    {
        float posX = ((gridSize.x - 1) * 0.5f - x) * gridOffset.x;
        float posY = y * gridOffset.y;
        return transform.position + new Vector3(posX, posY, 0);
    }

    /// <summary>
    /// Calcula a cor do tijolo com base na posição vertical na grade.
    /// </summary>
    /// <param name="y">Posição y na grade.</param>
    /// <returns>A cor calculada do tijolo.</returns>
    private Color CalculateBrickColor(int y)
    {
        float colorPosition = (float)y / (gridSize.y - 1);
        return brickGradient.Evaluate(colorPosition);
    }
}
