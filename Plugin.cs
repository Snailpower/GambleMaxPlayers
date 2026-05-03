using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace GambleMaxPlayers
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "com.gamblewithfriends.maxplayers";
        public const string PluginName = "GambleMaxPlayers";
        public const string PluginVersion = "1.0.0";

        internal static ConfigEntry<int> MaxPlayers;
        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;

            MaxPlayers = Config.Bind(
                "General",
                "MaxPlayers",
                10,
                new ConfigDescription(
                    "Maximum number of players allowed in a lobby.",
                    new AcceptableValueRange<int>(2, 100)
                )
            );

            // Set maxPlayers on the LobbySettings resource before any game code runs.
            // Resources.Load caches the instance, so all subsequent reads return our modified value
            // regardless of which code path creates the Steam lobby.
            var lobbySettings = Resources.Load<LobbySettings>("LobbySettings");
            if (lobbySettings != null)
            {
                lobbySettings.maxPlayers = MaxPlayers.Value;
                Logger.LogInfo($"Set LobbySettings.maxPlayers to {lobbySettings.maxPlayers}");
            }
            else
            {
                Logger.LogWarning("LobbySettings resource not found — lobby size NOT changed.");
            }

            new Harmony(PluginGuid).PatchAll();
            Logger.LogInfo($"{PluginName} loaded — max players: {MaxPlayers.Value}");
        }
    }
}
