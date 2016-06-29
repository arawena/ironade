using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelScreenScript : State {

    public int level = 0;

    private Text levelText;
    private GameObject levelImage;

    public List<LevelDescription> spawnList;

    private const float timeToDisplay = 3f;
    private float timer;

    void Start ()
    {
    }
    void OnEnable()
    {
        if (!levelImage && !levelText)
        {
            levelImage = GameObject.Find("LevelImage");
            levelText = GameObject.Find("LevelText").GetComponent<Text>();
            spawnList = FileOperations.getLevelDescription("spawnPresets");
        }
        if(level >= spawnList.Count)
        {
            levelText.text = "You Won!";
            doCleanup();
        }
        else if (level >= 0)
        {
            timer = 0;
            levelText.text = "Level " + (level + 1);
        }
        else 
        {
            levelText.text = "You Lost!";
            doCleanup();
        }
        levelImage.SetActive(true);
    }
	
    void doCleanup()
    {
        GameObject[] others = GameObject.FindGameObjectsWithTag("farmer");
        foreach (GameObject other in others)
        { Destroy(other); }
        others = GameObject.FindGameObjectsWithTag("building");
        foreach (GameObject other in others)
        { Destroy(other); }
    }
	void Update () {
        if(level>=0 && level < spawnList.Count)
        {
            timer += Time.deltaTime;
            if (timer > timeToDisplay)
            {
                levelImage.SetActive(false);
                InitializeLevel();
            }
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
