using System;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using Unity.Netcode;
using UnityEngine;

namespace Unity.BossRoom.Gameplay.GameplayObjects
{
    public class ManaReceiver : NetworkBehaviour, IManaable
    {
        public event Action<ServerCharacter, int> ManaReceived;

        [SerializeField]
        NetworkLifeState m_NetworkLifeState;

        public void ReceiveMana(ServerCharacter inflicter, int mana)
        {
            if (HasMana())
            {
                ManaReceived?.Invoke(inflicter, mana);
            }
        }

        public bool HasMana()
        {
            return m_NetworkLifeState.LifeState.Value == LifeState.Alive;
        }
    }
}
