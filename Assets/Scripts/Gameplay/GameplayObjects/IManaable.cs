using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using UnityEngine;

namespace Unity.BossRoom.Gameplay.GameplayObjects
{
    public interface IManaable
    {
        /// <summary>
        /// Receives HP damage or healing.
        /// </summary>
        /// <param name="inflicter">The Character responsible for the damage. May be null.</param>
        /// <param name="HP">The damage done. Negative value is damage, positive is healing.</param>
        void ReceiveMana(ServerCharacter inflicter, int mana);

        /// <summary>
        /// The NetworkId of this object.
        /// </summary>
        ulong NetworkObjectId { get; }

        /// <summary>
        /// The transform of this object.
        /// </summary>
        Transform transform { get; }

        /// <summary>
        /// Are we still able to take damage? If we're broken or dead, should return false!
        /// </summary>
        bool HasMana();
    }
}
