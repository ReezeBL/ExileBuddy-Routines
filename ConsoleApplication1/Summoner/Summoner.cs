using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Buddy.Coroutines;
using log4net;
using Loki.Bot;
using Loki.Common;
using Loki.Game.GameData;
using System.IO;
using System.Windows.Markup;
using System.Windows.Data;
using Loki.Game;
using System.Collections.Generic;
using Loki.Game.Objects;

namespace Summoner
{
    class Summoner : IPlugin
    {
        private static readonly ILog Log = Logger.GetLoggerInstanceForType();
        private TaskManager _taskManager;
        

        public void Start()
        {
            Log.Debug("[Summoner] Start");

            // load default taskmanager
            _taskManager = (TaskManager)BotManager.CurrentBot.Execute("GetTaskManager");
            if (_taskManager == null)
            {
                Log.Error("[Summoner] Fail to get the global TaskManager.");
                BotManager.Stop();
                return;
            }

            // add Summoner task
            if (!_taskManager.AddBefore(new SummonerTask(), "CombatTask (Leash 50)"))
            {
                Log.Error("[Summoner] Failed to add Task 'SummonerTask'.");
            }
        }

        
        public async Task<bool> Logic(string type, params dynamic[] param)
        {
            return false;
        }

        public void Tick()
        {
        }

        public object Execute(string name, params dynamic[] param)
        {
            return null;
        }

        public void Enable()
        {
            Log.Debug("[Summoner] Enable");
        }

        public void Disable()
        {
            Log.Debug("[Summoner] Disable");
        }

        public void Dispose()
        {
        }

        #region Implementation of ISettings
        public JsonSettings Settings
        {
            get
            {
                return SummonerSettings.Instance;
            }
        }

        public UserControl Control
        {
            get
            {
                using (var fs = new FileStream(@"3rdParty\Summoner\SettingsGui.xaml", FileMode.Open))
                {
                    var root = (UserControl)XamlReader.Load(fs);
                    var instance = SummonerSettings.Instance;

                    // flesh offering enabled
                    if (!Wpf.SetupCheckBoxBinding(root, "EnabledFleshOfferingCheckBox", "EnabledFleshOffering",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupCheckBoxBinding failed for 'EnabledFleshOfferingCheckBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // flesh offering cd
                    if (!Wpf.SetupTextBoxBinding(root, "CdFleshOfferingTextBox", "CdFleshOffering",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupTextBoxBinding failed for 'CdFleshOfferingTextBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // bone offering enabled
                    if (!Wpf.SetupCheckBoxBinding(root, "EnabledBoneOfferingCheckBox", "EnabledBoneOffering",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupCheckBoxBinding failed for 'EnabledBoneOfferingCheckBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // bone offering cd
                    if (!Wpf.SetupTextBoxBinding(root, "CdBoneOfferingTextBox", "CdBoneOffering",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupTextBoxBinding failed for 'CdBoneOfferingTextBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // spirit offering enabled
                    if (!Wpf.SetupCheckBoxBinding(root, "EnabledSpiritOfferingCheckBox", "EnabledSpiritOffering",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupCheckBoxBinding failed for 'EnabledSpiritOfferingCheckBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // spirit offering cd
                    if (!Wpf.SetupTextBoxBinding(root, "CdSpiritOfferingTextBox", "CdSpiritOffering",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupTextBoxBinding failed for 'CdSpiritOfferingTextBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // enduring cry enabled
                    if (!Wpf.SetupCheckBoxBinding(root, "EnabledBoneOfferingCheckBox", "EnabledBoneOffering",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupCheckBoxBinding failed for 'EnabledBoneOfferingCheckBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // enduring cry cd
                    if (!Wpf.SetupTextBoxBinding(root, "CdEnduringCryTextBox", "CdEnduringCry",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupTextBoxBinding failed for 'CdEnduringCryTextBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // golem cd
                    if (!Wpf.SetupTextBoxBinding(root, "CdGolemTextBox", "CdGolem",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupTextBoxBinding failed for 'CdGolemTextBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // stone golem enabled
                    if (!Wpf.SetupCheckBoxBinding(root, "EnabledStoneGolemCheckBox", "EnabledStoneGolem",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupCheckBoxBinding failed for 'EnabledStoneGolemCheckBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // ice golem enabled
                    if (!Wpf.SetupCheckBoxBinding(root, "EnabledIceGolemCheckBox", "EnabledIceGolem",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupCheckBoxBinding failed for 'EnabledIceGolemCheckBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // flame golem enabled
                    if (!Wpf.SetupCheckBoxBinding(root, "EnabledFlameGolemCheckBox", "EnabledFlameGolem",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupCheckBoxBinding failed for 'EnabledFlameGolemCheckBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // chaos golem enabled
                    if (!Wpf.SetupCheckBoxBinding(root, "EnabledChaosGolemCheckBox", "EnabledChaosGolem",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupCheckBoxBinding failed for 'EnabledChaosGolemCheckBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    // lightning golem enabled
                    if (!Wpf.SetupCheckBoxBinding(root, "EnabledLightningGolemCheckBox", "EnabledLightningGolem",
                        BindingMode.TwoWay, SummonerSettings.Instance))
                    {
                        Log.ErrorFormat(
                            "[SettingsControl] SetupCheckBoxBinding failed for 'EnabledLightningGolemCheckBox'.");
                        throw new Exception("The SettingsControl could not be created.");
                    }

                    return root;
                }
            }
        }

        #endregion
        
        #region Utility functions

        public static async Task SleepSafe(int ms)
        {
            await Coroutine.Sleep(Math.Max(LatencyTracker.Current, ms));
        }

        public static async Task<bool> WaitFor(Func<bool> condition, string desc, int step = 100, int timeout = 5000)
        {
            return await WaitFor(condition, desc, () => step, timeout);
        }

        public static async Task<bool> WaitFor(Func<bool> condition, string desc, Func<int> step, int timeout = 5000)
        {
            if (condition()) return true;
            Stopwatch timer = Stopwatch.StartNew();
            while (timer.ElapsedMilliseconds < timeout)
            {
                await Coroutine.Sleep(step());
                Log.DebugFormat("[WaitFor] Waiting for {0} ({1}/{2})",
                    desc, Math.Round(timer.ElapsedMilliseconds / 1000f, 2), timeout / 1000f);
                if (condition()) return true;
            }
            Log.ErrorFormat("[WaitFor] Wait for {0} timeout.", desc);
            return false;
        }

        #endregion

        #region TaskManager utilities

        private void AddTask(ITask task, string name, AddType type)
        {
            bool added = false;
            switch (type)
            {
                case AddType.Before:
                    added = _taskManager.AddBefore(task, name);
                    break;

                case AddType.After:
                    added = _taskManager.AddAfter(task, name);
                    break;

                case AddType.Replace:
                    added = _taskManager.Replace(name, task);
                    break;
            }
            if (!added)
            {
                Log.ErrorFormat("[Summoner] Fail to add \"{0}\".", name);
                BotManager.Stop();
            }
        }

        private enum AddType
        {
            Before,
            After,
            Replace
        }

        #endregion

        #region Unused stuff
        public void Initialize()
        {
            Log.Debug("[Summoner] Initialize");
        }

        public void Deinitialize()
        {
            Log.Debug("[Summoner] Deinitialize");
        }

        public void Stop()
        {
            Log.Debug("[Summoner] Stop");
        }

        public string Author
        {
            get { return "spliffermaster"; }
        }

        public string Description
        {
            get { return "Plugin for Summoners."; }
        }

        public string Name
        {
            get { return "Summoner"; }
        }

        public string Version
        {
            get { return "0.1.0"; }
        }
        #endregion

    }
}
