using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringpart : MonoBehaviour , Interactable
{
    [SerializeField] private int _Currentpart;
    [SerializeField] private int _AnimCount;
    public Animation Anim;
    public AnimationClip[] AnimClip;
    private Globe _globe;
    private Timer _interactDelayTimer = new();
    private float _interactTimeWait = 1f;

    private void Start()
    {
        _globe = GetComponentInParent<Globe>();
        Anim = GetComponent<Animation>();
        Anim.clip = AnimClip[_globe.GetCurrentSymbol(_Currentpart)];
        _AnimCount = _globe.GetCurrentSymbol(_Currentpart);
        _globe.CheckIfRightOrderStart(_Currentpart);
    }

    public void Interact()
    {
        if (!_globe.PuzzleCompleted && _interactDelayTimer.TimerDone() && _globe.CanStart)
        {
            _interactDelayTimer.SetTimer(_interactTimeWait);
            if (_AnimCount >= 12)
            {
                _AnimCount = 0;
            }
            else
            {
                _AnimCount += 1;
            }
            Anim.Play();
            _globe.AddNumber(_Currentpart);
            Anim.clip = AnimClip[_globe.GetCurrentSymbol(_Currentpart)];
            Debug.Log("Interacted");
        }
    }
}
