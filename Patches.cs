using System.Reflection;
using HarmonyLib;
using Mirror;

namespace GambleMaxPlayers
{
    // Patch the Steam lobby creation to use our configured max player count.
    // LobbyManager.CreateNewLobby() calls SteamMatchmaking.CreateLobby(..., lobbySettings.maxPlayers),
    // so we update lobbySettings.maxPlayers right before that call.
    [HarmonyPatch(typeof(LobbyManager), "CreateNewLobby")]
    internal static class CreateNewLobbyPatch
    {
        private static readonly FieldInfo LobbySettingsField =
            typeof(LobbyManager).GetField("lobbySettings", BindingFlags.NonPublic | BindingFlags.Instance);

        static void Prefix(LobbyManager __instance)
        {
            if (LobbySettingsField?.GetValue(__instance) is LobbySettings settings)
                settings.maxPlayers = Plugin.MaxPlayers.Value;
        }
    }

    // Patch Mirror's server startup so it accepts up to MaxPlayers connections
    // before the transport begins listening.
    [HarmonyPatch(typeof(NetworkManager), "StartServer")]
    internal static class NetworkManagerStartServerPatch
    {
        static void Prefix(NetworkManager __instance)
        {
            __instance.maxConnections = Plugin.MaxPlayers.Value;
        }
    }
}
