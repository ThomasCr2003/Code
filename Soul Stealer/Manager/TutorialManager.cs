using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance { private set; get; }

    [SerializeField] private GameObject _MovingText;
    [SerializeField] private GameObject _StatChangeText;
    [SerializeField] private GameObject _AbilityChoosingText;
    [SerializeField] private GameObject _AbilityUsingText;
    [SerializeField] private GameObject _MusicText;
    private bool _movingTextHappend;
    private bool _statTextHappend;
    private bool _abilityTextHappend;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        DisplayMovingText();
    }

    public void DisplayMovingText()
    {
        if (_movingTextHappend)
        {
            return;
        }
        else
        {
            Instantiate(_MovingText);
            _movingTextHappend = true;
            StartCoroutine("PlayMusicText");
        }
    }

    IEnumerator PlayMusicText()
    {
        yield return new WaitForSeconds(5);
        Instantiate(_MusicText);
    }

    public void DisplayStatChangeText()
    {
        if (_statTextHappend)
        {
            return;
        }
        else
        {
            Instantiate(_StatChangeText);
            _statTextHappend = true;
        }
    }

    public void DisplayAbilityChoosingText()
    {
        if (_abilityTextHappend)
        {
            return;
        }
        else
        {
            StartCoroutine("StartAbilityChooseDisplay");
            Instantiate(_AbilityChoosingText);
            _abilityTextHappend = true;
        }
    }

    IEnumerator StartAbilityChooseDisplay()
    {
        yield return new WaitForSeconds(0.5f);
        DisplayAbilityUseText();
        yield return null;
    }

    public void DisplayAbilityUseText()
    {
        Instantiate(_AbilityUsingText);
    }
}
