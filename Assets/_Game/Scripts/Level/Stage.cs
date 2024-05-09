using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Transform Bricks;
    [SerializeField] private List<Brick> spawnBrickList = new List<Brick>();
    private int bricksAmountOfEachCharater;


    public void OnInit()
    {
        foreach (Transform child in Bricks)
        {
            spawnBrickList.Add(child.GetComponent<Brick>());
        }
        bricksAmountOfEachCharater = spawnBrickList.Count / LevelManager.Ins.CharacterAmount;
    }

    public void InitColor(ColorType colorType)
    {
        for (int i = 0; i < bricksAmountOfEachCharater; i++)
        {
            SpawnNewBrick(colorType);
        }
    }

    public void SpawnNewBrick(ColorType colorType)
    {
        if (spawnBrickList.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, spawnBrickList.Count);
            Brick brick = spawnBrickList[rand];
            brick.stage = this;
            brick.ChangeColor(colorType);
            spawnBrickList.RemoveAt(rand);
        }
    }
}
