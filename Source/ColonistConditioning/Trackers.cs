using ColonistConditioning;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ColonyConditioning
{
    #region Harmony

    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        public static DistanceTracker DistanceTrackerInstance = new DistanceTracker();

        static HarmonyPatches()
        {
            var harmony = new Harmony(Utility.UID);
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("Tick")]

    public static class Pawn_Tick_Patch
    {
        static void Postfix(Pawn __instance)
        {
            if (__instance.IsColonist)
            {
                HarmonyPatches.DistanceTrackerInstance.Update(__instance);
            }
        }
    }

    #endregion

    #region Trackers
    public class TrackerManager
    {

    }

    public class Tracker<T> : IExposable
    {
        public virtual string label => "Generic Tracker";

        public Dictionary<Pawn, T> pawnValues = new Dictionary<Pawn, T>();

        public void SetValue(Pawn pawn, T value)
        {
            pawnValues[pawn] = value;
        }

        public virtual void Update(Pawn pawn) { }

        public virtual void ExposeData()
        {
            Scribe_Collections.Look(ref pawnValues, label, LookMode.Reference, LookMode.Value);

            if(Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                if(pawnValues == null)
                {
                    pawnValues = new Dictionary<Pawn, T>();
                }
            }
        }
    }
    
    public class DistanceTracker: Tracker<float>
    {
        public override string label => "Distance Tracker";
        public Dictionary<Pawn, IntVec3> lastPositions = new Dictionary<Pawn, IntVec3>();

        public override void Update(Pawn pawn)
        {
            if (!Utility.TickElapsed(pawn)) return;
            var newPosition = pawn.Position;
            if (!lastPositions.ContainsKey(pawn))
            {
                lastPositions[pawn] = newPosition;
                if (!pawnValues.ContainsKey(pawn))
                {
                    pawnValues[pawn] = 0;
                }
                return;
            }

            if (newPosition == lastPositions[pawn]) return;
            pawnValues[pawn] += (lastPositions[pawn] - newPosition).LengthHorizontal;
            lastPositions[pawn] = newPosition;
            Log.Message($"{pawn} Traveled {pawnValues[pawn]} Distance");
        }
    }

    #endregion
    
    #region Milestones


    #endregion
}
