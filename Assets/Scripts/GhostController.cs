using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private GameObject[] Ghost = new GameObject[4];
    private GameObject Pacman = default;
    private Vector2[] nextPosition = new Vector2[4];
    private Vector2 dir = Vector2.zero;
    private Tweeners tweener;
    private float duration = 0.48f;
    private int[] direction = new int[4];
    private int[] directionNew = new int[4];
    //Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            Ghost[i] = GameObject.FindGameObjectWithTag("Ghost" + (i + 1));
        }

        Pacman = GameObject.FindGameObjectWithTag("Pacman");
    }
    void Start()
    {
        tweener = GetComponent<Tweeners>();
        Ghost[0].transform.position = new Vector2(-3, 1);
        Ghost[1].transform.position = new Vector2(-2, 1);
        Ghost[2].transform.position = new Vector2(1, 1);
        Ghost[3].transform.position = new Vector2(2, 1);
        for (int i = 0; i < 4; i++)
        {
            direction[i] = -1;
            directionNew[i] = -1;
            nextPosition[i] = (Vector2)Ghost[i].transform.position;
        }
        dir = Vector2.zero;
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        if (!GameObject.FindWithTag("Audio_Intro").GetComponent<AudioSource>().isPlaying)
        {
            if ((Vector2)Ghost[0].transform.position == nextPosition[0])
            {
                if (Pacman.GetComponent<PacStudentController>().ghostDeadFlag[0])
                {
                    nextPosition[0] = new Vector2(-3, 1);
                    tweener.AddTween(Ghost[0].transform, Ghost[0].transform.position, nextPosition[0], 0.1f);
                    Pacman.GetComponent<PacStudentController>().GhostDeadTotalTime[0] = 0;
                }
                else
                {
                    GhostOneMoveout();
                }
            }
            //if ((Vector2)Ghost[1].transform.position == nextPosition[1])
            //{
            //    GhostTwoMoveout();
            //}
            //if ((Vector2)Ghost[2].transform.position == nextPosition[2])
            //{
            //    GhostThreeMoveout();
            //}

        }
    }
    private void GhostOneMoveout()
    {
        
            if ((Vector2)Ghost[0].transform.position == new Vector2(-3, 1) || (Vector2)Ghost[0].transform.position == new Vector2(-2, 1))
            {
                nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.right;
            }
            else if ((Vector2)Ghost[0].transform.position == new Vector2(-1, 1) || (Vector2)Ghost[0].transform.position == new Vector2(-1, 2) ||
                (Vector2)Ghost[0].transform.position == new Vector2(-1, 3))
            {
                nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.up;
            }
            else
            {
                GhostOneMove();
            }
            tweener.AddTween(Ghost[0].transform, Ghost[0].transform.position, nextPosition[0], duration);
        
        
        if (!Pacman.GetComponent<PacStudentController>().GhostScaredTimerFlag&&!Pacman.GetComponent<PacStudentController>().ghostDeadFlag[0])
        {
            Ghost[0].GetComponent<Animator>().SetFloat("DirX", 0);
            Ghost[0].GetComponent<Animator>().SetFloat("DirY", 0);
            Vector2 dir = nextPosition[0] - (Vector2)Ghost[0].transform.position;
            Ghost[0].GetComponent<Animator>().SetFloat("DirX", dir.x);
            Ghost[0].GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }
    private void GhostOneMove()
    {
        if (direction[0] == -1)
        {
            
            do
            {
                directionNew[0] = Random.Range(0, 4);
                if (directionNew[0] == 0)
                {
                    dir = Vector2.up;
                }
                if (directionNew[0] == 1)
                {
                    dir = Vector2.down;
                }
                if (directionNew[0] == 2)
                {
                    dir = Vector2.left;
                }
                if (directionNew[0] == 3)
                {
                    dir = Vector2.right;
                }
            } while (Valid(dir, 0));
            direction[0] = directionNew[0];

        }
        else
        {
            if (Pacman.GetComponent<PacStudentController>().currentInput == null)
            {
                if (direction[0] == 0)
                {
                    if (Valid(Vector2.up, 0))
                    {
                        nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.up;
                        directionNew[0] = direction[0];
                    }
                    else
                    {
                        do
                        {
                            directionNew[0] = Random.Range(0, 4);
                            if (directionNew[0] == 0)
                            {
                                dir = Vector2.up;
                            }
                            if (directionNew[0] == 1)
                            {
                                dir = Vector2.down;
                            }
                            if (directionNew[0] == 2)
                            {
                                dir = Vector2.left;
                            }
                            if (directionNew[0] == 3)
                            {
                                dir = Vector2.right;
                            }
                        } while (directionNew[0] == 1 || !Valid(dir, 0));
                        direction[0] = directionNew[0];
                    }
                }
                if (direction[0] == 1)
                {
                    if (Valid(Vector2.down, 0))
                    {
                        nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.down;
                        directionNew[0] = direction[0];
                    }
                    else
                    {
                        do
                        {
                            directionNew[0] = Random.Range(0, 4);
                            if (directionNew[0] == 0)
                            {
                                dir = Vector2.up;
                            }
                            if (directionNew[0] == 1)
                            {
                                dir = Vector2.down;
                            }
                            if (directionNew[0] == 2)
                            {
                                dir = Vector2.left;
                            }
                            if (directionNew[0] == 3)
                            {
                                dir = Vector2.right;
                            }
                        } while (directionNew[0] == 0 || !Valid(dir, 0));
                        direction[0] = directionNew[0];
                    }
                }
                if (direction[0] == 2)
                {
                    if (Valid(Vector2.left, 0))
                    {
                        nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.left;
                        directionNew[0] = direction[0];
                    }
                    else
                    {
                        do
                        {
                            directionNew[0] = Random.Range(0, 4);
                            if (directionNew[0] == 0)
                            {
                                dir = Vector2.up;
                            }
                            if (directionNew[0] == 1)
                            {
                                dir = Vector2.down;
                            }
                            if (directionNew[0] == 2)
                            {
                                dir = Vector2.left;
                            }
                            if (directionNew[0] == 3)
                            {
                                dir = Vector2.right;
                            }
                        } while (directionNew[0] == 3 || !Valid(dir, 0));
                        direction[0] = directionNew[0];
                    }
                }
                if (direction[0] == 3)
                {
                    if (Valid(Vector2.right, 0))
                    {
                        nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.right;
                        directionNew[0] = direction[0];
                    }
                    else
                    {
                        do
                        {
                            directionNew[0] = Random.Range(0, 4);
                            if (directionNew[0] == 0)
                            {
                                dir = Vector2.up;
                            }
                            if (directionNew[0] == 1)
                            {
                                dir = Vector2.down;
                            }
                            if (directionNew[0] == 2)
                            {
                                dir = Vector2.left;
                            }
                            if (directionNew[0] == 3)
                            {
                                dir = Vector2.right;
                            }
                        } while (directionNew[0] == 2 || !Valid(dir, 0));
                        direction[0] = directionNew[0];
                    }
                }
            }
            else
            {
                
                if (Pacman.GetComponent<PacStudentController>().currentInput == "W")
                {
                    if (Valid(Vector2.up, 0) && direction[0] != 1)
                    {
                        nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.up;
                        directionNew[0] = direction[0]=0;
                    }
                    else
                    {
                        GhostOneCheckPoint();
                    }
                }
                if (Pacman.GetComponent<PacStudentController>().currentInput == "S" )
                {
                    if (Valid(Vector2.down, 0) && direction[0] != 0)
                    {
                        nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.down;
                        directionNew[0] = direction[0]=1;
                    }
                    else
                    {
                        GhostOneCheckPoint();
                    }
                }
                if (Pacman.GetComponent<PacStudentController>().currentInput == "A" )
                {
                    if (Valid(Vector2.left, 0) && direction[0] != 3)
                    {
                        nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.left;
                        directionNew[0] = direction[0]=2;
                    }
                    else
                    {
                        GhostOneCheckPoint();
                    }
                }
                if (Pacman.GetComponent<PacStudentController>().currentInput == "D" )
                {
                    if (Valid(Vector2.right, 0) && direction[0] != 2)
                    {
                        nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.right;
                        directionNew[0] = direction[0]=3;
                    }
                    else
                    {
                        GhostOneCheckPoint();
                    }
                }
            }

        }
    }
    private void GhostOneCheckPoint()
    {
        if (direction[0] == 0)
        {
            if (Valid(Vector2.up, 0))
            {
                nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.up;
                directionNew[0] = direction[0];
            }
            else
            {
                do
                {
                    directionNew[0] = Random.Range(0, 4);
                    if (directionNew[0] == 0)
                    {
                        dir = Vector2.up;
                    }
                    if (directionNew[0] == 1)
                    {
                        dir = Vector2.down;
                    }
                    if (directionNew[0] == 2)
                    {
                        dir = Vector2.left;
                    }
                    if (directionNew[0] == 3)
                    {
                        dir = Vector2.right;
                    }
                } while (directionNew[0] == 1 || !Valid(dir, 0));
                direction[0] = directionNew[0];
            }
        }
        if (direction[0] == 1)
        {
            if (Valid(Vector2.down, 0))
            {
                nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.down;
                directionNew[0] = direction[0];
            }
            else
            {
                do
                {
                    directionNew[0] = Random.Range(0, 4);
                    if (directionNew[0] == 0)
                    {
                        dir = Vector2.up;
                    }
                    if (directionNew[0] == 1)
                    {
                        dir = Vector2.down;
                    }
                    if (directionNew[0] == 2)
                    {
                        dir = Vector2.left;
                    }
                    if (directionNew[0] == 3)
                    {
                        dir = Vector2.right;
                    }
                } while (directionNew[0] == 0 || !Valid(dir, 0));
                direction[0] = directionNew[0];
            }
        }
        if (direction[0] == 2)
        {
            if (Valid(Vector2.left, 0))
            {
                nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.left;
                directionNew[0] = direction[0];
            }
            else
            {
                do
                {
                    directionNew[0] = Random.Range(0, 4);
                    if (directionNew[0] == 0)
                    {
                        dir = Vector2.up;
                    }
                    if (directionNew[0] == 1)
                    {
                        dir = Vector2.down;
                    }
                    if (directionNew[0] == 2)
                    {
                        dir = Vector2.left;
                    }
                    if (directionNew[0] == 3)
                    {
                        dir = Vector2.right;
                    }
                } while (directionNew[0] == 3 || !Valid(dir, 0));
                direction[0] = directionNew[0];
            }
        }
        if (direction[0] == 3)
        {
            if (Valid(Vector2.right, 0))
            {
                nextPosition[0] = (Vector2)Ghost[0].transform.position + Vector2.right;
                directionNew[0] = direction[0];
            }
            else
            {
                do
                {
                    directionNew[0] = Random.Range(0, 4);
                    if (directionNew[0] == 0)
                    {
                        dir = Vector2.up;
                    }
                    if (directionNew[0] == 1)
                    {
                        dir = Vector2.down;
                    }
                    if (directionNew[0] == 2)
                    {
                        dir = Vector2.left;
                    }
                    if (directionNew[0] == 3)
                    {
                        dir = Vector2.right;
                    }
                } while (directionNew[0] == 2 || !Valid(dir, 0));
                direction[0] = directionNew[0];
            }
        }

    }
    private void GhostTwoMoveout()
    {
        if ((Vector2)Ghost[1].transform.position == new Vector2(-3, 1) || (Vector2)Ghost[1].transform.position == new Vector2(-2, 1))
        {
            nextPosition[1] = (Vector2)Ghost[1].transform.position + Vector2.right;
        }
        else if ((Vector2)Ghost[1].transform.position == new Vector2(-1, 1) || (Vector2)Ghost[1].transform.position == new Vector2(-1, 2) ||
            (Vector2)Ghost[1].transform.position == new Vector2(-1, 3))
        {
            nextPosition[1] = (Vector2)Ghost[1].transform.position + Vector2.up;
        }
        else
        {
            GhostTwoMove();
        }
        tweener.AddTween(Ghost[1].transform, Ghost[1].transform.position, nextPosition[1], duration);
        if (!Pacman.GetComponent<PacStudentController>().GhostScaredTimerFlag && !Pacman.GetComponent<PacStudentController>().ghostDeadFlag[1])
        {
            Ghost[1].GetComponent<Animator>().SetFloat("DirX", 0);
            Ghost[1].GetComponent<Animator>().SetFloat("DirY", 0);
            Vector2 dir = nextPosition[1] - (Vector2)Ghost[1].transform.position;
            Ghost[1].GetComponent<Animator>().SetFloat("DirX", dir.x);
            Ghost[1].GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }
    private void GhostTwoMove()
    {
        
    }
    private void GhostTwoCheckPoint()
    {
        
    }



    private void GhostThreeMoveout()
    {
        if ((Vector2)Ghost[2].transform.position == new Vector2(1, 1))
        {
            nextPosition[2] = (Vector2)Ghost[2].transform.position + Vector2.left;
        }
        else if ((Vector2)Ghost[2].transform.position == new Vector2(0, 1) || (Vector2)Ghost[2].transform.position == new Vector2(0, 2) ||
            (Vector2)Ghost[2].transform.position == new Vector2(0, 3))
        {
            nextPosition[2] = (Vector2)Ghost[2].transform.position + Vector2.up;
        }
        else
        {
            GhostThreeMove();

        }
        tweener.AddTween(Ghost[2].transform, Ghost[2].transform.position, nextPosition[2], duration);
        if (!Pacman.GetComponent<PacStudentController>().GhostScaredTimerFlag && !Pacman.GetComponent<PacStudentController>().ghostDeadFlag[2])
        {
            Ghost[2].GetComponent<Animator>().SetFloat("DirX", 0);
            Ghost[2].GetComponent<Animator>().SetFloat("DirY", 0);
            Vector2 dir = nextPosition[2] - (Vector2)Ghost[2].transform.position;
            Ghost[2].GetComponent<Animator>().SetFloat("DirX", dir.x);
            Ghost[2].GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }
    private void GhostThreeMove()
    {
        if (direction[2] == -1)
        {
            directionNew[2] = Random.Range(0, 4);
            direction[2] = directionNew[2];
        }
        else
        {
            if (direction[2] == 0)
            {
                if (Valid(Vector2.up, 2))
                {
                    nextPosition[2] = (Vector2)Ghost[2].transform.position + Vector2.up;
                    directionNew[2] = direction[2];
                }
                else
                {
                    do
                    {
                        directionNew[2] = Random.Range(0, 4);
                        if (directionNew[2] == 0)
                        {
                            dir = Vector2.up;
                        }
                        if (directionNew[2] == 1)
                        {
                            dir = Vector2.down;
                        }
                        if (directionNew[2] == 2)
                        {
                            dir = Vector2.left;
                        }
                        if (directionNew[2] == 3)
                        {
                            dir = Vector2.right;
                        }
                    } while (directionNew[2] == 1 || !Valid(dir, 2));
                    direction[2] = directionNew[2];
                }
            }
            if (direction[2] == 1)
            {
                if (Valid(Vector2.down, 2))
                {
                    nextPosition[2] = (Vector2)Ghost[2].transform.position + Vector2.down;
                    directionNew[2] = direction[2];
                }
                else
                {
                    do
                    {
                        directionNew[2] = Random.Range(0, 4);
                        if (directionNew[2] == 0)
                        {
                            dir = Vector2.up;
                        }
                        if (directionNew[2] == 1)
                        {
                            dir = Vector2.down;
                        }
                        if (directionNew[2] == 2)
                        {
                            dir = Vector2.left;
                        }
                        if (directionNew[2] == 3)
                        {
                            dir = Vector2.right;
                        }
                    } while (directionNew[2] == 0 || !Valid(dir, 2));
                    direction[2] = directionNew[2];
                }
            }
            if (direction[2] == 2)
            {
                if (Valid(Vector2.left, 2))
                {
                    nextPosition[2] = (Vector2)Ghost[2].transform.position + Vector2.left;
                    directionNew[2] = direction[2];
                }
                else
                {
                    do
                    {
                        directionNew[2] = Random.Range(0, 4);
                        if (directionNew[2] == 0)
                        {
                            dir = Vector2.up;
                        }
                        if (directionNew[2] == 1)
                        {
                            dir = Vector2.down;
                        }
                        if (directionNew[2] == 2)
                        {
                            dir = Vector2.left;
                        }
                        if (directionNew[2] == 3)
                        {
                            dir = Vector2.right;
                        }
                    } while (directionNew[2] == 3 || !Valid(dir, 2));
                    direction[2] = directionNew[2];
                }
            }
            if (direction[2] == 3)
            {
                if (Valid(Vector2.right, 2))
                {
                    nextPosition[2] = (Vector2)Ghost[2].transform.position + Vector2.right;
                    directionNew[2] = direction[2];
                }
                else
                {
                    do
                    {
                        directionNew[2] = Random.Range(0, 4);
                        if (directionNew[2] == 0)
                        {
                            dir = Vector2.up;
                        }
                        if (directionNew[2] == 1)
                        {
                            dir = Vector2.down;
                        }
                        if (directionNew[2] == 2)
                        {
                            dir = Vector2.left;
                        }
                        if (directionNew[2] == 3)
                        {
                            dir = Vector2.right;
                        }
                    } while (directionNew[2] == 2 || !Valid(dir, 2));
                    direction[2] = directionNew[2];
                }
            }
        }
    }
    private bool Valid(Vector2 dir, int i)
    {
        Vector2 pos = Ghost[i].transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        //wallParticlePosition = pos + dir;
        //hitRecord = hit;
        if (hit.collider.gameObject.name == "Teleport")
        {
            return (false);
        }
        else
        {
            return (hit.collider.gameObject.name != "Map");
        }

    }
}
