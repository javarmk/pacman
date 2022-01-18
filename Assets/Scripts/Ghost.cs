using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float speed = 0.05f;
    Vector3 direction;
    bool isMoving = false;
    int step = 0;
    public GhostMode ghostMode;
    public Pacman pacman;
    public TargetMode targetMode;
    // Start is called before the first frame update
    public Vector2 target;
    Ghost[] ghosts;

    List<Vector3> stepTotarget = new List<Vector3>();


    List<Vector3> allDirection = new List<Vector3>();

    public int litmitStep = 20;

    private void Awake()
    {
        allDirection.Add(new Vector3(1, 0, 0));
        allDirection.Add(new Vector3(-1, 0, 0));
        allDirection.Add(new Vector3(0, 1, 0));
        allDirection.Add(new Vector3(0, -1, 0));

        litmitStep = 20;
    }

    void Start()
    {
        pacman = GameObject.FindObjectOfType<Pacman>();
        ghosts = GameObject.FindObjectsOfType<Ghost>();
    }


    Vector3 lastPosision;
    int count = 0;
    // Update is called once per frame

    float count1212 = 0;
    void Update()
    {
        if (sick == true)
        {
            count1212 += Time.deltaTime;
        }
        if (this.transform.position.x % 1.0 == 0 && this.transform.position.y % 1.0 == 0)
        {
            if (sick)
            {
                this.speed = 0.02f;
                this.litmitStep = 50;
            }
            else
            {
                if (count1212 > 3)
                {
                    this.speed = 0.05f;
                    this.litmitStep = 20;
                    sick = false;
                    count1212 = 0;
                }
            }
        }



        if (pacman == null) return;
        if (this.transform.position.x <= 19 && this.transform.position.x >= 9 && this.transform.position.y <= 20 && this.transform.position.y >= 16)
        {
            this.target.x = (int)Random.Range(13, 17);
            this.target.y = 22;
        }



        count++;
        if (count % 20 > 10)
        {
            lastPosision = this.transform.position;
        }

        if (Vector3.Magnitude(transform.position - new Vector3(target.x, target.y, 0)) < 1f)
        {
            if (Vector3.Magnitude(this.transform.position - pacman.transform.position) < 10)
            {
                target.x = pacman.transform.position.x;
                target.y = pacman.transform.position.y;
            }
            else
            {
                int i = Random.Range(0, 2);
                if (i <= 1)
                {
                    target = MapMatric.FindBigPoint();
                }
                else
                {
                    var direc = pacman.direction;

                    direc.Normalize();
                    int iiii = UnityEngine.Random.Range(3, 5);
                    direc *= iiii;
                    if (direc.x <= 1)
                    {
                        direc.x = 1;
                    }
                    if (direc.x >= 27)
                    {
                        direc.x = 27;
                    }
                    if (direc.y <= 1)
                    {
                        direc.y = 1;
                    }
                    if (direc.y >= 32)
                    {
                        direc.x = 32;
                    }
                    this.target = direc;
                }
            }
        }
        else
        {
            if (Vector3.Magnitude(this.transform.position - pacman.transform.position) < 5)
            {
                target.x = pacman.transform.position.x;
                target.y = pacman.transform.position.y;
            }
        }

        SetTarget();
        HandleMove();
        EatPacman();

    }


    private void HandleMove()
    {
        if (isMoving)
        {
            if (step >= litmitStep)
            {
                isMoving = false;
                step = 0;
            }
            else
            {
                var newPosision = new Vector3(transform.position.x + (direction.x * speed), transform.position.y + (direction.y * speed), 0);
                if (step == litmitStep - 1)
                {
                    newPosision.x = Mathf.RoundToInt(newPosision.x);
                    newPosision.y = Mathf.RoundToInt(newPosision.y);
                }
                transform.position = newPosision;
                step++;
            }
        }
        else
        {
            SetDirection();
            if (direction.x == 0 && direction.y == 0)
            {

            }
            else
            {

                if (direction.y == 1)
                {
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else
                {
                    if (direction.y == -1)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 270);
                    }
                    else
                    {
                        if (direction.x == 1)
                        {
                            transform.eulerAngles = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, 0, 180);
                        }
                    }
                }

                if (!CheckWall(transform.position + direction))
                {
                    isMoving = true;
                }
            }
        }
    }



    private void SetDirection()
    {
        if (stepTotarget.Count > 0)
        {
            direction = stepTotarget[0];
            stepTotarget.RemoveAt(0);
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    public bool CheckWall(Vector3 newPosision)
    {
        int x = Mathf.RoundToInt(newPosision.x);
        int y = Mathf.RoundToInt(newPosision.y);

        if (MapMatric.CheckWall(x, y))
        {
            return true;
        }
        return false;
    }
    private bool sick = false;
    public void SickAction()
    {
        sick = true;
    }


    void EatPacman()
    {
        if (ghostMode == GhostMode.Normal)
        {
            if (Vector3.Magnitude(transform.position - pacman.transform.position) < 0.1f)
            {
                Destroy(pacman.gameObject);
            }
        }
        if (ghostMode == GhostMode.Sick1)
        {
            if (Vector3.Magnitude(transform.position - pacman.transform.position) < 0.1f)
            {

            }
        }
    }


    private void SetTarget()
    {
        // this.target = FindForkNearPacman();
        FingStepToTarget();
    }

    private void FindTarget()
    {

    }






    private void FingStepToTarget()
    {
        GreedyBestFirst();
    }


    float t = 1;


    IEnumerator countDownTimeCanGoBack()
    {
        t = 0;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 2.0f));
        t = 1;
    }

    private void GreedyBestFirst()
    {
        if (this.transform.position.x % 1.0 == 0 && this.transform.position.y % 1 == 0)
        {
            float abx = 0;
            List<Vector3> step = new List<Vector3>();
            Vector3 start = this.transform.position;

            float max = 1000;
            Vector3 huong = direction;

            var c = pacman.transform.position - pacman.lastPosision;
            c.Normalize();
            for (int j = 0; j < allDirection.Count; j++)
            {
                if (CheckWall(start + allDirection[j]))
                {
                    continue;
                }
                float distance = CalculatorDistancetoTarget(start + allDirection[j]);
                if (distance < max)
                {
                    abx++;
                    if (abx >= 2)
                    {
                        for (int iiii = 0; iiii < ghosts.Length; iiii++)
                        {
                            if (ghosts[iiii] != this)
                            {
                                if (ghosts[iiii].direction == allDirection[j])
                                {
                                    if (Vector3.Magnitude(ghosts[iiii].transform.position - this.transform.position) < 3)
                                    {
                                        if (this.transform.position.x == ghosts[iiii].transform.position.x || this.transform.position.y == ghosts[iiii].transform.position.y)
                                        {
                                            int kkk = Random.Range(0, 4);
                                            if (kkk <= 1)
                                            {
                                                goto endloopif;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (c.x != 0)
                    {

                        if (Mathf.Abs(this.transform.position.y + allDirection[j].y - pacman.transform.position.y) < Mathf.Abs(this.transform.position.y - pacman.transform.position.y))
                        {
                            huong = allDirection[j];
                            break;
                        }
                    }
                    if (c.y != 0)
                    {
                        if (Mathf.Abs(this.transform.position.x + allDirection[j].x - pacman.transform.position.x) < Mathf.Abs(this.transform.position.x - pacman.transform.position.x))
                        {
                            huong = allDirection[j];
                            break;
                        }
                    }
                    if (t == 1)
                    {
                        if (allDirection[j] == -direction)
                        {
                            // StartCoroutine(countDownTimeCanGoBack());
                        }
                    }
                    else
                    {
                        if (allDirection[j] == -direction)
                        {
                            continue;
                        }
                    }
                    if (t == 1)
                    {
                        if (allDirection[j] == -direction)
                        {
                            StartCoroutine(countDownTimeCanGoBack());
                        }
                    }



                    max = distance;
                    huong = allDirection[j];
                    continue;

                endloopif:
                    int afasf;


                }





            }
            step.Add(huong);
            stepTotarget = step;
        }

    }


    public float CalculatorDistancetoTarget(Vector3 posi)
    {
        float d1 = Vector3.Magnitude(new Vector3(target.x, target.y, 0) - posi);
        float d2 = Vector3.Magnitude(new Vector3(target.x, target.y, 0) - new Vector3(-1, 18, 0)) + Vector3.Magnitude(posi - new Vector3(29, 18, 0));
        float d3 = Vector3.Magnitude(new Vector3(target.x, target.y, 0) - new Vector3(29, 18, 0)) + Vector3.Magnitude(posi - new Vector3(-1, 18, 0));


        float d = d1;
        if (d2 < d1)
        {
            d1 = d2;
        }
        if (d3 < d1)
        {
            d1 = d3;
        }
        return d1;
    }

}



public enum GhostMode
{
    Normal,
    Sick1,
    Sick2,
}


public enum TargetMode
{
    Teleport1,
    TelePort2,
    Direc,
}
