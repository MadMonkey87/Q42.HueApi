using Q42.HueApi.Models;
using Q42.HueApi.Models.Groups;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Q42.HueApi
{
  /// <summary>
  /// Status data returned from the bridge
  /// </summary>
  internal class BridgeState
  {
    public Dictionary<string, Light> Lights { get; set; }
    public Dictionary<string, Group> Groups { get; set; }
    public BridgeConfig Config { get; set; }
    public Dictionary<string, WhiteList> Whitelist { get; set; }
    public Dictionary<string, Sensor> Sensors { get; set; }
    public Dictionary<string, Schedule> Schedules { get; set; }
    public Dictionary<string, Scene> Scenes { get; set; }
    public Dictionary<string, Rule> Rules { get; set; }
  }

  /// <summary>
  /// Hue Bridge
  /// </summary>
  public class Bridge
  {
    internal Bridge(BridgeState bridge)
    {
      if (bridge == null)
        throw new ArgumentNullException(nameof(bridge));

      Config = bridge.Config;

      foreach (var light in bridge.Lights)
      {
        light.Value.Id = light.Key;
      }
      Lights = bridge.Lights.Select(l => l.Value).ToList();

      foreach (var sensor in bridge.Sensors)
      {
        sensor.Value.Id = sensor.Key;
      }
      Sensors = bridge.Sensors.Select(s => s.Value).ToList();

      foreach (var group in bridge.Groups)
      {
        group.Value.Id = group.Key;
      }
      Groups = bridge.Groups.Select(l => l.Value).ToList();

      foreach (var scene in bridge.Scenes)
      {
        scene.Value.Id = scene.Key;
      }
      Scenes = bridge.Scenes.Select(s => s.Value).ToList();

      foreach (var rule in bridge.Rules)
      {
        rule.Value.Id = rule.Key;
      }
      Rules = bridge.Rules.Select(r => r.Value).ToList();

      foreach (var schedule in bridge.Schedules)
      {
        schedule.Value.Id = schedule.Key;
      }
      Schedules = bridge.Schedules.Select(s => s.Value).ToList();

      foreach (var whitelist in bridge.Config.WhiteList)
      {
        whitelist.Value.Id = whitelist.Key;
      }
      WhiteList = bridge.Config.WhiteList.Select(l => l.Value).ToList();
    }

    /// <summary>
    /// Light info from the bridge
    /// </summary>
    public IEnumerable<Light> Lights { get; private set; }

    /// <summary>
    /// Group info from the bridge
    /// </summary>
    public IEnumerable<Group> Groups { get; private set; }

    /// <summary>
    /// Bridge config info
    /// </summary>
    public BridgeConfig Config { get; private set; }

    /// <summary>
    /// Light info from the bridge
    /// </summary>
    public IEnumerable<WhiteList> WhiteList { get; private set; }

    /// <summary>
    /// Schedule info from the bridge
    /// </summary>
    public IEnumerable<Schedule> Schedules { get; private set; }

    /// <summary>
    /// Rule info from the bridge
    /// </summary>
    public IEnumerable<Rule> Rules { get; private set; }

    /// <summary>
    /// Scene info from the bridge
    /// </summary>
    public IEnumerable<Scene> Scenes { get; private set; }

    /// <summary>
    /// Sensor info from the bridge
    /// </summary>
    public IEnumerable<Sensor> Sensors { get; private set; }

    /// <summary>
    /// Is Hue Entertainment API used on a group right now?
    /// </summary>
    public bool IsStreamingActive
    {
      get
      {
        return Groups.Any(x => x.Stream?.Active ?? false);
      }
    }

  }
}
