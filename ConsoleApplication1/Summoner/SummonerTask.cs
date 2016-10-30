using log4net;
using Loki.Bot;
using Loki.Common;
using Loki.Game;
using Loki.Game.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summoner
{
    class SummonerTask : ITask
    {
        private static readonly ILog Log = Logger.GetLoggerInstanceForType();

        private bool _debug = false;
        private int _offeringCd = 2500;

        // timers
        private readonly Stopwatch _fleshOfferingStopwatch = Stopwatch.StartNew();
        private readonly Stopwatch _boneOfferingStopwatch = Stopwatch.StartNew();
        private readonly Stopwatch _spiritOfferingStopwatch = Stopwatch.StartNew();
        private readonly Stopwatch _offeringStopwatch = Stopwatch.StartNew();
        private readonly Stopwatch _enduringCryStopwatch = Stopwatch.StartNew();
        private readonly Stopwatch _golemStopwatch = Stopwatch.StartNew();
        private readonly Stopwatch _debugStopwatch = Stopwatch.StartNew();


        public async Task<bool> Logic(string type, params dynamic[] param)
        {
            // on map and area only
            if (LokiPoe.Me.IsDead || LokiPoe.Me.IsInHideout || LokiPoe.Me.IsInTown) return false;

            // scan for corpses and monsters
            var corpsesNearby = GetOfferingCorpses().OrderBy(m => m.Distance);
            var monstersNearby = GetMonstersNearbyCount();

            // Debug timers
            if (_debugStopwatch.ElapsedMilliseconds > _offeringCd && _debug)
            {
                Log.Info("[SummonerTask] =========== Summoner Debug ==========");
                Log.InfoFormat("[Summoner] Has 'Flesh Offering': {0}.", LokiPoe.Me.HasFleshOffering);
                Log.InfoFormat("[Summoner] Has 'Bone Offering': {0}.", LokiPoe.Me.HasBoneOffering);
                Log.InfoFormat("[Summoner] Has 'Enduring Charge': {0}.", LokiPoe.Me.HasEnduranceCharge);
                Log.InfoFormat("[Summoner] 'Monsters nearby': {0}.", monstersNearby);
                Log.InfoFormat("[Summoner] Cd 'Offering': {0} - Timeout: {1}.", _offeringStopwatch.ElapsedMilliseconds, _offeringCd);
                Log.InfoFormat("[Summoner] Cd 'Flesh Offering': {0}.", _fleshOfferingStopwatch.ElapsedMilliseconds);
                Log.InfoFormat("[Summoner] Cd 'Bone Offering': {0}.", _boneOfferingStopwatch.ElapsedMilliseconds);
                Log.InfoFormat("[Summoner] Cd 'Spirit Offering': {0}.", _spiritOfferingStopwatch.ElapsedMilliseconds);
                Log.InfoFormat("[Summoner] Cd 'Golem'': {0}.", _golemStopwatch.ElapsedMilliseconds);
                Log.InfoFormat("[Summoner] Cd 'Enduring Cry': {0}.", _enduringCryStopwatch.ElapsedMilliseconds);
                Log.Info("[SummonerTask] ====================================");
                _debugStopwatch.Restart();
            }

            // ==============
            // Flesh Offering
            // ==============
            if (SummonerSettings.Instance.EnabledFleshOffering && _offeringStopwatch.ElapsedMilliseconds > _offeringCd)
            {
                // cooldown
                if (_fleshOfferingStopwatch.ElapsedMilliseconds > Int32.Parse(SummonerSettings.Instance.CdFleshOffering))
                {
                    var setOffering = SetOffering("Flesh Offering", corpsesNearby.ToList(), monstersNearby, _fleshOfferingStopwatch);
                    if (!setOffering)
                    {
                        Log.InfoFormat("[SummonerTask] Set 'Flesh Offering' failed. No monsters or corpses nearby.");
                    }
                }
            }

            // =============
            // Bone Offering
            // =============
            if (SummonerSettings.Instance.EnabledBoneOffering && _offeringStopwatch.ElapsedMilliseconds > _offeringCd)
            {
                // cooldown
                if (_boneOfferingStopwatch.ElapsedMilliseconds > Int32.Parse(SummonerSettings.Instance.CdBoneOffering))
                {
                    var setOffering = SetOffering("Bone Offering", corpsesNearby.ToList(), monstersNearby, _boneOfferingStopwatch);
                    if (!setOffering)
                    {
                        Log.InfoFormat("[SummonerTask] Set 'Bone Offering' failed. No monsters or corpses nearby.");
                    }
                }
            }

            // ===============
            // Spirit Offering
            // ===============
            if (SummonerSettings.Instance.EnabledSpiritOffering && _offeringStopwatch.ElapsedMilliseconds > _offeringCd)
            {
                // cooldown
                if (_spiritOfferingStopwatch.ElapsedMilliseconds > Int32.Parse(SummonerSettings.Instance.CdSpiritOffering))
                {
                    var setOffering = SetOffering("Spirit Offering", corpsesNearby.ToList(), monstersNearby, _spiritOfferingStopwatch);
                    if (!setOffering)
                    {
                        Log.InfoFormat("[SummonerTask] Set 'Spirit Offering' failed. No monsters or corpses nearby.");
                    }
                }
            }

            // offering attempts time limit
            //_offeringStopwatch.Restart();

            // =====
            // Golem
            // =====
            if (SummonerSettings.Instance.EnabledStoneGolem ||
                SummonerSettings.Instance.EnabledChaosGolem ||
                SummonerSettings.Instance.EnabledFlameGolem ||
                SummonerSettings.Instance.EnabledIceGolem ||
                SummonerSettings.Instance.EnabledLightningGolem)
            {
                // Cooldown
                if (_golemStopwatch.ElapsedMilliseconds > Int32.Parse(SummonerSettings.Instance.CdGolem))
                {
                    if (monstersNearby == 0)
                    {
                        Log.InfoFormat("[SummonerTask] No monsters nearby. Offering not available.");
                        return false;
                    }

                    if (!corpsesNearby.Any())
                    {
                        Log.InfoFormat("[SummonerTask] No corpses nearby. Cannot use offering.");
                        return false;
                    }

                    bool result = false;
                    //Log.InfoFormat("[Summoner] Check 'Golem'.");

                    // stone golem
                    if (SummonerSettings.Instance.EnabledStoneGolem)
                    {
                        result = SetGolem("Summon Stone Golem", "Stone Golem");
                    }

                    // chaos golem
                    if (SummonerSettings.Instance.EnabledChaosGolem)
                    {
                        result = SetGolem("Summon Chaos Golem", "Chaos Golem");
                    }

                    // flame golem
                    if (SummonerSettings.Instance.EnabledFlameGolem)
                    {
                        result = SetGolem("Summon Flame Golem", "Flame Golem");
                    }

                    // ice golem
                    if (SummonerSettings.Instance.EnabledIceGolem)
                    {
                        result = SetGolem("Summon Ice Golem", "Ice Golem");
                    }

                    // lightning golem
                    if (SummonerSettings.Instance.EnabledLightningGolem)
                    {
                        result = SetGolem("Summon Lightning Golem", "Lightning Golem");
                    }

                    // reset cd
                    if (result)
                    {
                        Log.InfoFormat("[Summoner] Golem has been summoned.");
                    }

                    _golemStopwatch.Restart();
                }
            }

            return false;
        }

        #region Implementation of IRunnable
        public void Initialize()
        {
            Log.Debug("[SummonerTask] Initialize");
        }

        public void Deinitialize()
        {
            Log.Debug("[SummonerTask] Deinitialize");
        }

        public void Start()
        {
            Log.Debug("[SummonerTask] Start");
            // Start timers
            _fleshOfferingStopwatch.Start();
            _boneOfferingStopwatch.Start();
            _spiritOfferingStopwatch.Start();
            _offeringStopwatch.Start();
            _golemStopwatch.Start();
            _enduringCryStopwatch.Start();
            _debugStopwatch.Start();
        }

        public void Stop()
        {
            Log.Debug("[SummonerTask] Stop");

            // Stop timers
            _fleshOfferingStopwatch.Stop();
            _boneOfferingStopwatch.Stop();
            _spiritOfferingStopwatch.Stop();
            _golemStopwatch.Stop();
            _enduringCryStopwatch.Stop();
            _debugStopwatch.Stop();
        }

        public void Tick()
        {
        }

        public object Execute(string name, params dynamic[] param)
        {
            return null;
        }
        #endregion

        #region Implementation of IAuthored
        public string Author
        {
            get { return "spliffermaster"; }
        }

        public string Description
        {
            get { return "Summoner task."; }
        }

        public string Name
        {
            get { return "SummonerTask"; }
        }

        public string Version
        {
            get { return "0.1.0"; }
        }
        #endregion


        #region Offering
        private bool SetOffering(string name, List<Monster> corpses, int monstersNearby, Stopwatch sw)
        {
            //Log.InfoFormat("[SummonerTask] Need '{0}'.", name);
            // check for available offering corpse(s)
            if (corpses.Any() && monstersNearby > 0)
            {
                //Log.InfoFormat("[Summoner] Found '{0}' corpse(s) for '{1}'.", corpses.Count(), name);

                // use skill Spirit Offering
                var usedSkill = UseSkill(name, corpses.FirstOrDefault());

                // could not use skill
                if (!usedSkill) return false;

                // restart Spirit Offering timer
                sw.Restart();
                _offeringStopwatch.Restart();
                return true;
            }
            return false;
        }
        #endregion

        #region Golems
        public bool SetGolem(string name, string minionName = null)
        {
            // check golem is still active
            if (CheckGolem(name, minionName))
            {
                Log.InfoFormat("[SummonerTask] '{0}' is still active.", minionName);
                return false;
            }

            // apply golem
            if (!ApplyGolem(name))
            {
                Log.ErrorFormat("[SummonerTask] '{0}' could not be applied.", name);
            }
            return true;
        }

        public bool CheckGolem(string name, string minionName = null)
        {
            var minions = LokiPoe.Me.DeployedMinions.Where(
                m =>
                    m.Name == minionName
            );

            // check golem aura names
            //Log.InfoFormat("[Summoner] Deployed minions: {0}.", minions.Count());
            /*
            foreach (NetworkObject minion in minions)
            {
                Log.InfoFormat("[Summoner:CheckGolem] Found minion '{0}'.", minion.Name);
            }
            */

            // aura found
            if (minions.Any()) return true;

            Log.ErrorFormat("[SummonerTask:CheckGolem] No minion '{0}' found.", minionName);
            return false;
        }

        public bool ApplyGolem(string name)
        {
            // find skill in skill bar
            var slots = LokiPoe.InGameState.SkillBarHud.SkillBarSkills.Where(
                s =>
                    s.Name == name
            );

            // found skill in skillbar
            if (slots.Any())
            {
                var slot = slots.FirstOrDefault();

                // use skill
                var usedSkill = LokiPoe.InGameState.SkillBarHud.UseAt(slot.Slot, true, LokiPoe.Me.Position);
                if (usedSkill != LokiPoe.InGameState.UseResult.None)
                {
                    Log.ErrorFormat("[SummonerTask:UseSkill] Could not use skill '{0}' in skillbar: {1}.", name, usedSkill);
                    return false;
                }

                return true;
            }

            Log.ErrorFormat("[SummonerTask:ApplyGolem] Could not apply golem '{0}'. Skill not found.", name);
            return false;
        }
        #endregion

        #region Skills
        /// <summary> Use a skill on the skill bar</summary>
        public bool UseSkill(string skillName, NetworkObject nObject, bool aip = true)
        {
            // find skill in skillbar
            var slots = LokiPoe.InGameState.SkillBarHud.SkillBarSkills.Where(
                s =>
                    s.Name == skillName
            );

            // found skill in skill bar
            if (slots.Any())
            {
                var slot = slots.FirstOrDefault();

                Log.ErrorFormat("[SummonerTask:UseSkill] Skill '{0}' with distance '{1}' at position '{2}' on object '{3}'.",
                    skillName, nObject.Distance, nObject.Position, nObject.Name);

                // use skill
                var usedSkill = LokiPoe.InGameState.SkillBarHud.UseAt(slot.Slot, aip, nObject.Position);
                if (usedSkill != LokiPoe.InGameState.UseResult.None)
                {
                    Log.ErrorFormat("[SummonerTask:UseSkill] Could not use skill '{0}' in skillbar: {1}.", skillName, usedSkill);
                }

                //Log.ErrorFormat("[Summoner:UseSkill] Used skill '{0}' with result '{1}'.", skillName, usedSkill);
                return true;
            }

            Log.ErrorFormat("[SummonerTask:UseSkill] Could not find skill '{0}' in skillbar.", skillName);
            return false;
        }
        #endregion

        #region Corpses
        private List<Monster> GetOfferingCorpses(int radius = 40)
        {
            var monsters = LokiPoe.ObjectManager.GetObjectsByType<Monster>().Where(
                m =>
                    m.IsDead &&
                    m.IsHostile &&
                    m.IsTargetable &&
                    m.IsValid &&
                    m.Distance <= radius
            );

            //Log.InfoFormat("[Summoner:GetOfferingCorpses] Found {0} corpses to offer.", monsters.Count());
            return monsters.ToList();
        }
        #endregion

        #region Monsters
        private List<Monster> GetMonstersNearby(int radius = 100)
        {
            var monsters = LokiPoe.ObjectManager.GetObjectsByType<Monster>().Where(
                m =>
                    !m.IsDead &&
                    m.IsHostile &&
                    m.IsValid &&
                    m.Distance <= radius
            );

            //Log.InfoFormat("[Summoner:GetOfferingCorpses] Found {0} monsters neabry.", monsters.Count());
            return monsters.ToList();
        }

        private int GetMonstersNearbyCount()
        {
            var monsters = GetMonstersNearby(70);
            return monsters.Count();
        }
        #endregion


    }
}
