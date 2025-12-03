using System.Xml.Serialization;
using UnityEngine;

public class Square : Enemy
{
    public override int MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    public override int Damage { get => _dmg; set => _dmg = value; }


    public override void Attack(IDamagable target, int dmg)
    {
        target.GetDamage(Damage);
    }


    public override void GetDamage(int dmg)
    {
        Debug.Log("јуч");
        _hp -= dmg;
            if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.GetComponent<IDamagable>().GetDamage(_dmg);
        }
            
    }
}
