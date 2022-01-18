using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public int maxScore = 0;



    private int currentScore = 0;

    public Pacman pacman;
    public Ghost[] ghosts;

    List<Vector2> forksNearPacman;
    List<bool> use;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            LoadMaxScore();
        }


    }
    private void OnEnable()
    {
        pacman = GameObject.FindObjectOfType<Pacman>();
        ghosts = GameObject.FindObjectsOfType<Ghost>();

        FindNewFork();
        MapMatric.CloseDoor();
    }
    private void Update()
    {
        if (pacman == null)
        {
            return;
        }
        for (int i = 0; i < forksNearPacman.Count; i++)
        {
            if (Vector3.Magnitude(pacman.transform.position - new Vector3(forksNearPacman[i].x, forksNearPacman[i].y, 0)) < 0.5f)
            {
                FindNewFork();
            }
        }
    }

    public void FindNewFork()
    {
        forksNearPacman = MapMatric.FindForkNearPosision((int)pacman.transform.position.x, (int)pacman.transform.position.y);
        use = new List<bool>();
        for (int i = 0; i < 10; i++)
        {
            use.Add(false);
        }
        SetTargetToGhosts(40);
    }


    public void Keugoiae(Vector3 posision)
    {
        // SetTargetToGhosts(100);
    }



    public void SetTargetToGhosts(int aa)
    {
        for (int i = 0; i < forksNearPacman.Count; i++)
        {
            if (use[i])
            {
                continue;
            }
            float max = 1000;
            int index = -1;
            for (int j = 0; j < ghosts.Length; j++)
            {
                float d = Caculator(new Vector3(forksNearPacman[i].x, forksNearPacman[i].y, 0), ghosts[j].transform.position);
                if (d > aa) continue;
                if (d < max)
                {
                    max = d;
                    index = j;
                }
            }
            if (index != -1)
            {
                use[i] = true;

                ghosts[index].target = forksNearPacman[i];

            }
        }

        for (int kki = 0; kki < ghosts.Length; kki++)
        {
            for (int ii = 0; ii < forksNearPacman.Count; ii++)
            {
                if (forksNearPacman[ii] != ghosts[kki].target)
                {
                    if (ii == forksNearPacman.Count - 1)
                    {
                        var direc = pacman.transform.position - pacman.lastPosision;

                        var cccc = MapMatric.FindBigPoint();

                        int tswa = UnityEngine.Random.Range(0, 1);
                        if (tswa == 0 && cccc != Vector2.zero)
                        {
                            if (cccc != Vector2.zero)
                            {
                                ghosts[kki].target = cccc;
                            }
                        }
                        else
                        {
                            direc.Normalize();
                            int i = UnityEngine.Random.Range(3, 10);
                            direc *= i;
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
                            ghosts[kki].target = direc;

                        }

                    }
                }
            }
        }
    }

    private float Caculator(Vector3 a, Vector3 b)
    {
        float d1 = Vector3.Magnitude(a - b);
        float d2 = Vector3.Magnitude(a - new Vector3(-1, 18, 0)) + Vector3.Magnitude(b - new Vector3(29, 18, 0));
        float d3 = Vector3.Magnitude(a - new Vector3(29, 18, 0)) + Vector3.Magnitude(b - new Vector3(-1, 18, 0));


        float d = d1;

        if (d > d2)
        {
            d = d2;
        }
        if (d > d3)
        {
            d = d3;
        }
        return d;
    }



    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void PlusCurrentScore(int value)
    {
        currentScore += value;
    }


    private void LoadMaxScore()
    {
        string path = Application.persistentDataPath + "/maxScore.json";
        if (File.Exists(path))
        {

            var json = File.ReadAllText(path);
            MaxScore data = JsonUtility.FromJson<MaxScore>(json);
            this.maxScore = data.maxScore;
        }
        else
        {
            this.maxScore = 0;
            return;
        }
    }
    private void WriteMaxScrore()
    {
        string path = Application.persistentDataPath + "/maxScore.json";
        if (File.Exists(path))
        {
            MaxScore maxScore = new MaxScore();
            maxScore.maxScore = this.maxScore;
            string json = JsonUtility.ToJson(maxScore);
            File.WriteAllText(path, json);
        }
        else
        {
            File.Create(Application.persistentDataPath + "/maxScore.json");
            MaxScore maxScore = new MaxScore();
            maxScore.maxScore = this.maxScore;
            string json = JsonUtility.ToJson(maxScore);
            File.WriteAllText(path, json);
        }

    }

    [Serializable]
    private class MaxScore
    {
        public int maxScore;
    }
}


public enum GameMode
{
    Normal,
    end,
}
