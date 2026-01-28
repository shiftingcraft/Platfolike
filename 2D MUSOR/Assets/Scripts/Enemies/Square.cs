using System.Xml.Serialization;
using UnityEngine;

public class Square : Enemy
{
    [SerializeField] private Transform[] _patrolingPoints;
    private int _patrolingIndex = 0;
    private bool _isMoving = false;
    private float _patroolingTime = 0f;
    public override void Attack(IDamagable target, int dmg)
    {
        target.GetDamage(dmg);
    }

    public override void GetDamage(int dmg)
    {
        Debug.Log("Àó÷");
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

    private void Update()
    {
        CheckState();
    }
    public override void CheckState()
    {
        var triggercast = Physics2D.CircleCast(transform.position, _enemyData.TriggerRadius, Vector2.zero, _enemyData.TriggerRadius, 1<<6);
        if (triggercast)
        {
            OnPursue(triggercast.transform);
        }
        else
        {
            OnPatrooling();
        }
            
    }
    public override void OnPursue(Transform target)
    {
        Vector3 currentPos = transform.position;
        transform.position = Vector3.MoveTowards(currentPos, target.position, _enemyData.Speed * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        Gizmos.DrawSphere(transform.position, _enemyData.TriggerRadius);
    }
    public override void OnPatrooling()
    {
        if (Vector3.Distance(transform.position, _patrolingPoints[_patrolingIndex].position) < 0.1f)
            _isMoving = false;
        else
            _isMoving = true;
        if (_isMoving)
        {
            Vector3 currentPos = transform.position;
            Vector3 nextPos = _patrolingPoints[(_patrolingIndex)].position;
            float distacnce = Vector3.Distance(currentPos, nextPos);
            
            transform.position = Vector3.MoveTowards(currentPos, nextPos, _enemyData.Speed * Time.deltaTime);
        }
        else
        {
            _patroolingTime += Time.deltaTime;
            if (_patroolingTime > _enemyData.PatroolingDelay)
            {
                _isMoving = true;
                _patroolingTime = 0f;
                _patrolingIndex = (_patrolingIndex + 1) % _patrolingPoints.Length;
            }
        }


    }

}
