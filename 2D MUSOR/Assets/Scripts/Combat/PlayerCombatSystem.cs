using UnityEngine;

public class PlayerCombatSystem : MonoBehaviour, IDamagable
{
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int Damage { get => _dmg; set => _dmg = value; }

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _dmg;
    private int _hp;

    private void Awake()
    {
        _hp = MaxHealth;
    }

    public void Attack(int dmg)
    {
        throw new System.NotImplementedException();
    }

    public void GetDamage(int dmg)
    {
        _hp -= _dmg;
        if (_hp <= 0)
            Debug.Log("Player is baba");
    }
}
