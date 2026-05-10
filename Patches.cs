using HarmonyLib;
using Mirror;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace GambleMaxPlayers
{
    // Patch NetworkServer.Listen directly — this is what Mirror calls internally to enforce
    // the connection limit, regardless of which NetworkManager subclass starts the server.
    [HarmonyPatch(typeof(NetworkServer), nameof(NetworkServer.Listen))]
    internal static class NetworkServerListenPatch
    {
        static void Prefix(ref int maxConns)
        {
            maxConns = Plugin.MaxPlayers.Value;
            Plugin.Log.LogInfo($"NetworkServer.Listen patch fired — set maxConns to {maxConns}");
        }
    }

    // Players above 6 get stuck in box state on spawn. Force-wake them server-side after
    // AssignPlayer runs, since the base game only reliably wakes the first 6 slots.
    [HarmonyPatch(typeof(SpawnBoxPlayerRagdollTrigger), "AssignPlayer")]
    internal static class SpawnWakeFixPatch
    {
        [HarmonyPostfix]
        private static void AssignPlayerPatch(PlayerController player)
        {
            if (player == null || !NetworkServer.active)
                return;

            player.StartCoroutine(FinalWakeRoutine(player));
        }

        private static IEnumerator FinalWakeRoutine(PlayerController pc)
        {
            yield return new WaitForSeconds(0.5f);

            if (pc == null)
                yield break;

            SafeInvoke(pc, "ServerLock", false);
            pc.LocalLock(false);
            SafeInvoke(pc, "ServerWakeUp");

            yield return new WaitForSeconds(0.5f);

            SafeInvoke(pc, "ServerLock", false);
            pc.LocalLock(false);
        }

        private static void SafeInvoke(object obj, string methodName, params object[] args)
        {
            MethodInfo method = obj.GetType().GetMethod(
                methodName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            );
            method?.Invoke(obj, args);
        }
    }
}
