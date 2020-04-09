using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class TrackAnalytics : MonoBehaviour
{
    private bool isFinishedSendingResults;
    private float timeSpentInLevelBeforeDeath;

    // Start is called before the first frame update
    void Start()
    {
        timeSpentInLevelBeforeDeath = 0;
        isFinishedSendingResults = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinishedSendingResults == false)
        {
            timeSpentInLevelBeforeDeath += Time.deltaTime;
        }

        if (Health.isPlayerDead == true && isFinishedSendingResults == false)
        {
            ReportAnalytics();
        }
    }

    public void ReportAnalytics()
    {
        AnalyticsResult waveNumberResult = Analytics.CustomEvent("Wave number before death", new Dictionary<string, object>
        {
            { "Wave number", EnemySpawnerController.currentWave }
        });
        //Debug.Log("Wave number before death " + waveNumberResult);

        AnalyticsResult jumpsMadeResult = Analytics.CustomEvent("Jumps made before death", new Dictionary<string, object>
        {
            { "Jumps made", PlayerMovementV2.jumpsMade }
        });
        //Debug.Log("Jumps made before death " + jumpsMadeResult);

        AnalyticsResult shotsMadeResult = Analytics.CustomEvent("Shots made before death", new Dictionary<string, object>
        {
            { "Shots made", SpawnObject.shots }
        });
        //Debug.Log("Shots made before death " + shotsMadeResult);

        AnalyticsResult enemiesKilledResult = Analytics.CustomEvent("Enemies killed before death", new Dictionary<string, object>
        {
            { "Enemies killed", TestEnemyController.enemiesKilled }
        });
        //Debug.Log("Enemies killed before death " + enemiesKilledResult);

        AnalyticsResult timeSpentInLevelResult = Analytics.CustomEvent("Time spent in level before death",
            new Dictionary<string, object>
        {
            { "Time spent in level", timeSpentInLevelBeforeDeath }
        });
        //Debug.Log("Time spent in level before death " + timeSpentInLevelResult);

        isFinishedSendingResults = true;
    }
}
