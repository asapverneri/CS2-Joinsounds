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
    public override string ModuleVersion => "1.2";

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
        if (@event == null) return HookResult.Continue;
        var player = @event.Userid;

        Playjoinsound(player);

        return HookResult.Continue;
    }

    public void Playjoinsound(CCSPlayerController? player)
    {
        if (player == null || player.IsHLTV || player.IsBot || !player.PlayerPawn.IsValid)
            return;

        string? soundToPlay = null;

        foreach (var setting in Config.SoundSettings)
        {
            if (!string.IsNullOrEmpty(setting.SteamID64) &&
                player.SteamID.ToString() == setting.SteamID64)
            {
                soundToPlay = setting.Sound;
                break;
            }

            if (!string.IsNullOrEmpty(setting.Flag) &&
                AdminManager.PlayerHasPermissions(player, setting.Flag))
            {
                soundToPlay = setting.Sound;
                break;
            }

            if (string.IsNullOrEmpty(setting.SteamID64) && string.IsNullOrEmpty(setting.Flag))
            {
                soundToPlay = setting.Sound;
            }
        }

        if (soundToPlay == null)
            return;

        foreach (var allPlayer in Utilities.GetPlayers().Where(p => !p.IsHLTV && !p.IsBot))
        {
            allPlayer.ExecuteClientCommand("play " + soundToPlay);
        }
    }

}