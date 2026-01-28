using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected EnemyData _enemyData;

    protected int _hp;
    protected int _dmg;
    protected float _currentSpeed;
    protected EnemyState _state;

    public abstract void Attack(IDamagable target, int dmg);

    public abstract void GetDamage(int dmg);
    private void Awake()
    {
        _hp = _enemyData.Health;
        _dmg = _enemyData.Dmg;
        _currentSpeed = _enemyData.Speed;
        _state = EnemyState.Idle;
    }
    public virtual void CheckState() { }
    public virtual void OnIdle() { }
    public virtual void OnPatrooling() { }
    public virtual void OnPursue(Transform target) { }
    public virtual void OnAttacking() { }
    public virtual void OnDeath() { }

}
