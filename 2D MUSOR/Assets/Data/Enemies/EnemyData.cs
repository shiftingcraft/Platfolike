using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField][Min(0)] private int _health;
    [SerializeField][Min(0)] private int _armor;
    [SerializeField][Min(0)] private float _speed;

    [SerializeField][Min(0)] private int _dmg;
    [SerializeField][Min(0)] private float _triggerRadius;
    [SerializeField][Min(0)] private float _attackRange;
    [SerializeField][Min(0)] private float _attackDelay;
    [SerializeField][Min(0)] private float _patroolingDelay;
    [Tooltip("The time during which a player can take damage")]
    [SerializeField][Min(0)] private float _attackTime;
    [SerializeField] private EnemyState _state;

    public string Name => _name;
    public int Health => _health;
    public int Armor => _armor;
    public float Speed => _speed;
    public float TriggerRadius => _triggerRadius;
    public int Dmg => _dmg;
    public float AttackRange => _attackRange;
    public float AttackTime => _attackTime;
    public float AtackDelay => _attackDelay;
    public float PatroolingDelay => _patroolingDelay;


}
public enum EnemyState
{
    Idle, Patroling, Pursue, Attacking, Dead
}
