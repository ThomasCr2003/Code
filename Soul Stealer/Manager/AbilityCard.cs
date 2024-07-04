using UnityEngine;

public class AbilityCard : MonoBehaviour
{
    [SerializeField] private EAbilities _Ability;
    [SerializeField] private GameObject _AbilityIcon;   
    private void Update()
    {
        if (AbilityManager.Instance.CardHasBeenPicked)
        {
            LevelUp.Instance.Continue();
            Destroy(gameObject);
        }
    }

    public void OnPress()
    {
        AbilityManager abilityManager = AbilityManager.Instance;
        abilityManager.CardHasBeenPicked = true;
        abilityManager.ChoosingCards.Clear();
        Instantiate(_AbilityIcon, abilityManager.AbilityPlacements[abilityManager.GetCurrentPlaceIndex()].position, transform.rotation, abilityManager.Parent);
        switch (_Ability)
        {
            case EAbilities.Teleport:
                abilityManager.AddAbility(EAbilities.Teleport);
                break;
            case EAbilities.BigScream:
                abilityManager.AddAbility(EAbilities.BigScream);
                break;
            case EAbilities.LifeSteal:
                abilityManager.AddAbility(EAbilities.LifeSteal);
                break;
            case EAbilities.Metaform:
                abilityManager.AddAbility(EAbilities.Metaform);
                break;
            default:
                break;
        }
        abilityManager.AddCurrentPlaceIndex(1);
        if (abilityManager.ActivatedAbilities.Count == abilityManager.Cards.Count)
        {
            abilityManager.AllAbilitiesChosen = true;
        }
    }

    public EAbilities GetAbility()
    {
        return _Ability;
    }
}
