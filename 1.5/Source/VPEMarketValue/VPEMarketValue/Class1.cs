using System;
using System.Text;
using RimWorld;
using Verse;
using HarmonyLib;

namespace PsylinkMarketValue
{
    [StaticConstructorOnStartup]
    public static class PsylinkMarketValue
    {
        static PsylinkMarketValue()
        {
            var harmony = new Harmony("Person.PsylinkMarketValue");
            harmony.PatchAll();
            Log.Message("Psylink Market Value Initialized");
        }
    }

    [HarmonyPatch(typeof(PriceUtility))]
    [HarmonyPatch("PawnQualityPriceOffset")]
    class PsylinkMarketValuePatch
    {
        static void Postfix(ref float __result, ref Pawn pawn, ref StringBuilder explanation)
        {
            if (pawn.GetPsylinkLevel() != 0) 
            {
                float newvalueoffset = 0;
                float PApriceOffset = HediffDefOf.PsychicAmplifier.priceOffset;
                newvalueoffset = Convert.ToSingle(PApriceOffset * pawn.GetPsylinkLevel() - PApriceOffset);
                explanation?.AppendLine("Psylink Level Offset: " + newvalueoffset.ToStringMoneyOffset());
                //Log.Message(pawn.Name.ToStringFull + "'s psylink level offset: " + newvalueoffset);
                __result += newvalueoffset;
                
            }
        }
    }
}
