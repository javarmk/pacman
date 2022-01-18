using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorFood : MonoBehaviour
{
    public Food food;
    public BigFood bigFood;
    [SerializeField] int width, height;

    private void Awake()
    {
        width = MapMatric.mapInfo[0].Length;
        height = MapMatric.mapInfo.Length;
    }
    private void Start()
    {
        Generator();
    }

    private void Generator()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width / 2 + 1; x++)
            {

                switch (MapMatric.mapInfo[height - 1 - y][x])
                {
                    case '*':
                        if (x < width - 1 - x)
                        {
                            var wall1 = Instantiate(food, new Vector3(x, y, 0), Quaternion.identity);
                            var wall2 = Instantiate(food, new Vector3(width - 1 - x, y, 0), Quaternion.identity);
                        }
                        else
                        {
                            var wall1 = Instantiate(food, new Vector3(x, y, 0), Quaternion.identity);
                        }
                        break;
                    case '@':
                        if (x < width - 1 - x)
                        {
                            var wall1 = Instantiate(bigFood, new Vector3(x, y, 0), Quaternion.identity);
                            var wall2 = Instantiate(bigFood, new Vector3(width - 1 - x, y, 0), Quaternion.identity);
                        }
                        else
                        {
                            var wall1 = Instantiate(bigFood, new Vector3(x, y, 0), Quaternion.identity);
                        }
                        break;
                    default: break;
                }
            }
        }
    }
}
