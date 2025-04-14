using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MusicBox : MonoBehaviour , Interactable
{
    public TimeLineManager TLManager;
    public PlayableDirector Cutscene;
    public Sanity Sanity;
    public GameObject InteractText;

    public void Interact()
    {
        TLManager.PlayCutScene(Cutscene);
        Sanity.RemoveVignette();
        Sanity.TurnOffWhisper();
        InteractText.SetActive(false);
    }
}
