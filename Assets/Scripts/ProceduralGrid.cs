using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ensures that there is alwasys a renderer and filter attacthed
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    //grid settings
    public float cellSize;
    public Vector3 gridOffset;
    public int gridSize;

    //lava settings
    public float perlinScale = 4.56f;
    public float waveSpeed = 1f;
    public float waveHeight = 2f;
    public float floatingMaxHeight;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    // Start is called before the first frame update
    void Start()
    {
        MakeGrid();
        UpdateMesh();
    }

    void Update()
    {
        AnimateMesh(); 
    }

    //make a grid of vertices and triangles
    void MakeGrid()
    {
        int ver = 0;
        int tri = 0;

        //array sizes
        vertices = new Vector3[gridSize * gridSize * 4];
        triangles = new int[gridSize * gridSize * 6];

        //vertex offset spaces them so the centre is the centre of quad and not the corner
        float vertexOffset = cellSize * .5f;

        for(int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Vector3 cellOffset = new Vector3(x * cellSize, 0, z * cellSize);

                //fill vetex/triangles arrays and add offset for whole grid + individual quads
                vertices[ver + 0] = new Vector3(-vertexOffset, 0, -vertexOffset) + gridOffset + cellOffset;
                vertices[ver + 1] = new Vector3(-vertexOffset, 0, vertexOffset) + gridOffset + cellOffset;
                vertices[ver + 2] = new Vector3(vertexOffset, 0, -vertexOffset) + gridOffset + cellOffset;
                vertices[ver + 3] = new Vector3(vertexOffset, 0, vertexOffset) + gridOffset + cellOffset;

                triangles[tri + 0] = ver + 0;
                triangles[tri + 1] = triangles[tri + 4] = ver + 1;
                triangles[tri + 2] = triangles[tri + 3] = ver + 2;
                triangles[tri + 5] = ver + 3;

                // + 4 each quad
                ver += 4;
                // + 6 every triangle
                tri += 6;
            }
        }
            
    }

    //aniamtes each vertice on y axis with perlin noise
    void AnimateMesh()
    {
        vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            float x = (vertices[i].x * perlinScale) + (Time.timeSinceLevelLoad * waveSpeed);
            float z = (vertices[i].z * perlinScale) + (Time.timeSinceLevelLoad * waveSpeed);

            vertices[i].y = (Mathf.PerlinNoise(x, z) - 0.5f) * waveHeight;
        }

       mesh.vertices = vertices;
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
