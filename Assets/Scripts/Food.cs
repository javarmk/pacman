using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int value = 10;

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
        }
    }


    private void Eaten()
    {
        GameManager.instance.PlusCurrentScore(this.value);
        Destroy(gameObject);
    }
}
