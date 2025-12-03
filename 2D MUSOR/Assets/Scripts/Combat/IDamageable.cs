using UnityEngine;

public interface IDamagable
{
    public int MaxHealth { get; set; }
    public int Damage { get; set; }

    public void Attack(IDamagable target, int dmg);
    public void GetDamage(int dmg);
}
