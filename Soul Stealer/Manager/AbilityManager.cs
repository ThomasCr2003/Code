using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EAbilities
{
    Teleport,
    BigScream,
    LifeSteal,
    Metaform,
}

public enum EStatChanges
{
    Range,
    Damage,
    HealthIncrease,
}

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }
    [Header("Player")]
    public GameObject _player;

    [Header("Cards + Card Positions")]
    public List<AbilityCard> Cards = new();
    public List<AbilityCard> ChoosingCards = new();
    public List<Transform> CardPositions = new();
    public List<StatChange> StatCards = new();
    public Transform Parent;

    [Header("Ability Placement")]
    public List<Transform> AbilityPlacements = new();
    private int AbilityPlaceIndex;

    public bool CardHasBeenPicked;
    public bool AllAbilitiesChosen;
    public bool IsScreaming;
    private bool _isFirstAbility = true;
    private bool _isFirstStatChange = true;
    public bool CanUseAbility;
    public List<IAbility> ActivatedAbilities { get; set; } = new();
    private KeyCode[] _keys = new[] {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
    };

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
        }
    }


    private void Update()
    {
        int index = -1;
        for (int i = 0; i < _keys.Length; i++)
        {
            if (Input.GetKeyDown(_keys[i]))
            {
                index = i;
                break;
            }
        }
        if (index > -1 && index < ActivatedAbilities.Count && !IsScreaming && CanUseAbility)
        {
            ActivatedAbilities[index].Activate();
        }
    }

    public void AddAbility(EAbilities pability)
    {
        IAbility ability = ConvertEAbility(pability);
        if (ability != null)
        {
            ActivatedAbilities.Add(ability);
            Debug.Log("Added " + ability.ToString() + " ability");
        }
    }

    public IAbility ConvertEAbility(EAbilities eAbility)
    {
        switch (eAbility)
        {
            case EAbilities.Teleport:
                return _player.GetComponent<Teleport>();

            case EAbilities.BigScream:
                return _player.GetComponent<BigScream>();

            case EAbilities.LifeSteal:
                return _player.GetComponent<LifeSteal>();

            case EAbilities.Metaform:
                return _player.GetComponent<Metaform>();
            default:
                return null;
        }
    }

    public bool HasAbility(IAbility pability)
    {
        return ActivatedAbilities.Contains(pability);
    }

    public void SetPlayer(GameObject Pplayer)
    {
        _player = Pplayer;
    }

    public void ChooseAbilities()
    {
        CardHasBeenPicked = false;
        if (_isFirstAbility)
        {
            TutorialManager.instance.DisplayAbilityChoosingText();
            _isFirstAbility = false;
        }
        List<AbilityCard> CardOptions = new List<AbilityCard>(Cards);

        for (int i = CardOptions.Count - 1; i >= 0; i--)
        {
            IAbility Abil = ConvertEAbility(CardOptions[i].GetAbility());

            for (int j = 0; j < ActivatedAbilities.Count; j++)
            {
                if (ActivatedAbilities[j] == Abil)
                {
                    CardOptions.RemoveAt(i);
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (CardOptions.Count != 0)
            {
                int random = Random.Range(0, CardOptions.Count);

                Debug.Log("Added Ability That isnt chosen yet");
                ChoosingCards.Add(CardOptions[random]);
                CardOptions.RemoveAt(random);
                Instantiate(ChoosingCards[i], CardPositions[i].position, transform.rotation, Parent);
            }
            else
            {
                return;
            }
        }
    }

    public void ChooseStatChange()
    {
        CardHasBeenPicked = false;

        if (_isFirstStatChange)
        {
            TutorialManager.instance.DisplayStatChangeText();
            _isFirstStatChange = false;
        }

        for (int i = 0; i < 3; i++)
        {
            Instantiate(StatCards[i], CardPositions[i].position, transform.rotation, Parent);
        }
    }

    public int GetCurrentPlaceIndex()
    {
        return AbilityPlaceIndex;
    }

    public void AddCurrentPlaceIndex(int amount) 
    {
        AbilityPlaceIndex += amount;
    }

    public void ResetAbilities()
    {
        ActivatedAbilities.Clear();
    }
}
