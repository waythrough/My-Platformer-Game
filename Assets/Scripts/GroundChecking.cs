using UnityEngine;
using System;

public class GroundChecking : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float extraHeight = 0.02f;

    public bool isGrounded => _isGrounded;
    private bool _isGrounded = true;

    private void OnDrawGizmos () {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center, boxCollider2D.bounds.size);
    }

    private RaycastHit2D GetRaycastHit2D()
    {
        return Physics2D.BoxCast(origin: boxCollider2D.bounds.center + (Vector3.down * extraHeight), size: boxCollider2D.bounds.size, angle: 0f, direction: Vector2.down, distance: 0f, layerMask: groundLayerMask);
    }

    private void Update()
    {
        RaycastHit2D raycastHit2D = GetRaycastHit2D();

        if (raycastHit2D.collider == null)
        {
            if (_isGrounded == false)
                return;

            _isGrounded = false;
        }

        if (raycastHit2D.collider != null)
        {
            if (_isGrounded == true)
                return;

            _isGrounded = true;
        }
    }
}
