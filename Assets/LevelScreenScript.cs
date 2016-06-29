using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelScreenScript : State {

    public int level = -1;

    private Text levelText;
    private GameObject levelImage;

    public List<LevelDescription> spawnList;

    private const float timeToDisplay = 3f;
    private float timer;

    void Start ()
    {
        spawnList = FileOperations.getLevelDescription("spawnPresets");
    }

    void OnEnable () {
        level++;
        timer = 0;
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelImage = GameObject.Find("LevelImage");

        levelText.text = "Level " + level;
        levelImage.SetActive(true);
    }
	
	void Update () {
        timer += Time.deltaTime;
        if(timer > timeToDisplay)
        {
            levelImage.SetActive(false);
            InitializeLevel();
        }
	}

    public void InitializeLevel()
    {
        GetComponent<SetupScene>().availableBudget = spawnList[level].budget;
        nextState = gameObject.GetComponent<SetupScene>();
        SpawnWave spawnWaves = GetComponent<SpawnWave>();
        spawnWaves.spawnWave = spawnList[level].enemySpawnDetails;
    }
}
