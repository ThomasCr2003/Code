using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globe : MonoBehaviour 
{
    [Header("Globe")]
    [SerializeField] private int[] _ESymbolsOrder;
    [SerializeField] private int[] _ESymbolsSelected;
    [SerializeField] private int _TotalSymbols;
    [SerializeField] private bool[] _Completed;

    [Header("StickyNotes")]
    [SerializeField] private GameObject[] _StickyNotes;
    [SerializeField] private Material[] _StickyNoteMaterial;

    private Animation _Animation;
    private AudioSource _CompletionAudio;
    private int _RandomNumber;
    public bool PuzzleCompleted;
    public bool CanStart;

    private void Awake()
    {
        RandomizeOrder();
        SetStartSelectedOrder();
        _Animation = GetComponent<Animation>();
        _CompletionAudio = GetComponent<AudioSource>();
    }

    private void CheckIfRightOrder()
    {
        if (_Completed[0] == true && _Completed[1] == true && _Completed[2] == true)
        {
            PuzzleCompleted = true;
            _Animation.Play();
            _CompletionAudio.Play();
        }
    }

    public void CheckIfRightOrderStart(int CurrentPart)
    {
        if (_ESymbolsSelected[CurrentPart] == _ESymbolsOrder[CurrentPart])
        {
            _Completed[CurrentPart] = true;
            Debug.Log("Done");
        }
        else
        {
            _Completed[CurrentPart] = false;
        }
        CheckIfRightOrder();
    }

    public void AddNumber(int CurrentPart)
    {
        _ESymbolsSelected[CurrentPart] += 1;
        if (_ESymbolsSelected[CurrentPart] > _TotalSymbols)
        {
            _ESymbolsSelected[CurrentPart] = 0;
        }
        if (_ESymbolsSelected[CurrentPart] == _ESymbolsOrder[CurrentPart])
        {
            _Completed[CurrentPart] = true;
            Debug.Log("Done");
        }
        else
        {
            _Completed[CurrentPart] = false;
        }
        CheckIfRightOrder();
    }
    private void SetStartSelectedOrder()
    {
        for (int i = 0; i < _ESymbolsSelected.Length; i++)
        {
            _ESymbolsSelected[i] = 0;
        }
    }

    private void RandomizeOrder()
    {
        for (int i = 0; i < 3; i++)
        {
            _RandomNumber = Random.Range(0,_TotalSymbols);
            _ESymbolsOrder[i] = _RandomNumber;
            if (_ESymbolsOrder[i] < _TotalSymbols)
            {
                _ESymbolsOrder[i] = _RandomNumber + 1;
            }
            else
            {
                _ESymbolsOrder[i] = 0;
            }
            _StickyNotes[i].GetComponent<MeshRenderer>().material = _StickyNoteMaterial[_RandomNumber];
            if (_ESymbolsOrder[i] != 0)
            {
                _ESymbolsOrder[i] -= 1;
            }
        }
    }

    public int GetCurrentSymbol(int CurrentPart)
    {
        return _ESymbolsSelected[CurrentPart];
    }
}
