using Content.Shared.Damage;

namespace Content.Shared.Backmen.Abilities.Psionics
{
    /// <summary>
    /// Takes damage when dispelled.
    /// </summary>
    [RegisterComponent]
    public sealed partial class DamageOnDispelComponent : Component
    {
        [DataField("damage", required: true), ViewVariables(VVAccess.ReadWrite)]
        public DamageSpecifier  Damage = default!;

        [DataField("variance")]
        public float Variance = 0.5f;
    }
}
