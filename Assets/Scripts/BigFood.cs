using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFood : MonoBehaviour
{
    public int value = 100;

    Pacman pacman;


    private void Start()
    {
        pacman = GameObject.FindObjectOfType<Pacman>();
    }

    private void Update()
    {
        if (pacman == null) return;

        if (Vector3.Magnitude(pacman.transform.position - this.transform.position) < 0.1f)
        {
            Eaten();
            for (int i = 0; i < GameManager.instance.ghosts.Length; i++)
            {
                // GameManager.instance.ghosts[i].SickAction();
            }
        }
    }


    private void Eaten()
    {
        GameManager.instance.PlusCurrentScore(this.value);
        Destroy(gameObject);
    }
}
