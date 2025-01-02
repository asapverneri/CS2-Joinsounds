using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Admin;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Joinsounds;

[MinimumApiVersion(290)]
public class JoinsoundsConfig : BasePluginConfig
{
    [JsonPropertyName("SoundSettings")]
    public List<SoundSetting> SoundSettings { get; set; } = new List<SoundSetting>
    {
        new SoundSetting
        {
            SteamID64 = "76561198134597930",
            Sound = "sounds/training/timer_bell"
        },
        new SoundSetting
        {
            Flag = "@css/admin",
            Sound = "sounds/training/timer_bell"
        },
        new SoundSetting
        {
            Flag = "@css/vip",
            Sound = "sounds/training/timer_bell"
        },
        new SoundSetting
        {
            Flag = "@css/vip",
            Sound = "sounds/training/timer_bell"
        }
    };
}

public class SoundSetting
{
    [JsonPropertyName("Flag")]
    public string Flag { get; set; } = string.Empty;

    [JsonPropertyName("SteamID64")]
    public string SteamID64 { get; set; } = string.Empty;

    [JsonPropertyName("Sound")]
    public string Sound { get; set; } = string.Empty;
}
public class Joinsounds : BasePlugin, IPluginConfig<JoinsoundsConfig>
{
    public override string ModuleName => "Joinsounds";
    public override string ModuleDescription => "Notification VIP/ADMIN join sound for cs2";
    public override string ModuleAuthor => "verneri";
    public override string ModuleVersion => "1.0";

    public JoinsoundsConfig Config { get; set; } = new();

    public void OnConfigParsed(JoinsoundsConfig config)
    {
        Config = config;
    }
    public override void Load(bool hotReload)
    {
        Logger.LogInformation($"loaded successfully! (Version {ModuleVersion})");
    }

    [GameEventHandler(HookMode.Pre)]
    public HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
    {
        if (@event == null) return HookResult.Handled;
        var player = @event.Userid;

        Playjoinsound();

        return HookResult.Handled;
    }

    public void Playjoinsound()
    {
        foreach (var player in Utilities.GetPlayers().Where(p => !p.IsHLTV && !p.IsBot && p.PlayerPawn.IsValid))
        {
            foreach (var setting in Config.SoundSettings)
            {
                if (!string.IsNullOrEmpty(setting.Flag) && AdminManager.PlayerHasPermissions(player, setting.Flag))
                {
                    foreach (var allPlayer in Utilities.GetPlayers().Where(p => !p.IsHLTV && !p.IsBot && p.PlayerPawn.IsValid))
                    {
                        allPlayer.ExecuteClientCommand($"play {setting.Sound}");
                    }
                    break;
                }
                else if (!string.IsNullOrEmpty(setting.SteamID64) && player.SteamID.ToString() == setting.SteamID64)
                {
                    foreach (var allPlayer in Utilities.GetPlayers().Where(p => !p.IsHLTV && !p.IsBot && p.PlayerPawn.IsValid))
                    {
                        allPlayer.ExecuteClientCommand($"play {setting.Sound}");
                    }
                    break;
                }
            }
        }
    }

}