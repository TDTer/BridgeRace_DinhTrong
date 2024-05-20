using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levelPrefabs;
    private Level currentLevel;
    public Bot botPrefab;
    public Player player;
    private int levelIndex;
    private List<Bot> bots = new List<Bot>();
    public Vector3 FinishPoint => currentLevel.finishPoint.position;
    public int CharacterAmount => currentLevel.botAmount + 1;

    private void Awake()
    {
        levelIndex = 0;
    }
    void Start()
    {
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Ins.OpenUI<MainMenu>();
    }

    public void OnInit()
    {
        float space = 3.0f;
        Vector3 firstCharPos = currentLevel.startPoint.position - ((CharacterAmount / 2) * space - ((CharacterAmount - 1) % 2) * (space / 2)) * Vector3.right;

        List<Vector3> startPoints = new List<Vector3>();
        for (int i = 0; i < CharacterAmount; i++)
        {
            startPoints.Add(firstCharPos + space * Vector3.right * i);
        }

        //random danh sach mau
        List<ColorType> colorDatas = new List<ColorType>();

        var colorTypes = Enum.GetValues(typeof(ColorType)).Cast<ColorType>().Where(c => c != ColorType.None).ToList();
        int n = colorTypes.Count;

        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(1, n);
            ColorType value = colorTypes[k];
            colorTypes[k] = colorTypes[n];
            colorTypes[n] = value;
        }
        colorDatas = colorTypes;

        //set vi tri player va mau player
        int rand = UnityEngine.Random.Range(0, CharacterAmount);
        player.TF.position = startPoints[rand] + Vector3.up * 0.1f;
        player.TF.rotation = Quaternion.identity;
        startPoints.RemoveAt(rand);
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);
        player.OnInit();

        //update navmesh data
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentLevel.navMeshData);

        //bots
        for (int i = 0; i < CharacterAmount - 1; i++)
        {
            Bot bot = Instantiate(botPrefab, startPoints[i], Quaternion.identity);
            bot.ChangeColor(colorDatas[i]);
            bot.OnInit();
            bots.Add(bot);
        }
    }
    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levelPrefabs[level]);
        currentLevel.OnInit();
    }

    public void OnStartGame()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new PatrolState());
        }
    }

    public void OnFinishGame()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(null);
            bots[i].MoveStop();
        }
    }

    public void OnReset()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            Destroy(bots[i].gameObject);
        }
        bots.Clear();
    }

    internal void OnRetry()
    {
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Ins.OpenUI<MainMenu>();
    }

    internal void OnNextLevel()
    {
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Ins.OpenUI<MainMenu>();
    }

}
