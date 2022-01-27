using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelNumber;
    public int starsAmount;
    [SerializeField] private GameObject[] emptyStars;
    [SerializeField] private GameObject[] fullStars;
    void Start()
    {
        foreach (var fullStar in fullStars)
        {
            fullStar.SetActive(false);
        }
    }

    public void setStarsActive()
    {
        for (int i = 0; i < starsAmount; i++)
        {
            fullStars[i].SetActive(true);
        }
        
    }
    
}
