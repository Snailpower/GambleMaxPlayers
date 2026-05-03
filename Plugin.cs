using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace GambleMaxPlayers
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "com.gamblewithfriends.maxplayers";
        public const string PluginName = "GambleMaxPlayers";
        public const string PluginVersion = "1.0.0";

        internal static ConfigEntry<int> MaxPlayers;

        private void Awake()
        {
            MaxPlayers = Config.Bind(
                "General",
                "MaxPlayers",
                10,
                new ConfigDescription(
                    "Maximum number of players allowed in a lobby.",
                    new AcceptableValueRange<int>(2, 100)
                )
            );

            new Harmony(PluginGuid).PatchAll();
            Logger.LogInfo($"{PluginName} loaded — max players: {MaxPlayers.Value}");
        }
    }
}
