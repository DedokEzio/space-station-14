using Content.Server.Stunnable.Components;
using Content.Shared.Audio;
using Content.Shared.Damage.Events;
using Content.Shared.Damage.Components;
using Content.Shared.Examine;
using Content.Shared.Interaction.Events;
using Content.Shared.Item;
using Content.Shared.Popups;
using Content.Shared.Toggleable;
using Content.Shared.Weapons.Melee.Events;
using Robust.Server.GameObjects;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Player;

namespace Content.Server.Imperial.Telebaton
{
    public sealed partial class TelebatonSystem : EntitySystem
    {
        [Dependency] private readonly SharedItemSystem _item = default!;
        [Dependency] private readonly SharedAppearanceSystem _appearance = default!;
        [Dependency] private readonly SharedAudioSystem _audio = default!;

        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<TelebatonComponent, UseInHandEvent>(OnUseInHand);
            SubscribeLocalEvent<TelebatonComponent, ExaminedEvent>(OnExamined);
            SubscribeLocalEvent<TelebatonComponent, MeleeHitEvent>(OnMeleeHit);
        }

        private void OnUseInHand(EntityUid uid, TelebatonComponent component, UseInHandEvent arguments)
        {
            if (component.Activated)
            {
                RetractTelebaton(uid, component);
            }
            else
            {
                ExtendTelebaton(uid, component, arguments.User);
            }
        }

        private void RetractTelebaton(EntityUid uid, TelebatonComponent component)
        {
            if (!component.Activated)
                return;

            if (TryComp<AppearanceComponent>(component.Owner, out var appearance) && TryComp<ItemComponent>(component.Owner, out _))
            {
                _item.SetHeldPrefix(component.Owner, "retracted");
                _appearance.SetData(uid, ToggleVisuals.Toggled, false, appearance);
            }

            _audio.PlayPvs(component.extendsound, uid);
            component.Activated = false; 
        }

        private void ExtendTelebaton(EntityUid uid, TelebatonComponent component, EntityUid user)
        {
            if (component.Activated)
                return;

            if (TryComp<AppearanceComponent>(component.Owner, out var appearance) && TryComp<ItemComponent>(component.Owner, out _))
            {
                _item.SetHeldPrefix(component.Owner, "extended");
                _appearance.SetData(uid, ToggleVisuals.Toggled, true, appearance);
            }

            _audio.PlayPvs(component.extendsound, uid);
            component.Activated = true;
        }

        private void OnExamined(EntityUid uid, TelebatonComponent component, ExaminedEvent args)
        {
            var msg = component.Activated
                ? Loc.GetString("melee-telebaton-examined-extended")
                : Loc.GetString("melee-telebaton-examined-retracted");
            args.PushMarkup(msg);
        }

        private void OnMeleeHit(EntityUid uid, TelebatonComponent component, MeleeHitEvent args)
        {
            if (!component.Activated)
                return;
            args.BonusDamage -= args.BaseDamage;
        }
    }
}
