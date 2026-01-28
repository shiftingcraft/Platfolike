using UnityEngine;

public interface IDamagable
{

    public void Attack(IDamagable target, int dmg);
    public void GetDamage(int dmg);
}
