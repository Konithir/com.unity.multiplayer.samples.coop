using System.Collections;
using System.Collections.Generic;
using Unity.BossRoom.Gameplay.GameplayObjects;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using UnityEngine;

namespace Unity.BossRoom.Gameplay.Actions
{
    /// <summary>
    /// A supportinve action where the character recharges his own mana and players around him.
    /// </summary>
    /// <remarks>
    /// Mana Aura works only in specified range and for limited amount of time.
    /// </remarks>
    [CreateAssetMenu(menuName = "BossRoom/Actions/Mana Aura Action")]
    public class ManaAura : Action
    {
        private float _spellStart;
        private float _lastAuraInterval;

        public override bool OnStart(ServerCharacter serverCharacter)
        {
            serverCharacter.serverAnimationHandler.NetworkAnimator.ResetTrigger(Config.Anim2);

            // raise the start trigger to start the animation loop!
            serverCharacter.serverAnimationHandler.NetworkAnimator.SetTrigger(Config.Anim);

            serverCharacter.clientCharacter.RecvDoActionClientRPC(Data);

            _spellStart = Time.time;
            _lastAuraInterval = Time.time;

            return true;
        }

        public override bool OnUpdate(ServerCharacter clientCharacter)
        {
            if(_lastAuraInterval + Config.ExecTimeSeconds <= Time.time)
            {
                _lastAuraInterval = Time.time;
                RegenManaInArea(clientCharacter);
            }

            if(_spellStart + Config.DurationSeconds <= Time.time)
            {
                return false;
            }

            return true;
        }

        private void RegenManaInArea(ServerCharacter clientCharacter)
        {
            Collider[] hitColliders = ActionUtils.DetectAllEntitiesInRange(true, false, clientCharacter.physicsWrapper.DamageCollider, Config.Radius);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                RegenMana(clientCharacter, hitColliders[i]);
            }
        }

        private void RegenMana(ServerCharacter clientCharacter, Collider collider)
        {
            var manaOwner = collider.GetComponent<IManaable>();

            if(manaOwner != null)
            {
                manaOwner.ReceiveMana(clientCharacter, Config.Amount);
            }
        }

        public override void Reset()
        {
            base.Reset();
            _spellStart = 0;
            _lastAuraInterval = 0;
    }
    }
}
