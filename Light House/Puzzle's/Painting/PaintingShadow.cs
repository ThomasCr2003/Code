using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PaintingShadow : MonoBehaviour, Interactable
{
    public PlayableDirector Cutscene;
    public bool CutsceneCanPlay;
    [SerializeField] private TimeLineManager Manager;

    private void Start()
    {
        this.enabled = false;
    }

    public void Interact()
    {
        if (CutsceneCanPlay)
        {
            Manager.PlayCutScene(Cutscene);
        }
    }
}
