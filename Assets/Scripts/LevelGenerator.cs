using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int i;
    public static int Row = 15;
    public static int Column = 14;
    public static int x = -14;
    public static int y = 15;
    public static int[,] levelMap = {
                        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
                        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
                        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
                        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
                        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
                        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
                        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
                        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
                        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
                        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
                        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
                        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
                        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
                        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
                        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
                       };
    [SerializeField]
    private GameObject[] MapGameObjects = default;
    private GameObject[] upperLeft = new GameObject[211];
    private GameObject[] upperRight = new GameObject[211];
    private GameObject[] lowerLeft = new GameObject[197];
    private GameObject[] lowerRight = new GameObject[197];

    private GameObject map = default;
    private GameObject dot = default;
    private static int[] RotationLine = {
        15,28,29,42,43,45,48,50,54,56,57,71,85,99,106,107,113,120,121,126,134,140,146,148,160,162,163,174,176,177,193,207};
    private static int[] RotationA = { 1, 31, 36, 87, 92, 95, 149, 179 };//dont move in UpperLeft
    private static int[] RotationB = { 34, 40, 90, 93, 112, 132, 138 };//nagetive90 in UL
    private static int[] RotationC = { 59, 64, 70, 101, 109, 127, 135, 154, 190 };//positie90 in UL
    private static int[] RotationD = { 62, 68, 104, 152, 188, 191 };//180 in UL
    private static int Tjuc = 14;
    private GameObject[] powerPellets = new GameObject[4];
    private GameObject[] heart = new GameObject[3];


    // Start is called before the first frame update
    void Awake()
    {
        map = GameObject.FindWithTag("map");
        dot = GameObject.FindWithTag("Dot");
        UpperLeft();
        UpperRight();
        LowerLeft();
        LowerRight();
        PowerPellets();
        Heart();

    }

    // Update is called once per frame
    private void UpperLeft()
    {
        i = 1;
        x = -14;
        y = 15;

        for (int row = 0; row < Row; row++)
        {
            for (int column = 0; column < Column; column++)
            {
                upperLeft[i] = Instantiate(MapGameObjects[levelMap[row, column]], new Vector2(x + column, y - row), Quaternion.identity);
                if(levelMap[row, column] == 5)
                {
                    upperLeft[i].transform.parent = dot.transform;
                    upperLeft[i].AddComponent<BoxCollider2D>();
                    upperLeft[i].GetComponent<BoxCollider2D>().offset = new Vector2(0,0);
                    upperLeft[i].GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.5f);
                    upperLeft[i].GetComponent<BoxCollider2D>().isTrigger = true;
                }
                else
                {
                    upperLeft[i].transform.parent = map.transform;
                }
                upperLeft[i].name = "MapUpperLeft" + i;
                i++;
            }
        }
        Destroy(upperLeft[44]);
        foreach (int i in RotationLine)
        {
            upperLeft[i].transform.rotation = Quaternion.Euler(0f, 0f, 90.0f);
        }
        foreach (int i in RotationB)
        {
            upperLeft[i].transform.rotation = Quaternion.Euler(0f, 0f, -90.0f);
        }
        foreach (int i in RotationC)
        {
            upperLeft[i].transform.rotation = Quaternion.Euler(0f, 0f, 90.0f);
        }
        foreach (int i in RotationD)
        {
            upperLeft[i].transform.rotation = Quaternion.Euler(0f, 0f, 180.0f);
        }
    }
    private void UpperRight()
    {
        i = 1;
        x = 13;
        y = 15;
        for (int row = 0; row < Row; row++)
        {
            for (int column = 0; column < Column; column++)
            {
                upperRight[i] = Instantiate(MapGameObjects[levelMap[row, column]], new Vector2(x - column, y - row), Quaternion.identity);
                //upperRight[i].transform.parent = map.transform;
                if (levelMap[row, column] == 5)
                {
                    upperRight[i].transform.parent = dot.transform;
                    upperRight[i].AddComponent<BoxCollider2D>();
                    upperRight[i].GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                    upperRight[i].GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.5f);
                    upperRight[i].GetComponent<BoxCollider2D>().isTrigger = true;
                }
                else
                {
                    upperRight[i].transform.parent = map.transform;
                }
                upperRight[i].name = "MapUpperRight" + i;
                //upperRight[i].GetComponent<SpriteRenderer>().flipX = true;
                if (i <= 210) i++;
            }
        }
        Destroy(upperRight[44]);
        foreach (int i in RotationLine)
        {
            upperRight[i].transform.Rotate(0f, 0f, -90.0f);
        }

        foreach (int i in RotationA)
        {
            upperRight[i].transform.Rotate(0f, 0f, -90.0f);
        }
        foreach (int i in RotationC)
        {
            upperRight[i].transform.Rotate(0f, 0f, 180.0f);
        }
        foreach (int i in RotationD)
        {
            upperRight[i].transform.Rotate(0f, 0f, 90f);
        }
        upperRight[Tjuc].GetComponent<SpriteRenderer>().flipX = true;

    }
    private void LowerLeft()
    {
        i = 1;
        x = -14;
        y = -13;
        for (int row = 0; row < Row - 1; row++)
        {
            for (int column = 0; column < Column; column++)
            {
                lowerLeft[i] = Instantiate(MapGameObjects[levelMap[row, column]], new Vector2(x + column, y + row), Quaternion.identity);
                //lowerLeft[i].transform.parent = map.transform;
                if (levelMap[row, column] == 5)
                {
                    lowerLeft[i].transform.parent = dot.transform;
                    lowerLeft[i].AddComponent<BoxCollider2D>();
                    lowerLeft[i].GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                    lowerLeft[i].GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.5f);
                    lowerLeft[i].GetComponent<BoxCollider2D>().isTrigger = true;
                }
                else
                {
                    lowerLeft[i].transform.parent = map.transform;
                }
                lowerLeft[i].name = "MapLowerLeft" + i;
                if (i < lowerLeft.Length) i++;
            }
        }
        Destroy(lowerLeft[44]);
        lowerLeft[Tjuc].GetComponent<SpriteRenderer>().flipY = true;
        Destroy(lowerLeft[182]);
        lowerLeft[182] = Instantiate(MapGameObjects[4], new Vector3(-1, -1), Quaternion.identity);
        lowerLeft[182].transform.parent = map.transform;
        lowerLeft[182].name = "MapLowerLeft182";
        foreach (int i in RotationLine)
        {
            if (i <= 196) lowerLeft[i].transform.Rotate(0f, 0f, -90.0f);
        }

        foreach (int i in RotationA)
        {
            lowerLeft[i].transform.Rotate(0f, 0f, 90.0f);
        }

        foreach (int i in RotationB)
        {
            lowerLeft[i].transform.Rotate(0f, 0f, 180.0f);
        }

        foreach (int i in RotationD)
        {
            lowerLeft[i].transform.Rotate(0f, 0f, -90f);
        }
    }
    private void LowerRight()
    {
        i = 1;
        x = 13;
        y = -13;
        for (int row = 0; row < Row - 1; row++)
        {
            for (int column = 0; column < Column; column++)
            {
                lowerRight[i] = Instantiate(MapGameObjects[levelMap[row, column]], new Vector2(x - column, y + row), Quaternion.identity);
                //lowerRight[i].transform.parent = map.transform;
                if (levelMap[row, column] == 5)
                {
                    lowerRight[i].transform.parent = dot.transform;
                    lowerRight[i].AddComponent<BoxCollider2D>();
                    lowerRight[i].GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                    lowerRight[i].GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.5f);
                    lowerRight[i].GetComponent<BoxCollider2D>().isTrigger = true;
                }
                else
                {
                    lowerRight[i].transform.parent = map.transform;
                }
                lowerRight[i].name = "MapLowerRight" + i;
                if (i <= 210) i++;
            }
        }
        Destroy(lowerRight[44]);
        lowerRight[Tjuc].GetComponent<SpriteRenderer>().flipX = true;
        lowerRight[Tjuc].GetComponent<SpriteRenderer>().flipY = true;
        Destroy(lowerRight[182]);
        lowerRight[182] = Instantiate(MapGameObjects[4], new Vector3(0, -1), Quaternion.identity);
        lowerRight[182].transform.parent = map.transform;
        lowerRight[182].name = "MapLowerRight182";
        foreach (int i in RotationLine)
        {
            if (i <= 196) lowerRight[i].transform.Rotate(0f, 0f, -90.0f);
        }

        foreach (int i in RotationA)
        {
            lowerRight[i].transform.Rotate(0f, 0f, 180.0f);
        }

        foreach (int i in RotationC)
        {
            lowerRight[i].transform.Rotate(0f, 0f, -90.0f);
        }
        foreach (int i in RotationB)
        {
            lowerRight[i].transform.Rotate(0f, 0f, 90f);
        }
    }
    private void PowerPellets()
    {
        powerPellets[0] = GameObject.FindWithTag("PowerPellet1");
        powerPellets[1] = Instantiate(powerPellets[0], new Vector2(12, 12), Quaternion.identity);
        powerPellets[2] = Instantiate(powerPellets[0], new Vector2(-13, -10), Quaternion.identity);
        powerPellets[3] = Instantiate(powerPellets[0], new Vector2(12, -10), Quaternion.identity);
        for(i = 1; i < 4; i++)
        {
            int a = i + 1;
            powerPellets[i].name = "PowerPellets" + a;
        }
    }

    private void Heart()
    {
        heart[0] = GameObject.FindWithTag("Heart");
        heart[1] = Instantiate(heart[0], new Vector2(-13, -14), Quaternion.identity);
        heart[2] = Instantiate(heart[0], new Vector2(-12, -14), Quaternion.identity);
        heart[1].name = "Heart2";
        heart[2].name = "Heart3";
    }

}

