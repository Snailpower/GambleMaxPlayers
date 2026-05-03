using HarmonyLib;
using Mirror;

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
}
