using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int order;
    [TextArea(3, 10)]
    public string explenation;
    
    private void Start()
    {
        GameManager.GetManager<TutorialManager>().tutorials.Add(this);
    }

    public virtual void CheckIfHappening()
    {

    }
}
