using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeLineManager : MonoBehaviour
{
    public TutorialText Tutorial;
    private bool firstCutscene, ringCutscene;
    public PlayableDirector StartCutscene;
    public Camera PlayerCamera;
    public CharacterController CharacterController;
    public GameObject NextCutSceneTrigger, Ring, InteractText;
    public Teleport Teleport;
    public Globe Globe;
    public Image Dot;
    

    void Start()
    {
        PlayCutScene(StartCutscene);
        Ring.SetActive(false);
        Teleport.SetTeleport(false);
    }

    public void CutSceneDone()
    {
        PlayerHandler(true);
        if (NextCutSceneTrigger != null)
        {
            NextCutSceneTrigger.SetActive(true);
        }
        if (!firstCutscene)
        {
            Tutorial.StartTutorialText();
            firstCutscene = true;
        }
    }

    public void PlayCutScene(PlayableDirector Scene)
    {
        Scene.Play();
        PlayerHandler(false);
        InteractText.SetActive(false);
    }

    public void GetNextTrigger(GameObject Object)
    {
        NextCutSceneTrigger = Object;
    }

    private void PlayerHandler(bool State)
    {
        PlayerCamera.enabled = State;
        CharacterController.enabled = State;
        Dot.enabled = State;
        if (ringCutscene)
        {
            Teleport.SetTeleport(true);
        }
    }

    public void EnableRing()
    {
        Ring.SetActive(true);
        Globe.CanStart = true;
        Teleport.SetTeleport(true);
        Tutorial.StartMechanicTutorialText();
        ringCutscene = true;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
