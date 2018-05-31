using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphRender : MonoBehaviour
{
    public Texture2D ChartImage;
    public int Reps = 50, Sets = 10;
    public float yScale = 25;
    public float[,] ProgressData;
    public int ChartWidth = 800;
    public int ChartHeight = 400;
    public CanvasManip CM;
    public target_random TR;


    Color c_back = new Color(0.9f, 0.9f, 0.9f, 0.7f);
    Color c_sides = new Color(0.15f, 0.15f, 0.15f, 1);
    Color c_grid = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    Color c_line = new Color(0.2f, 0.7f, 0.5f, 1);

    // Use this for initialization
    void Start()
    {
        ProgressData = new float[Sets, 999];
        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        ChartImage = new Texture2D(ChartWidth, ChartHeight, TextureFormat.ARGB32, false);
        GreateData();
    }

    void Update()
    {
    }

    public void GreateData()
    {
        for (int j = 0; j < ProgressData.GetLength(0); j++)
        {
            ProgressData[j, 0] = 10;
            for (int i = 1; i < ProgressData.GetLength(1); i++) //ProgressData.GetLength(0)
            {
                ProgressData[j, i] = ProgressData[j, i - 1] + Random.Range(0, 2) * 10 + i % 1; //Korvaa Random scorella ja viimeinen ykkönen käden puolella 
            }
        }

        ChartGrid();
        if (GameObject.Find("ShowAll").GetComponent<Toggle>().isOn)
        {
            for (int i = 0; i < ProgressData.GetLength(0); i++)
            {
                LineDrawer(TR.shotCount, i, GameObject.Find("Fill").GetComponent<Toggle>().isOn);
            }
        }

        else { LineDrawer(TR.shotCount, 0, GameObject.Find("Fill").GetComponent<Toggle>().isOn); }
        //Refresh();
    }

    public void ChangeScale()
    {
        ChartGrid();
        if (GameObject.Find("ShowAll").GetComponent<Toggle>().isOn)
        {
            for (int i = 0; i < ProgressData.GetLength(0); i++)
            {
                LineDrawer(TR.shotCount, i, GameObject.Find("Fill").GetComponent<Toggle>().isOn);
            }
        }

        else { LineDrawer(TR.shotCount, 0, GameObject.Find("Fill").GetComponent<Toggle>().isOn); }
    }

    void ChartGrid()
    {
        // set the pixel values
        for (int y = 0; y < ChartHeight; y++)
        {
            for (int x = 0; x < ChartWidth; x++)
            {
                if (x <= 5 && x > 2)
                {
                    ChartImage.SetPixel(x, y, c_sides);
                }

                else if (y > ChartHeight - 10 && y < ChartHeight - 5)
                {
                    ChartImage.SetPixel(x, y, c_sides);
                }

                else if ((ChartHeight - 10 - y) % (int)(ChartHeight / yScale * 10) == 0 && x > 6)
                {
                    ChartImage.SetPixel(x, y, c_grid);
                }

                else { ChartImage.SetPixel(x, y, c_back); }

            }
        }
    }

    void LineDrawer(int ValueSize, int lineNumb, bool fill)
    {
        c_line = new Color((float)((9 / 1 + lineNumb % 3) % 10) / 10, (float)((4f + lineNumb - (lineNumb % 3)) % 10) / 10, (float)((8f + lineNumb) % 10) / 10);
        float yAdjust = (ChartHeight / yScale);
        // set the pixel values
        for (int y = 0; y < ChartHeight; y++)
        {
            for (int x = 6; x < ChartWidth; x++)
            {
                if (ValueSize == 0)
                {
                }
                else
                {
                    int xCoord = (x / (ChartWidth / ValueSize));
                    float xPoint = ProgressData[lineNumb, xCoord];
                    float yPoint = (ProgressData[lineNumb, xCoord + 1]) - xPoint; // delete (+1) from array if you want to disably merge.
                    float yMultiply = yPoint / (ChartWidth / ValueSize);
                    float measure = ChartHeight - 10 - (xPoint + (x - xCoord * (ChartWidth / ValueSize)) * yMultiply) * yAdjust / 10;

                    if (y + 1 >= measure && y <= ChartHeight - 10)
                    {
                        if (!fill && y - 1 <= measure)
                        {
                            ChartImage.SetPixel(x, y, c_line);
                        }

                        else if (fill)
                        {
                            ChartImage.SetPixel(x, y, c_line);
                        }

                    }

                    else { }
                }
            }
        }
    }

    public void Refresh()
    {
        // Apply all SetPixel calls
        ChartImage.Apply();
        // connect texture to material of GameObject this script is attached to
        //GetComponent<RawImage>().material.mainTexture = ChartImage;
        GetComponent<Renderer>().material.mainTexture = ChartImage;
    }

    public void yScaleChange()
    {
        yScale = (CM.RepsSlider.value * CM.SetsSlider.value * 2f);
    }
}
