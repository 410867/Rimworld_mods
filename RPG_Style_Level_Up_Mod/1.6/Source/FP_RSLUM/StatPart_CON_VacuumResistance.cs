using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;

namespace FP_RSLUM
{
    class StatPart_CON_VacuumResistance : StatPart
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
				if (pawnlvcomp != null)
				{
					val += (float)(Math.Min(0.5, (0.03 * pawnlvcomp.CON)));
				}
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null)
				{
					PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
					if (pawnlvcomp != null)
						return "StatsReport_STAT_CON".Translate() + ": +" + (Math.Min(0.5, (0.03 * pawnlvcomp.CON)));
				}
			}
			return "";
		}
	}
}
