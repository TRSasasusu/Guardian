using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian {
    [HarmonyPatch]
    public static class Patch {
        public static bool _canBeDamaged = true;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(DeathManager), nameof(DeathManager.KillPlayer))]
        public static bool DeathManager_KillPlayer_Prefix(DeathType deathType) {
            if (deathType == DeathType.Meditation) {
                return true;
            }
            if (_canBeDamaged) {
                return true;
            }
            return false;
        }
    }
}
