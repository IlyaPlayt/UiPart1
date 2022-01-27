using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject victoryPopap;
    [SerializeField] private GameObject stagePopap;
    [SerializeField] private GameObject[] emptyStars;
    [SerializeField] private GameObject[] fullStars;
    private Vector3[] points;
    private Vector3 startStarsPositionBuffer=new Vector3(0,76f,0);

    private Transform[] shootingStars;
    private float timer;
    private bool levelStarted;
    private Level curentLevel;

    void Start()
    {
        setDefaultState();
        points = new Vector3[emptyStars.Length];
        //panelRoot.SetActive(false);
    }

    void Update()
    {
        if (!levelStarted) return;
        
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ActiveStars(1);
                curentLevel.starsAmount = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ActiveStars(2);
                curentLevel.starsAmount = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ActiveStars(3);
                curentLevel.starsAmount = 3;
            }
        

        if (shootingStars == null || timer > 1f)
            return;
        for (var i = 0; i < shootingStars.Length; i++)
        {
            shootingStars[i].position = Vector3.Lerp(points[i], emptyStars[i].transform.position, timer);
        }

        timer += Time.deltaTime;


        if (timer < 1f)
            return;
        
        var amount = shootingStars.Length;
        for (var i = 0; i < emptyStars.Length; i++)
        {

            emptyStars[i].SetActive(i >= amount);

        }


        shootingStars = null;
    }

    public void RunLevel(Level level)
    {
        curentLevel = level;
        levelStarted = true;
        stagePopap.SetActive(false);
        victoryPopap.SetActive(true);
    }

    private void setDefaultState()
    {
        foreach (var fullStar in fullStars)
        {
            fullStar.SetActive(false);
        }
        foreach (var emptyStar in emptyStars)
        {
            emptyStar.SetActive(true);
        }
    }

    private void ActiveStars(int amount)
    {
        for (int i = 0; i < emptyStars.Length; i++)
        {
            emptyStars[i].SetActive(i >= amount);
            fullStars[i].SetActive(i < amount);
        }

        if (amount == 0)
        {
            return;
        }
        for (var i = 0; i < emptyStars.Length; i++)
        {
            points[i] = emptyStars[i].transform.position-startStarsPositionBuffer;
        }
        shootingStars = new Transform[amount];
        for (int i = 0; i < amount; i++)
        {
            shootingStars[i] = fullStars[i].transform;
        }

        timer = 0;
    }

    public void callStagePopup()
    {
        stagePopap.SetActive(true);
        victoryPopap.SetActive(false);
        setDefaultState();
        curentLevel.setStarsActive();
        curentLevel = null;
        levelStarted = false;
        shootingStars = null;

    }
}