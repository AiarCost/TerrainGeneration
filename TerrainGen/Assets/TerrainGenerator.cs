using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 20;

    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float Tiers = 5f;

    public bool TieredHeight = false;
    public bool IslandHeight = false;
    public void Update()
    {
        Terrain terrain = GetComponent<Terrain>();

        terrain.terrainData = GenerateTerrain(terrain.terrainData);

    }

    TerrainData GenerateTerrain ( TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }



    float [,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        if (TieredHeight)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    heights[x, y] = TieredHeightCalculate(x, y, heights[x, y]);
                }
            }
        }

        if (IslandHeight)
        {
            for(int x = 0; x < width; x++)
            {
                for( int y = 0; y < height; y++)
                {
                    heights[x, y] = IslandHeightCalculate(x, y, heights[x, y]);
                }
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    float TieredHeightCalculate(int x, int y, float height)
    {
        float stepProportion = height / depth * Tiers;

        float theta = stepProportion * 2 * Mathf.PI;
        float newHeight = theta - Mathf.Sin(theta);

        return newHeight / 2 / Mathf.PI / Tiers * depth;
    }

    float IslandHeightCalculate(int x, int y, float height)
    {
        float halfSize = width / 2;
        float cx = x - halfSize;
        float cy = y - halfSize;
        float r = Mathf.Sqrt(cx * cx + cy * cy) / halfSize;

        float p = (1 - r) * 4;

        if(p < 0)
        {
            return 0;
        }
        else if (p >= 1)
        {
            return height;
        }
        else
        {
            return p * height;
        }
    }
}
