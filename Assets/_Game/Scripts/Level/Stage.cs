using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Transform Bricks;
    [SerializeField] private List<Brick> spawnBrickList = new List<Brick>();
    private int bricksAmountOfEachCharater;
    private Dictionary<ColorType, List<Brick>> pendingBrickList = new Dictionary<ColorType, List<Brick>>();


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

            // Add brick vào list tương ứng
            if (pendingBrickList.ContainsKey(colorType))
            {
                // Nếu key đã tồn tại, thêm Brick mới vào List hiện tại
                var brickList = pendingBrickList[colorType];
                brickList.Add(brick);
            }
            else
            {
                // Nếu key chưa tồn tại, tạo một List mới với Brick mới
                pendingBrickList[colorType] = new List<Brick> { brick };
            }
        }
    }

    internal void AddBrick(Brick brick)
    {
        if (pendingBrickList.ContainsKey(brick.ColorType))
        {
            var brickList = pendingBrickList[brick.ColorType];
            brickList.RemoveAt(brickList.IndexOf(brick));
        }
        brick.ChangeColor(ColorType.None);
        spawnBrickList.Add(brick);
    }

    internal Brick FindBrick(ColorType colorType)
    {
        Brick brick = null;

        var brickList = pendingBrickList[colorType];
        if (brickList.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, brickList.Count);
            brick = brickList[rand];
        }

        return brick;
    }
}
