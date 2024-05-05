using Content.Server.Stunnable.Systems;
using Content.Shared.Timing;
using Robust.Shared.Audio;

namespace Content.Server.Telebaton
{
  [RegisterComponent, Access(typeof(TelebatonSystem))]
  public sealed partial class TelebatonComponent : Component 
  {
    public bool Activated { get; set; } = false;
    [DataField("onThrowStunChance")]
    public float onThrowStunChance { get; set; } = 0.7f;

    [DataField("stunSound")]
    public SoundSpecifier stunsound { get; set; } = new SoundPathSpecifier("/Audio/Weapons/Telebaton/bonk-sound-effect.ogg");

    [DataField("extendSound")]
    public SoundSpecifier extendsound { get; set; } = new SoundPathSpecifier("/Audio/Weapons/Telebaton/batonextend.ogg");
  }
}
