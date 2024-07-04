using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconReference : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private EAbilities _ability;
    [SerializeField] private TMP_Text _Text;
    [SerializeField] private Image _Image;
    private void Start()
    {
        player = AbilityManager.Instance._player;
        switch (_ability)
        {
            case EAbilities.Teleport:
                player.GetComponent<Teleport>().SetIconStuff(_Image, _Text);
                break;
            case EAbilities.BigScream:
                player.GetComponent<BigScream>().SetIconStuff(_Image, _Text);
                break;
            case EAbilities.LifeSteal:
                player.GetComponent<LifeSteal>().SetIconStuff(_Image, _Text);
                break;
            case EAbilities.Metaform:
                player.GetComponent<Metaform>().SetIconStuff(_Image, _Text);
                break;
            default:
                break;
        }
    }
}
