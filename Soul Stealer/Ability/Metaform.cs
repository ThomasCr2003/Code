using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Metaform : MonoBehaviour, IAbility
{
    [SerializeField] private float _CooldownTimer;
    [SerializeField] private float _Duration;
    private bool _isOnCoolDown;

    public GameObject Model;
    public GameObject[] EnemyModel;
    [SerializeField] private ParticleSystem _TransitionParticle;
    [SerializeField] private PlayerScream _PlayerScream;
    public bool isTransformed;

    //CooldownStuff
    [SerializeField] private Image _Fade;
    [SerializeField] private TMP_Text _CooldownText;
    private float _cooldownTime;

    private void Start()
    {
        _PlayerScream = GetComponent<PlayerScream>();
    }

    private void Update()
    {
        if (_isOnCoolDown)
        {
            ApplyCooldown();
        }
        while (isTransformed)
        {
            _PlayerScream.SetCanScream(false);
            _PlayerScream.SetScreamindicatorOff();
            break;
        }
    }

    public void Activate()
    {
        if (!_isOnCoolDown)
        {
            _TransitionParticle.Play();
            _isOnCoolDown = true;
            gameObject.tag = "Untagged";
            isTransformed = true;
            StartCoroutine(MetaformIntoEnemyModel(_Duration));
            _CooldownText.gameObject.SetActive(true);
            _cooldownTime = _CooldownTimer;
            _CooldownText.text = Mathf.RoundToInt(_cooldownTime).ToString();
            _Fade.fillAmount = 1.0f;

        }
        else
        {
            Debug.Log("Ability is Not Ready Yet.");
        }
    }

    public void ApplyCooldown()
    {
        _cooldownTime -= Time.deltaTime;
        if (_cooldownTime < 0.0f)
        {
            _isOnCoolDown = false;
            _CooldownText.gameObject.SetActive(false);
            _Fade.fillAmount = 0.0f;
        }
        else
        {
            _CooldownText.text = Mathf.RoundToInt(_cooldownTime).ToString();
            _Fade.fillAmount = _cooldownTime / _CooldownTimer;

        }
    }

    public IEnumerator MetaformIntoEnemyModel(float duration)
    {
        yield return new WaitForSeconds(0.2f);
        _PlayerScream.SetCanScream(false);
        _PlayerScream.SetScreamindicatorOff();
        int TransformInt = Random.Range(0, EnemyModel.Length);
        Model.SetActive(false);
        EnemyModel[TransformInt].SetActive(true);
        yield return new WaitForSeconds(duration);
        isTransformed = false;
        _TransitionParticle.Play();
        yield return new WaitForSeconds(0.2f);
        _PlayerScream.SetCanScream(true);
        Model.SetActive(true);
        EnemyModel[TransformInt].SetActive(false);
        gameObject.tag = "Player";
    }

    public void SetIconStuff(Image fade, TMP_Text text)
    {
        _Fade = fade;
        _CooldownText = text;
    }
}
