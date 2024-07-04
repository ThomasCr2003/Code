using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Health
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] public float _maxHealth;

        [SerializeField] private Animator _Playeranimator;
        [SerializeField] private Animator _Enemyanimator;
        [SerializeField] private EnemyCircle _enemyCircle;
        
        public float _originalHealth;
        public float _currentHealth;

        [SerializeField] private Image Healthbar;

        public float Damage;
        private float _damagePlaceHolder;
        public float LifeStealChance { get; private set; }

        [SerializeField] private bool isPlayer = false;

        private bool isDead;

        private void Start(){
            _currentHealth = _maxHealth;
            if (Healthbar != null)
            {
                Healthbar.fillAmount = _currentHealth / _maxHealth;
            }

            if (!isPlayer)
            {
                _originalHealth = _currentHealth;
                _enemyCircle = GetComponentInChildren<EnemyCircle>();
            }

            _damagePlaceHolder = Damage;
        }



        public void TakeDamage(float damageAmount){
            _currentHealth -= damageAmount;

            if (Healthbar != null)
            {
                Healthbar.fillAmount = _currentHealth / _maxHealth;
            }

            if (!isPlayer)
            {
                float number = (_currentHealth / _originalHealth);
                _enemyCircle.UpdateEnemyCircle(number);
                if (_currentHealth <= 0)
                {
                    _currentHealth = 0;
                    if (_Enemyanimator != null)
                    {
                        _Enemyanimator.SetTrigger("death");
                        _Enemyanimator.SetBool("Attack", false);
                    }
                }
            }
            else
            {
                if (_currentHealth <= 0)
                {
                    _currentHealth = 0;
                    if (_Playeranimator != null && !isDead)
                    {
                        isDead = true;
                        _Playeranimator.SetTrigger("death");
                        _Playeranimator.SetBool("Attack", false);
                        DeathTransition.instance.FadeIn();
                    }
                }
            }
        }

        public void AddHealth(float amount){
            if (_currentHealth < _maxHealth)
            {
                _currentHealth += amount;
                if (Healthbar != null)
                {
                    Healthbar.fillAmount = _currentHealth / _maxHealth;
                }
            }
            else
            {
                return;
            }
        }

        public float GetDamageCurrentDamageNumber(){
            return Damage;
        }

        public void SetDamage(float amount){
            Damage += amount;
        }

        public float GetCurrentLifeStealChance(){
            return LifeStealChance;
        }

        /// <summary>
        /// Dont Set It Above 1!
        /// </summary>
        /// <param name="amount"></param>
        public void SetLifeSteal(float amount)
        {
            LifeStealChance = amount;
        }

        public void IncreaseMaximunHealth(float amount)
        {
            _maxHealth += amount;
            if (Healthbar != null)
            {
                Healthbar.fillAmount = _currentHealth / _maxHealth;
            }
        }

        public void ResetHealthSystem(){
            LifeStealChance = 0;
            Damage = _damagePlaceHolder;
            _currentHealth = 100;
            _maxHealth = 100;
        }

        public void SetHealth(float number)
        {
            _currentHealth = number;
            _originalHealth = number;
        }
    }
}