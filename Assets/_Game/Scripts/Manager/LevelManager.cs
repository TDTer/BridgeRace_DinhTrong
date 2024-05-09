using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public int CharacterAmount => currentLevel.botAmount + 1;

    public Level currentLevel;
    public Vector3 FinishPoint => currentLevel.finishPoint.position;

    void Start()
    {
        currentLevel.OnInit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
