using UnityEngine;

[CreateAssetMenu(
    fileName = "SO_CharacterHealth_Data",
    menuName = "Game/HealthSystem/CharacterHealthData"
)]
public class CharacterHealthSO : ScriptableObject
{
    [field: SerializeField]
    public int MaxHealth { get; private set; }
}
