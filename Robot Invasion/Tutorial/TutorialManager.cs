using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Manager
{
    public List<Tutorial> tutorials = new List<Tutorial>();
    public Tutorial currentTutorial;
    private Timer timer = new Timer();

    public override void Start()
    {
        SetNextTutorial(0);
    }
    
    public override void Update()
    {
        UpdateCheck();
        if (timer.TimerDone() && timer.isActive)
        {
            GameManager.LoadLevel(Levels.Level1);
            timer.StopTimer();
        }
    }
    
    /// <summary>
    /// Sets the next Tutorial in the list.
    /// </summary>
    /// <param name="currentOrder"></param>
    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);
        if (!currentTutorial)
        {
            CompletedAllTutorials();
            return;
        }
    }

    /// <summary>
    /// If All Tutorials are completed Start the next level.
    /// </summary>
    public void CompletedAllTutorials()
    {
        GameManager.instance.tutorialCompleted = true;
        timer.SetTimer(5f);
    }

    /// <summary>
    /// Gets Tutorail by the order.
    /// </summary>
    /// <param name="_order"></param>
    /// <returns></returns>
    public Tutorial GetTutorialByOrder(int _order)
    {
        for (int i = 0; i < tutorials.Count; i++)
        {
            if (tutorials[i].order == _order)
            {
                return tutorials[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Executes next tutorial.
    /// </summary>
    public void CompletedTutorial()
    {
        SetNextTutorial(currentTutorial.order + 1);
    }

    public void UpdateCheck()
    {
        if (currentTutorial != null)
        {
            if (currentTutorial)
            {
                currentTutorial.CheckIfHappening();
            }
        }
        else
        {
            return;
        }
    }
}
