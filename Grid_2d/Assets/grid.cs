using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour {
    [SerializeField] private int X;
    [SerializeField] private int Y;
    [SerializeField] private GameObject cube;
    private GameObject[,] Grid;
    private bool[,] Gridbool;
    private int _alivecount;
    private int _count = 0;
    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        Grid = new GameObject[X, Y];
        Gridbool = new bool[X, Y];
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                Gridbool[i, j] = false;
                Grid[i, j] = Instantiate(cube, new Vector3(i, j, 0), transform.rotation);
                Grid[i, j].GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                changecoler(Grid[(int)hit.transform.position.x, (int)hit.transform.position.y]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            game();
        }
    }
    void changecoler(GameObject gameobject)
    {
        if (gameobject.GetComponent<alive>()._alive)
        {
            gameobject.GetComponent<alive>()._alive = false;
        }
        else if (!gameobject.GetComponent<alive>()._alive)
        {
            gameobject.GetComponent<alive>()._alive = true;
        }
    }
    void game()
    {
        for (int i = 1; i < Y - 1; i++)
        {
            for (int j = 1; j < X - 1; j++)
            {
                for (int n = -1; n < 2; n++)
                {
                    for (int m = -1; m < 2; m++)
                    {
                        if (Grid[(int)i + n, (int)j + m].GetComponent<alive>()._alive && (n != 0 || m != 0))//kijken hoeveel er leven
                        {
                            _alivecount++;
                        }
                        _count++;
                        if (_count == 9)//check voor verandering
                        {
                            if ((Grid[i, j].GetComponent<alive>()._alive && _alivecount == 2) || _alivecount == 3)
                            {
                                Gridbool[i, j] = true;
                            }
                            else
                            {
                                Gridbool[i, j] = false;
                            }
                            _count = 0;
                            _alivecount = 0;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < Y; i++)
        {
            for (int j = 0; j < X; j++)
            {
                Grid[i, j].GetComponent<alive>()._alive = Gridbool[i, j];
            }
        }
    }
}

