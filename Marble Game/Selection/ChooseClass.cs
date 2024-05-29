using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseClass : MonoBehaviour
{
    public EStyleType Type;
    public bool canReset;
    private ClassManager _classManager;
    private ClassButtonText _currentClassText;
    private ClassDescription _currentDescription;

    private void Start()
    {
        _classManager = FindObjectOfType<ClassManager>();
        _currentClassText = FindObjectOfType<ClassButtonText>();
        _currentDescription = FindObjectOfType<ClassDescription>();
        _currentClassText.currentClassText.text = "Current Style: " + EStyleType.NONE;
    }

    public void OnButtonPress() 
    {
        // Depending on what button you click you get that class + a description about the class.
        _classManager.StyleChosen(Type);
        _currentClassText.currentClassText.text = "Current Style: " + Type;
        if (Type == EStyleType.Mantis)
        {
            _currentDescription.currentClassDescriptionText.text = "You have increased jump height";
        }
        if (Type == EStyleType.Sonic)
        {
            _currentDescription.currentClassDescriptionText.text = "You have increased movement speed";
        }
        if (Type == EStyleType.Hooker)
        {
            _currentDescription.currentClassDescriptionText.text = "Left click to deploy a hook, Hold left click to swing." +
            "While swinging you can use A and D to move, use S to extend the cable, Press Space to pull yourself towards the grapple";
        }
        if (Type == EStyleType.NONE)
        {
            _currentDescription.currentClassDescriptionText.text = "You have the normal value's, just a regular ball";
        }
    }
}
