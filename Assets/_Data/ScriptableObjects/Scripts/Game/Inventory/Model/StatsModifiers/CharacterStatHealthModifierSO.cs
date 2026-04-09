using UnityEngine;

namespace StatsModifiers
{
    [CreateAssetMenu(
        fileName = "SO_CharacterStatHealthModifier",
        menuName = "Game/Inventory/StatsModifiers/CharacterStatHealthModifier"
    )]
    public class CharacterStatHealthModifierSO : CharacterStatModifierSO
    {
        public override void AffectCharacter(GameObject character, float val)
        {
            // Xử lí thao tác thực hiện máu với nhân vật
        }

    }
}

