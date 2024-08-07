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

    // Probabilidade de spawn dos tijolos, variando de 0 a 1.
    [SerializeField] private float spawnProbability = 0.5f;


    // Método chamado ao inicializar o script. Gera a grade de tijolos.
    private void Awake()
    {
        GenerateLevel();
    }

    // Gera a grade de tijolos com base no tamanho e deslocamento definidos.
    private void GenerateLevel()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (Random.value > spawnProbability)
                    continue;

                // Cria o tijolo e define sua posição
                GameObject newBrick = CreateBrick(x, y);

                // Define a cor do tijolo
                SetBrickColor(newBrick, y);

                // Define o tamanho do tijolo apenas no eixo X
                SetBrickSize(newBrick, x, y);

                Debug.Log($"Brick at ({x}, {y}) color: {newBrick.GetComponent<SpriteRenderer>().color} size: {newBrick.transform.localScale}");
            }
        }
    }

    // Cria o tijolo e define sua posição.
    private GameObject CreateBrick(int x, int y)
    {
        // Instancia o tijolo
        GameObject newBrick = Instantiate(brickPrefab, transform);

        // Calcula a posição do tijolo
        Vector3 brickPosition = CalculateBrickPosition(x, y);
        newBrick.transform.position = brickPosition;

        return newBrick;
    }

    // Define a cor do tijolo com base na posição vertical.
    private void SetBrickColor(GameObject brick, int y)
    {
        SpriteRenderer brickRenderer = brick.GetComponent<SpriteRenderer>();
        brickRenderer.color = CalculateBrickColor(y);
    }

    // Define o tamanho do tijolo apenas no eixo X.
    private void SetBrickSize(GameObject brick, int x, int y)
    {
        // Tamanhos dos tijolos
        float originalSizeX = 1.0f; // Tamanho original em X
        float halfSizeX = originalSizeX * 0.5f; // Metade do tamanho original em X

        // Obtenha a escala atual do tijolo
        Vector3 brickScale = brick.transform.localScale;

        // Alterna o tamanho do tijolo apenas no eixo X
        if ((x + y) % 2 == 0)
        {
            brickScale.x = originalSizeX; // Tamanho original em X
        }
        else
        {
            brickScale.x = halfSizeX; // Metade do tamanho original em X
        }

        // Aplique a nova escala ao tijolo
        brick.transform.localScale = brickScale;
    }

    // Calcula a cor do tijolo com base na posição vertical na grade.
    private Color CalculateBrickColor(int y)
    {
        float colorPosition = (float)y / (gridSize.y - 1);

        Debug.Log($"colorPosition for y={y}: {colorPosition}"); // Debug: Log color position

        return brickGradient.Evaluate(colorPosition);
    }

    // Calcula a posição do tijolo na grade.
    private Vector3 CalculateBrickPosition(int x, int y)
    {
        float posX = ((gridSize.x - 1) * 0.5f - x) * gridOffset.x;
        float posY = y * gridOffset.y;

        return transform.position + new Vector3(posX, posY, 0);
    }

}
