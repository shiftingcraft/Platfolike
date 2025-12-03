using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _dmg;
    protected int _hp;

    public abstract int MaxHealth { get; set; }

    public abstract int Damage { get; set; }

    public abstract void Attack(IDamagable target, int dmg);

    public abstract void GetDamage(int dmg);
    private void Awake()
    {
        _hp = _maxHealth;
    }

}
