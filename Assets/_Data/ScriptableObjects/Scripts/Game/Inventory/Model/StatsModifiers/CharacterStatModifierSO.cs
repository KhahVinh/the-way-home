
using UnityEngine;

namespace StatsModifiers
{
    public abstract class CharacterStatModifierSO : ScriptableObject
    {
        public abstract void AffectCharacter(GameObject character, float val);
    }
}

