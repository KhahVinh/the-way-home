using UnityEngine;

public class PlayerChop : MonoBehaviour
{
    public float range = 1.5f;
    public LayerMask treeLayer;
    public int damage = 1;
    public PlayerMovement _playerMoment;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Chop();
        }
    }

    private void Chop()
    {
        if (_playerMoment.LastMoveDir != Vector2.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                _playerMoment.LastMoveDir,
                range,
                treeLayer
            );

            if (hit.collider != null)
            {
                hit.collider.GetComponent<ItemDetect>()?.TakeDamage(damage);
            }
        }
    }

}
