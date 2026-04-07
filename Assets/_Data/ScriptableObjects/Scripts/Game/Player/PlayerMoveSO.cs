using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerMoveData",
    menuName = "Game/Player/PlayerMoveData"
)]
public class PlayerMoveSO : ScriptableObject
{
    [Header("Move")]
    public float MoveSpeed = 5f;
}
