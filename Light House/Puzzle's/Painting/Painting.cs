using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buckets
{
    None,
    Red,
    Green,
    Pink,
    Purple,
    Yellow,
    Blue,
}

public class Painting : MonoBehaviour, Interactable
{

    [SerializeField] private Buckets CurrentBucket;
    [SerializeField] MeshRenderer NormalPainting, ShadowPainting;
    [SerializeField] private PaintingShadow PShadow;
    public bool IsHoldingBucket, CanStart;
    public Material[] MaterialsNormal, MaterialsShadow;

    public void SetCurrentBucketColor(Buckets Color)
    {
        CurrentBucket = Color;
        IsHoldingBucket = true;
    }


    public void Interact()
    {
        if (IsHoldingBucket && CanStart)
        {
            NormalPainting.material = MaterialsNormal[(int)CurrentBucket];
            ShadowPainting.material = MaterialsShadow[(int)CurrentBucket];
        }
        if (CurrentBucket == Buckets.Red)
        {
            PShadow.CutsceneCanPlay = true;
        }
        else
        {
            PShadow.CutsceneCanPlay = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            IsHoldingBucket = false;
        }
    }
}
