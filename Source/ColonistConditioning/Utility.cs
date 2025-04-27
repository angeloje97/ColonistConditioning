using RimWorld.QuestGen;
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
        readonly static Dictionary<Tuple<string, string>, int> tickCountersFunction = new Dictionary<Tuple<string, string>, int>();
        
        readonly static int tickInterval = 60;
        readonly static int dayTickInterval = 1000;

        public static bool TickElapsed(Pawn pawn, [CallerMemberName] string methodName = null, [CallerLineNumber] int lineNumber = 0)
        {
            var key = new Tuple<Pawn, string>(pawn, methodName);
            if (!tickCounters.ContainsKey(key))
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
        public static bool LongTickElapsed(string id, [CallerMemberName] string methodName = null, [CallerLineNumber] int lineNumber = 0)
        {
            var key = new Tuple<string, string>(id, methodName);
            if (!tickCountersFunction.ContainsKey(key))
            {
                tickCountersFunction[key] = 1;
                return false;
            }

            if (tickCountersFunction[key] < dayTickInterval)
            {
                tickCountersFunction[key]++;
                return false;
            }

            tickCountersFunction[key] = 0;

            return true;
        }

    }

}
