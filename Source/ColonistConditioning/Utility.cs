using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ColonistConditioning
{
    public static class Utility
    {
        #region Properties

        public static string UID => "com.stormdexter.colonistconditioning";

        #endregion

        readonly static Dictionary<Tuple<Pawn, string>, int> tickCounters = new Dictionary<Tuple<Pawn, string>, int>();
        readonly static int tickInterval = 60;
        public static bool TickElapsed(Pawn pawn, [CallerMemberName] string methodName = null, [CallerLineNumber] int lineNumber = 0)
        {
            var key = new Tuple<Pawn, string>(pawn, methodName);
            if(!tickCounters.ContainsKey(key))
            {
                tickCounters[key] = 1;
                return false;
            }

            if (tickCounters[key] < tickInterval)
            {
                tickCounters[key]++;
                return false;
            }

            tickCounters[key] = 0;

            return true;
        }
    }

}
