using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    public float speed = 0.05f;
    public Vector3 direction;
    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        print(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
    }
    int step = 0;

    private void HandleMove()
    {
        lastPosision = this.transform.position;
        if (isMoving)
        {
            if (step >= 20)
            {
                isMoving = false;
                step = 0;
            }
            else
            {
                var newPosision = new Vector3(transform.position.x + (direction.x * speed), transform.position.y + (direction.y * speed), 0);
                if (step == 19)
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
    public Vector3 lastPosision;
    public bool IsMoving()
    {
        if (lastPosision != this.transform.position
        )
        {
            return true;
        }
        return false;
    }

    private void SetDirection()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        if (direction.x != 0 && direction.y != 0)
        {
            direction.x = 0;
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
}
