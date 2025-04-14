using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineActivate : MonoBehaviour
{
    private TimeLineManager Manager;
    public PlayableDirector CutScene;
    public GameObject NextCutscene;
    private AudioSource Audio;
    

    private void Awake()
    {
        Manager = GetComponentInParent<TimeLineManager>();
        Audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameObject.SetActive(false);    
    }

    private void OnTriggerEnter(Collider other)
    {
        Manager.PlayCutScene(CutScene);
        if (Audio != null)
        {
            Audio.Stop();
        }
        if (NextCutscene != null)
        {
            Manager.GetNextTrigger(NextCutscene);
        }
        else
        {
            Debug.Log("No Next Scene Found");
        }
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (Audio != null)
        {
            Audio.Play();
        }
    }
}
