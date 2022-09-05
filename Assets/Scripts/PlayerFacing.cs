using UnityEngine;

// Se encarga de decidir, hacia donde mira el jugador
public class PlayerFacing : MonoBehaviour
{
    public int direction => _direction;
    private int _direction = 1; // Vale 1, si mira a la derecha, y -1, si mira hacia la izquierda

    private void Update ()
    {
        int value = (int)Input.GetAxisRaw("Horizontal");

        if(value != 0)
        {
            _direction = value;
            transform.localScale = new Vector3(value, 1, 1);
        }
    }
}
