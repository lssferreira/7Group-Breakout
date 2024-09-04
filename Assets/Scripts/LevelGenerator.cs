using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private LevelData[] levels;  // Array de níveis
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Gradient brickGradient;

    private float minX, maxX, minY, maxY;

    public void LoadNewLevel()
    {
        CalculateVisibleBounds();
        GenerateLevel();
    }

    private void CalculateVisibleBounds()
    {
        Camera camera = Camera.main;
        if (camera == null) return;

        // Calcula os limites da tela visível com base na posição da câmera e no tamanho da viewport
        float camHalfHeight = camera.orthographicSize;
        float camHalfWidth = camHalfHeight * camera.aspect;

        Vector3 camPosition = camera.transform.position;

        minX = camPosition.x - camHalfWidth;
        maxX = camPosition.x + camHalfWidth;
        minY = camPosition.y - camHalfHeight;
        maxY = camPosition.y + camHalfHeight;
    }

    private void GenerateLevel()
    {
        LevelData levelData = levels[GameManager.Instance.CurrentLevelIndex];

        for (int x = 0; x < levelData.gridSize.x; x++)
        {
            for (int y = 0; y < levelData.gridSize.y; y++)
            {
                if (GameManager.Instance.GetRandomIntNumber() > levelData.spawnProbability)
                    continue;

                GameObject newBrick = CreateBrick(x, y, levelData);

                // Verifica se o bloco está dentro dos limites visíveis
                if (IsBrickWithinBounds(newBrick.transform.position))
                {
                    SetBrickColor(newBrick, y, levelData.gridSize.y);
                    SetBrickSize(newBrick, x, y);
                }
                else
                {
                    Destroy(newBrick); // Remove o bloco se estiver fora dos limites
                }
            }
        }
    }

    private GameObject CreateBrick(int x, int y, LevelData levelData)
    {
        GameObject newBrick = Instantiate(brickPrefab, transform);
        Vector3 brickPosition = CalculateBrickPosition(x, y, levelData);
        newBrick.transform.position = brickPosition;
        return newBrick;
    }

    private void SetBrickColor(GameObject brick, int y, int gridHeight)
    {
        SpriteRenderer brickRenderer = brick.GetComponent<SpriteRenderer>();
        brickRenderer.color = CalculateBrickColor(y, gridHeight);
    }

    private void SetBrickSize(GameObject brick, int x, int y)
    {
        float originalSizeX = 1.0f;
        float halfSizeX = originalSizeX * 0.5f;
        Vector3 brickScale = brick.transform.localScale;

        if ((x + y) % 2 == 0)
        {
            brickScale.x = originalSizeX;
        }
        else
        {
            brickScale.x = halfSizeX;
        }

        brick.transform.localScale = brickScale;
    }

    private Color CalculateBrickColor(int y, int gridHeight)
    {
        float colorPosition = (float)y / (gridHeight - 1);
        return brickGradient.Evaluate(colorPosition);
    }

    private Vector3 CalculateBrickPosition(int x, int y, LevelData levelData)
    {
        float posX = ((levelData.gridSize.x - 1) * 0.5f - x) * levelData.gridOffset.x;
        float posY = y * levelData.gridOffset.y;
        return transform.position + new Vector3(posX, posY, 0);
    }

    private bool IsBrickWithinBounds(Vector3 position)
    {
        // Verifica se a posição do bloco está dentro dos limites visíveis
        float brickWidth = brickPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
        float brickHeight = brickPrefab.GetComponent<SpriteRenderer>().bounds.extents.y;

        return (position.x - brickWidth >= minX &&
                position.x + brickWidth <= maxX &&
                position.y - brickHeight >= minY &&
                position.y + brickHeight <= maxY);
    }
}
