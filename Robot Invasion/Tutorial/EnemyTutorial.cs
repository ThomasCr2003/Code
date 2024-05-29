using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : Tutorial
{
    public EnemyType enemy;
    private bool isCurrentObjective;
    public override void CheckIfHappening()
    {
        isCurrentObjective = true;
        Completed();
    }

    private void Completed()
    {
        for (int i = 0; i < GameManager.instance.enemies.Count; i++)
        {
            if (GameManager.instance.enemies[i].gameObject.GetComponent<EnemyStats>().enemyType == enemy)
            {
                if (GameManager.instance.enemies[i].enemyDead && isCurrentObjective)
                {
                    GameManager.instance.doors[order].GetComponent<TutorialDoor>().OpenDoor();
                    GameManager.GetManager<TutorialManager>().CompletedTutorial();
                    isCurrentObjective = false;
                }
            }
        }
    }
}
