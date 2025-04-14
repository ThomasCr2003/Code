using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCodePaper : MonoBehaviour
{
    [SerializeField] private Material[] _Materials;
    private MeshRenderer _MRenderer;
    private bool _Transfering;


    private void Start()
    {
        _MRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchMaterial();
        }
    }

    public void SwitchMaterial()
    {
        //Switch To Shadow Material
        if (!_Transfering)
        {
            _MRenderer.material = _Materials[0];
            _Transfering = true;
        }
        else // Switch To Overworld Material
        {
            _MRenderer.material = _Materials[1];
        }
    }
}
