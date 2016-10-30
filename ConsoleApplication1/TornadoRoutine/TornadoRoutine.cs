using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Buddy.Coroutines;
using log4net;
using Loki.Bot;
using Loki.Bot.Pathfinding;
using Loki.Game;
using Loki.Game.GameData;
using Loki.Game.Objects;
using Loki.Common;

namespace TornadoRoutine
{
    class TornadoRoutine : IRoutine
    {
        public UserControl Control { get; }
        public JsonSettings Settings { get; }
        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public string Name { get { return "TornadoRoutine"; } }
        public string Author { get { return "BL, Inc."; } }
        public string Description { get { return "Basic Routine for Tornado Shot archers"; } }
        public string Version { get { return "1.0.0"; } }



        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Deinitialize()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Logic(string type, params dynamic[] param)
        {
            throw new NotImplementedException();
        }

        public object Execute(string name, params dynamic[] param)
        {
            throw new NotImplementedException();
        }
    }
}
