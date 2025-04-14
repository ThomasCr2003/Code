using UnityEngine;

public enum EOverworldEvents
{
    Knocking,
    Tv,
}

public enum EShadowEvents
{
    Knocking,
    Whisper,
}

public class RandomEvents : MonoBehaviour
{
    #region Timers And Sanity Reference
    private Sanity _sanity;
    private float timer;
    private float MinRandomTimer = 60; //240
    private float MaxRandomTimer = 120; //360
    private float WaitTime;
    #endregion

    #region Event Variables
    [Header("Sanity Amount Trigger")]
    [SerializeField] private float _SanityEventTrigger;

    [Header("Max Events")]
    [SerializeField] private int _MaxEventsOverworld;
    [SerializeField] private int _MaxEventsShadowDimension;

    [Header("References")]
    [SerializeField] private EOverworldEvents _OverworldEvent;
    [SerializeField] private EShadowEvents _ShadowEvent;

    [Header("Knocking Event")]
    [SerializeField] private GameObject _KnockingGameObject;
    [SerializeField] private Transform[] _OverworldKnockingPlaces;
    [SerializeField] private Transform[] _ShadowKnockingPlaces;

    [Header("Overworld DoorEvent")]
    [SerializeField] private Door[] _OverworldDoors;

    [Header("Overworld TV Event")]
    [SerializeField] private GameObject _OverworldTVOff;
    [SerializeField] private GameObject _OverworldTVOn;
    private bool tvTurnedOn;

    [Header("Shadow Whisper Event")]
    [SerializeField] private GameObject _ShadowWhisper;
    [SerializeField] private Transform[] _ShadowWhisperLocation;
    #endregion

    private void Start()
    {
        _sanity = GetComponent<Sanity>();
        WaitTime = Random.Range(MinRandomTimer, MaxRandomTimer);
        SetTVBackOff();
    }

    private void Update()
    {
        ActivateTimer();
    }

    private void RandomEvent()
    {
        //Overworld Events
        if (!_sanity.GetCurrentDimension())
        {
            int RandomInt = Random.Range(0, _MaxEventsOverworld);
            _OverworldEvent = (EOverworldEvents)RandomInt;

            switch (_OverworldEvent)
            {
                case EOverworldEvents.Knocking:
                    KnockingEventOverworld();
                    break;
                case EOverworldEvents.Tv:
                    TVEventOverworld();
                    break;
                default:
                    Debug.Log("Nothing");
                    break;
            }

        }
        else //Shadow Dimension Events
        {
            int RandomInt = Random.Range(0, _MaxEventsShadowDimension);
            _ShadowEvent = (EShadowEvents)RandomInt;

            switch (_ShadowEvent)
            {
                case EShadowEvents.Knocking:
                    KnockingEventShadow();
                    break;
                case EShadowEvents.Whisper:
                    WhisperEventShadow();
                    break;
                default:
                    break;
            }
        }
    }

    private void ActivateTimer()
    {
        timer += Time.deltaTime;
        if (timer >= WaitTime)
        {
            RandomEvent();
            timer = 0;
            WaitTime = Random.Range(MinRandomTimer, MaxRandomTimer);
        }
    }


    #region RandomEvents
    private void KnockingEventOverworld()
    {
        Debug.Log("Knocking Overworld");
        int RandomInt = Random.Range(0, _OverworldKnockingPlaces.Length);
        Instantiate(_KnockingGameObject, _OverworldKnockingPlaces[RandomInt]);
    }

    #region TVEvent
    private void TVEventOverworld()
    {
        Debug.Log("TV");
        if (tvTurnedOn)
        {
            return;
        }
        else
        {
            _OverworldTVOff.SetActive(false);
            _OverworldTVOn.SetActive(true);
            tvTurnedOn = true;
        }
    }

    public void SetTVBackOff()
    {
        _OverworldTVOff.SetActive(true);
        _OverworldTVOn.SetActive(false);
        tvTurnedOn = false;
    }
    #endregion


    private void KnockingEventShadow()
    {
        Debug.Log("Knocking Shadow");
        int RandomInt = Random.Range(0, _ShadowKnockingPlaces.Length);
        Instantiate(_KnockingGameObject, _ShadowKnockingPlaces[RandomInt]);
    }

    private void WhisperEventShadow()
    {
        Debug.Log("Whisper");
        int RandomInt = Random.Range(0, _ShadowWhisperLocation.Length);
        Instantiate(_ShadowWhisper, _ShadowWhisperLocation[RandomInt]);
    }
    #endregion
}
