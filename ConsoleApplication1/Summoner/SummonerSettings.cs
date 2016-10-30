using System.ComponentModel;
using System;
using Loki.Common;
using Loki;

namespace Summoner
{
    /// <summary>Settings for the ExamplePlugin. </summary>
    public class SummonerSettings : JsonSettings
    {
        private static SummonerSettings _instance;

        // flesh offering
        private bool enabledFleshOffering;
        private string cdFleshOffering;

        // bone offering
        private bool enabledBoneOffering;
        private string cdBoneOffering;

        // spirit offering
        private bool enabledSpiritOffering;
        private string cdSpiritOffering;

        // enduring cry
        private bool enabledEnduringCry;
        private string cdEnduringCry;

        // golems
        private string cdGolem;
        private bool enabledStoneGolem;
        private bool enabledFlameGolem;
        private bool enabledChaosGolem;
        private bool enabledIceGolem;
        private bool enabledLightningGolem;

        /// <summary>The current instance for this class. </summary>
        public static SummonerSettings Instance
        {
            get { return _instance ?? (_instance = new SummonerSettings()); }
        }

        /// <summary>The default ctor. Will use the settings path "ExamplePlugin".</summary>
        public SummonerSettings()
            : base(GetSettingsFilePath(Configuration.Instance.Name, string.Format("{0}.json", "Summenor")))
        {
            // TODO: Setup defaults here if needed for properties that don't support DefaultValue.
        }

        // flesh offering
        [DefaultValue(false)]
        public bool EnabledFleshOffering
        {
            get { return enabledFleshOffering; }
            set
            {
                if (value.Equals(enabledFleshOffering))
                {
                    return;
                }
                enabledFleshOffering = value;
                NotifyPropertyChanged(() => EnabledFleshOffering);
            }
        }

        [DefaultValue("10")]
        public string CdFleshOffering
        {
            get { return cdFleshOffering; }
            set
            {
                if (value.Equals(cdFleshOffering))
                {
                    return;
                }
                cdFleshOffering = value;
                NotifyPropertyChanged(() => CdFleshOffering);
            }
        }

        // bone offering
        [DefaultValue(false)]
        public bool EnabledBoneOffering
        {
            get { return enabledBoneOffering; }
            set
            {
                if (value.Equals(enabledBoneOffering))
                {
                    return;
                }
                enabledBoneOffering = value;
                NotifyPropertyChanged(() => EnabledBoneOffering);
            }
        }

        [DefaultValue("10")]
        public string CdBoneOffering
        {
            get { return cdBoneOffering; }
            set
            {
                if (value.Equals(cdBoneOffering))
                {
                    return;
                }
                cdBoneOffering = value;
                NotifyPropertyChanged(() => CdBoneOffering);
            }
        }

        // spirit offering
        [DefaultValue(false)]
        public bool EnabledSpiritOffering
        {
            get { return enabledSpiritOffering; }
            set
            {
                if (value.Equals(enabledSpiritOffering))
                {
                    return;
                }
                enabledSpiritOffering = value;
                NotifyPropertyChanged(() => EnabledSpiritOffering);
            }
        }

        [DefaultValue("10000")]
        public string CdSpiritOffering
        {
            get { return cdSpiritOffering; }
            set
            {
                if (value.Equals(cdSpiritOffering))
                {
                    return;
                }
                cdSpiritOffering = value;
                NotifyPropertyChanged(() => CdSpiritOffering);
            }
        }

        // enduring cry
        [DefaultValue(false)]
        public bool EnabledEnduringCry
        {
            get { return enabledEnduringCry; }
            set
            {
                if (value.Equals(enabledEnduringCry))
                {
                    return;
                }
                enabledEnduringCry = value;
                NotifyPropertyChanged(() => EnabledEnduringCry);
            }
        }

        [DefaultValue("10000")]
        public string CdEnduringCry
        {
            get { return cdEnduringCry; }
            set
            {
                if (value.Equals(cdEnduringCry))
                {
                    return;
                }
                cdEnduringCry = value;
                NotifyPropertyChanged(() => CdEnduringCry);
            }
        }

        // golem cd
        [DefaultValue("10000")]
        public string CdGolem
        {
            get { return cdGolem; }
            set
            {
                if (value.Equals(cdGolem))
                {
                    return;
                }
                cdGolem = value;
                NotifyPropertyChanged(() => CdGolem);
            }
        }

        // stone golem
        [DefaultValue(false)]
        public bool EnabledStoneGolem
        {
            get { return enabledStoneGolem; }
            set
            {
                if (value.Equals(enabledStoneGolem))
                {
                    return;
                }
                enabledStoneGolem = value;
                NotifyPropertyChanged(() => EnabledStoneGolem);
            }
        }

        // chaos golem
        [DefaultValue(false)]
        public bool EnabledChaosGolem
        {
            get { return enabledChaosGolem; }
            set
            {
                if (value.Equals(enabledChaosGolem))
                {
                    return;
                }
                enabledChaosGolem = value;
                NotifyPropertyChanged(() => EnabledChaosGolem);
            }
        }

        // flame golem
        [DefaultValue(false)]
        public bool EnabledFlameGolem
        {
            get { return enabledFlameGolem; }
            set
            {
                if (value.Equals(enabledFlameGolem))
                {
                    return;
                }
                enabledFlameGolem = value;
                NotifyPropertyChanged(() => EnabledFlameGolem);
            }
        }

        // ice golem
        [DefaultValue(false)]
        public bool EnabledIceGolem
        {
            get { return enabledIceGolem; }
            set
            {
                if (value.Equals(enabledIceGolem))
                {
                    return;
                }
                enabledIceGolem = value;
                NotifyPropertyChanged(() => EnabledIceGolem);
            }
        }

        // lightning golem
        [DefaultValue(false)]
        public bool EnabledLightningGolem
        {
            get { return enabledLightningGolem; }
            set
            {
                if (value.Equals(enabledLightningGolem))
                {
                    return;
                }
                enabledLightningGolem = value;
                NotifyPropertyChanged(() => EnabledLightningGolem);
            }
        }
    }
}