using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetupScene : State
{
    public int availableBudget;
    private Text budgetText;
    private Button startLevelButton;

    private const string budgetString = "Budget : ";

    void Start()
    {
        budgetText = GameObject.Find("BudgetText").GetComponent<Text>();
        startLevelButton = GameObject.Find("StartLevelButton").GetComponent<Button>();

        budgetText.text = budgetString + availableBudget;
        startLevelButton.GetComponentInChildren<Text>().text = "Start New Wave";

        startLevelButton.GetComponent<Button>().onClick.AddListener(() => { StartLevel(); });
    }

    void StartLevel ()
    {
        SpawnWave spawnState = gameObject.GetComponent<SpawnWave>();
        nextState = spawnState;
    }

    void OnEnable()
    {
        if(budgetText)
        {
            budgetText.text = budgetString + availableBudget;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && availableBudget >= 100)
        {
            gameObject.GetComponent<CreateBuildingScript>().type = BuildingType.Sun;
            nextState = gameObject.GetComponent<CreateBuildingScript>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && availableBudget >= 120)
        {
            gameObject.GetComponent<CreateBuildingScript>().type = BuildingType.Moon;
            nextState = gameObject.GetComponent<CreateBuildingScript>();
        }
    }
    //} else if(Input.GetKeyDown(KeyCode.Alpha2))
    //{
    //    SpawnWave spawnState = gameObject.GetComponent<SpawnWave>();
    //    nextState = spawnState;
    //}
}

