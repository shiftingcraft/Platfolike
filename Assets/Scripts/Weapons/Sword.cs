using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private PlayerCombatSystem _pcs;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.GetComponent<IDamagable>().GetDamage(_pcs.Damage); 
        }
    }
}
