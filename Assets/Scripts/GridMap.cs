using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{


    [SerializeField] int width, height;

    [SerializeField] private Tile tile;
    private void Awake()
    {
        width = MapMatric.mapInfo[0].Length;
        height = MapMatric.mapInfo.Length;

        SetMirror();


    }
    private void Start()
    {
        GeneratorGrid();



    }

    private void SetMirror()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 15; x < width; x++)
            {
                MapMatric.mapInfo[height - 1 - y][x] = MapMatric.mapInfo[height - 1 - y][28 - x];
            }
        }
    }



    private void GeneratorGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width / 2 + 1; x++)
            {

                switch (MapMatric.mapInfo[height - 1 - y][x])
                {
                    case '#':
                        if (x < width - 1 - x)
                        {
                            var wall1 = Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
                            var wall2 = Instantiate(tile, new Vector3(width - 1 - x, y, 0), Quaternion.identity);
                        }
                        else
                        {
                            var wall1 = Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
                        }

                        break;
                    default: break;
                }
            }
        }
    }

}
