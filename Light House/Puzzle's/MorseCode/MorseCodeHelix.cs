using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCodeHelix : MonoBehaviour
{
    private Animator _animation;
    [SerializeField] private float FailTime, ShortTime, LongTime;

    private void Start()
    {
        _animation = GetComponent<Animator>();
    }

    public void Fail()
    {
        StartCoroutine("SetFail");
    }

    public void ShortBlink()
    {
        StartCoroutine("SetShort");
    }

    public void LongBlink()
    {
        StartCoroutine("SetLong");
    }

    public void SetOff(bool State)
    {
        _animation.SetBool("Off",State);
    }

    IEnumerator SetFail()
    {
        SetOff(false);
        _animation.SetTrigger("Fail");
        yield return new WaitForSeconds(FailTime);
        _animation.ResetTrigger("Fail");
        SetOff(true);
    }

    IEnumerator SetShort()
    {
        SetOff(false);
        _animation.SetTrigger("Short");
        yield return new WaitForSeconds(ShortTime);
        _animation.ResetTrigger("Short");
        SetOff(true);
    }

    IEnumerator SetLong()
    {
        SetOff(false);
        _animation.SetTrigger("Long");
        yield return new WaitForSeconds(LongTime);
        _animation.ResetTrigger("Long");
        SetOff(true);
    }
}
