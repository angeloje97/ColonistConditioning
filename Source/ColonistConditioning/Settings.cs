using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ColonistConditioning
{
    public class Settings : ModSettings
    {
        [Range(0f, 100f)] float traitGainMultiplier = 100f;

        public float TraitGainMultiplier => traitGainMultiplier;

        [Range(0f, 100f)]float traitLossMultiplier = 100f;
        public float TraitLossMultiplier => traitLossMultiplier;

        bool worsenWhenNeutral = true;

        public bool WorsenWhenNeutral => worsenWhenNeutral;


        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref traitGainMultiplier, "traitGainMultiplier", 100f);
            Scribe_Values.Look(ref traitLossMultiplier, "traitLossMultiplier", 100f);
            Scribe_Values.Look(ref worsenWhenNeutral, "worsenWhenNeutral", true);

        }

        public void HandleListing(Listing_Standard listing, Rect rect)
        {
            var rowPos = 0f;
            HandleFloat(ref traitGainMultiplier, $"Trait gain multiplier: {traitGainMultiplier}%");
            HandleFloat(ref traitLossMultiplier, $"Trait loss multiplier: {traitLossMultiplier}%");
            
            HandleBool(ref worsenWhenNeutral, "Worsen on neutral");

            #region Handlers

            void HandleFloat(ref float val, string label)
            {
                var nextRect = NextRect();

                nextRect.width *= .5f;
                
                Widgets.HorizontalSlider(
                    nextRect,
                    ref val,
                    new FloatRange(0f, 200f),
                    label,
                    roundTo: 1
                 );
            }

            void HandleBool(ref bool val, string label, string toolTip = "")
            {
                var nextRect = NextRect(0f);
                nextRect.width *= .5f;
                Widgets.CheckboxLabeled(nextRect, label, ref val);

                if (!string.IsNullOrEmpty(toolTip) && Mouse.IsOver(nextRect))
                {
                    TooltipHandler.TipRegion(nextRect, toolTip);
                }
            }

            Rect NextRect(float height = 30f)
            {
                rowPos += height;
                return listing.GetRect(rowPos);
            }
            
            #endregion
        }

    }
}
