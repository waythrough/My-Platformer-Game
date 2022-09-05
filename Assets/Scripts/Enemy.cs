using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField] private int maxHP;
    private int currentHP;

    public void TakeDamage (int damage)
    {
        currentHP -= damage;

        if(currentHP <= 0)
        {
            Destroy(gameObject);
            Debug.Log("El enemigo ha muerto!");

            return;
        }

        Debug.Log("El enemigo :" + this + " has sufrido da;o");
    }

    private void Start ()
    {
        currentHP = maxHP;
    }
}
