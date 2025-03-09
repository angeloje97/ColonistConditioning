using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ColonistConditioning
{
    class Manager: Mod
    {
        public static Settings settings;

        public override string SettingsCategory() => "Colony Conditioning";

        public Manager(ModContentPack content) : base(content)
        {
            settings = GetSettings<Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listingStandard = new Listing_Standard();

            #region Settings List
            listingStandard.Begin(inRect);

            settings.HandleListing(listingStandard, inRect);

            listingStandard.End();
            #endregion

            listingStandard.End();
            settings.Write();
        }
    }
}
