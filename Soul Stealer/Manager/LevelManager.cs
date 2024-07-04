using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelManager : MonoBehaviour
{
    #region Instance

    public static LevelManager Instance;
    [SerializeField] private Animator animator; // just for testing

    #endregion

    public List<GameObject> levelPrefabs;
    public List<int> usedIndexes = new List<int>();

    [SerializeField] private GameObject currentLevel;

    private int GetRandomIndex()
    {
        int randomIndex = Random.Range(0, levelPrefabs.Count);

        // Controleer of randomIndex al is gebruikt, zoja, zoek een andere
        while (usedIndexes.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, levelPrefabs.Count);
        }

        // Voeg de index toe aan de gebruikte indexen
        usedIndexes.Add(randomIndex);

        // Als alle levels zijn gebruikt, reset de lijst van gebruikte indexen
        if (usedIndexes.Count >= levelPrefabs.Count)
        {
            usedIndexes.Clear();
        }

        return randomIndex;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            return;
        }
        else
        {
            Instance = this;
        }

        animator.SetTrigger("StartFade");
    }

    private void Start() {
        DeathTransition.instance.FadeOut();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // animator.SetTrigger("StartFade");
        }
    }

    public void StartFadeAnimation()
    {
        // animator.SetTrigger("StartFade");
    }
}