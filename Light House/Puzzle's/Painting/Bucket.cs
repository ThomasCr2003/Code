using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour, Interactable
{
    public Buckets Color;
    public Painting p;

    public void Interact()
    {
        p.SetCurrentBucketColor(Color);
    }

}
