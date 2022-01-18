using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePortPoint : MonoBehaviour
{
    Pacman pacman;
    Ghost[] ghosts;
    // Start is called before the first frame update
    void Start()
    {
        pacman = GameObject.FindObjectOfType<Pacman>();
        ghosts = GameObject.FindObjectsOfType<Ghost>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pacman == null)
        {
            return;
        }

        if (transform.position.x == -1 && transform.position.y == 18)
        {
            if (Vector3.Magnitude(transform.position - pacman.transform.position) <= 0.1f)
            {
                pacman.transform.position = new Vector3(28, 18, 0);
            }
            for (int i = 0; i < ghosts.Length; i++)
            {
                if (Vector3.Magnitude(transform.position - ghosts[i].transform.position) <= 0.1f)
                {
                    ghosts[i].transform.position = new Vector3(28, 18, 0);
                }
            }
        }
        else
        {
            if (Vector3.Magnitude(transform.position - pacman.transform.position) <= 0.1f)
            {
                pacman.transform.position = new Vector3(0, 18, 0);
            }
            for (int i = 0; i < ghosts.Length; i++)
            {
                if (Vector3.Magnitude(transform.position - ghosts[i].transform.position) <= 0.1f)
                {
                    ghosts[i].transform.position = new Vector3(0, 18, 0);
                }
            }
        }
    }
}
