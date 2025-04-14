using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCodePuzzle : MonoBehaviour , Interactable
{
    private const string MorseCodeWord = "RedPaint";
    public Light LightBulb;
    [SerializeField] private GameObject _LightOmhulshing;
    [SerializeField] private MorseCodeHelix _MorseCodeHelix;
    [SerializeField] private Painting p;
    [SerializeField] private GameObject _LampDecoy;
    [SerializeField] private Animator _Hands;
    public bool _LightBulbPickedUp;
    private int _morseProgress;

    #region Timers
    private float _activationDelay = 7f;
    private Timer _delayTimer = new();
    private float _generalDelay = 2.5f;
    private Timer _generalDelayTimer = new();
    private Timer _shortBlink = new();
    private Timer _longBlink = new();
    private float _shortBlinkFloat = 0.5f, _longBlinkFloat = 1f;

    #endregion
    public enum MorseLang
    {
        Space,
        Short,
        Long
    }

    private MorseLang[] morseCode = { MorseLang.Short, MorseLang.Long, MorseLang.Short, MorseLang.Space, MorseLang.Short, MorseLang.Space, MorseLang.Long, MorseLang.Short, MorseLang.Short
  , MorseLang.Space, MorseLang.Short , MorseLang.Long, MorseLang.Long, MorseLang.Short, MorseLang.Space, MorseLang.Short, MorseLang.Long, MorseLang.Space, MorseLang.Short, MorseLang.Short,MorseLang.Space
  , MorseLang.Long, MorseLang.Short, MorseLang.Space , MorseLang.Long, };


    private void Start()
    {
        LightBulb.enabled = false;
        _LightOmhulshing.SetActive(false);
        _MorseCodeHelix = GetComponentInChildren<MorseCodeHelix>();
    }

    public void NextMorseActivation(MorseLang morse)
    {
        switch (morse)
        {
            case MorseLang.Space:
                LightBulb.enabled = false;
                _MorseCodeHelix.SetOff(true);
                break;
            case MorseLang.Short:
                LightBulb.enabled = true;
                _MorseCodeHelix.ShortBlink();
                _shortBlink.SetTimer(_shortBlinkFloat);
                break;
            case MorseLang.Long:
                LightBulb.enabled = true;
                _MorseCodeHelix.LongBlink();
                _longBlink.SetTimer(_longBlinkFloat);
                break;
            default:
                break;
        }

        _morseProgress++;
        if (_morseProgress >= morseCode.Length)
        {
            _morseProgress = 0;
            _delayTimer.SetTimer(_activationDelay);
        }
        else
        {
            _generalDelayTimer.SetTimer(_generalDelay);
        }

    }

    public void StopMorseCodePuzzle()
    {
        _delayTimer.StopTimer();
        _generalDelayTimer.StopTimer();
    }

    public void ActivateMorseCodePuzzle()
    {
        _LightOmhulshing.SetActive(true);
        _delayTimer.SetTimer(_activationDelay);
    }

    public MorseLang GetProgress(int currentProgress)
    {
        return morseCode[currentProgress];
    }

    private void Update()
    {
        if (_delayTimer.TimerDone() && _delayTimer.isActive)
        {
            _delayTimer.StopTimer();
            NextMorseActivation(GetProgress(_morseProgress));
        }

        if (_generalDelayTimer.isActive && _generalDelayTimer.TimerDone())
        {
            _generalDelayTimer.StopTimer();
            NextMorseActivation(GetProgress(_morseProgress));
        }

        if (_shortBlink.isActive && _shortBlink.TimerDone())
        {
            _shortBlink.StopTimer();
            LightBulb.enabled = false;
            _MorseCodeHelix.SetOff(true);
        }

        if (_longBlink.isActive && _longBlink.TimerDone())
        {
            _longBlink.StopTimer();
            LightBulb.enabled = false;
            _MorseCodeHelix.SetOff(true);
        }
    }

    public void Interact()
    {
        if (_LightBulbPickedUp)
        {
            ActivateMorseCodePuzzle();
            Destroy(_LampDecoy);
            _Hands.SetBool("IsHoldingItem", false);
            p.CanStart = true;
        }
        else
        {
            _MorseCodeHelix.Fail();
        }
    }
}
