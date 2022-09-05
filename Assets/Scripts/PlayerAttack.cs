using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerFacing playerFacing;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private int damage = 5;
    [SerializeField] private float cooldown = 0.25f; // Tiempo de enfriamiento del ataque (el tiempo entre ataquess)

    private Coroutine attacking;

    private IEnumerator Attacking ()
    {
        // Desplazamiento de la posicion de la caja
        Vector3 offset = Vector3.right * playerFacing.direction;

        // Devuelve todos los colisionadores enemigos dentro de una caja
        Collider2D[] colliders = Physics2D.OverlapBoxAll(point: transform.position + offset, size: Vector2.one, angle: 0f, layerMask: enemyLayerMask);

        // Para cada colisionador enemigo en colisionadores enemigos
        foreach (Collider2D collider in colliders)
        {
            // Atacar al colisionador enemigo actual
            Attack(target: collider.transform);
        }

        // Esperar el tiempo de enfriamento del ataque
        yield return new WaitForSeconds(cooldown);

        // Retirar la referencia de la corrutina almacenada en 'attacking'
        attacking = null;
    }

    private void Attack(Transform target)
    {
        // Obtener la interfaz IDamage del objetivo
        IDamage damage = target.GetComponent<IDamage>();

        // Si el objetivo no puede ser da;ado
        if(damage == null)
        {
            Debug.Log("El objetivo, no puede ser da;ado");
        }

        // Si el objetivo puede ser da;ado
        if(damage != null)
        {
            damage.TakeDamage(this.damage);
            Debug.Log("Se ha efectuado un ataque exitosamente!");
        }
    }

    private void Update()
    {
        // Si se desea atacar, y no hay ninguna corrutina almacenada en 'attacking'
        if (Input.GetMouseButtonDown(0) && attacking == null)
        {
            // Iniciar y guardar la corrutina en 'attacking'
            attacking = StartCoroutine(Attacking());
        }
    }
}

public interface IDamage
{
    void TakeDamage(int damage);
}