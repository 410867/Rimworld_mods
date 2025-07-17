using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace FPSRT
{
    public class Building_FPSRT : Building_Trap
    {
        private static readonly FloatRange DamageRandomFactorRange = new FloatRange(0.8f, 1.2f);

        private static readonly float DamageCount = 5f;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (!respawningAfterLoad)
            {
                SoundDefOf.TrapArm.PlayOneShot(new TargetInfo(base.Position, map, false));
            }
            
        }

        protected override void SpringSub(Pawn p)
        {
            SoundDefOf.TrapSpring.PlayOneShot(new TargetInfo(base.Position, base.Map));
            if (p == null)
            {
                return;
            }
            float num = this.GetStatValue(StatDefOf.TrapMeleeDamage) * DamageRandomFactorRange.RandomInRange;
            float armorPenetration = num * 0.015f;
            for (int i = 0; (float)i < 5f; i++)
            {
                DamageInfo dinfo = new DamageInfo(DamageDefOf.Stab, num, armorPenetration, -1f, this);
                DamageWorker.DamageResult damageResult = p.TakeDamage(dinfo);
                if (i == 0)
                {
                    BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(p, RulePackDefOf.DamageEvent_TrapSpike);
                    Find.BattleLog.Add(battleLogEntry_DamageTaken);
                    damageResult.AssociateWithLog(battleLogEntry_DamageTaken);
                }
            }

            Map map = base.Map;
            IntVec3 loc = this.Position;
            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("Building_FPSRTunarmed"), this.Stuff), loc, map, WipeMode.Vanish);
            thing.SetFaction(Faction.OfPlayer, null);
        }

    }
}
