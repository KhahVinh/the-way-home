using Inventory.Model;
using UnityEngine;

public class PlayerChop : MonoBehaviour
{
    public float range = 1.5f;
    public LayerMask detectLayer;
    public PlayerMovement _playerMoment;

    [SerializeField]
    private AgentWeapon _agentWeapon;
    [SerializeField]
    private ItemParameter _parameter;

    public void Chop()
    {
        Vector2 dir = new Vector2();
        if (_playerMoment.LastMoveDir != Vector2.zero)
        {
            dir.x = _playerMoment.LastMoveDir.x;
            dir.y = _playerMoment.LastMoveDir.y;
        }
        // Xử lí phá hủy đồ vật bằng raycast
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            dir,
            range,
            detectLayer
        );

        // Xử lí chặt cây, phá hủy item
        if (hit.collider != null)
        {
            hit.collider.GetComponent<ItemDetect>()?.TakeDamage((int)_agentWeapon.GetValueOfParameter(_parameter));
        }
    }

}
