using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EButtonColor
{
    Green,
    Red,
    Blue,
    Yellow
}
public class Puzzle1Button : MonoBehaviour
{
    public EButtonColor buttonColor;
    public Puzzle1 Puzzle;
    public int CurrentRandomNumber;
    public Animation Animation;


    public void PressButton()
    {
        Puzzle.ButtonPressed((int)buttonColor);
        Animation.Play();
    }
}
