﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace FP_RSLUM
{
    class PawnColumnWorker_Need_EXP : PawnColumnWorker
    {
        private static NumericStringComparer comparer = new NumericStringComparer();

        protected virtual int Width
        {
            get
            {
                return this.def.width;
            }
        }

        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            Rect rect2 = new Rect(rect.x, rect.y, rect.width - 70, Mathf.Min(rect.height, 30f));
            String textFor = this.GetTextFor(pawn);
            if (textFor != null)
            {
                Text.Font = GameFont.Small;
                Text.Anchor = TextAnchor.MiddleCenter;
                Text.WordWrap = false;
                Widgets.Label(rect2, textFor);
                Text.WordWrap = true;
                Text.Anchor = TextAnchor.UpperLeft;
                String tip = this.GetTip(pawn);
                if (!tip.NullOrEmpty())
                {
                    TooltipHandler.TipRegion(rect2, tip);
                }
            }
        }

        protected String GetTip(Pawn pawn)
        {
            return "PawnColumnWorker_Need_EXP_Tip_Desc".Translate();
        }
        protected string GetTextFor(Pawn pawn)
        {
            PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
            if (pawnlvcomp != null)
            {
                return ((int)(pawnlvcomp.need_exp / 100)).ToString();
            }
            else
            {
                // WHAT?
                Log.Message("error in PawnColumnWorker_Need_EXP GetTextFor. no pawnlvcomp.");
                return "err";
            }
        }

        public override int Compare(Pawn a, Pawn b)
        //    => a.ageTracker.AgeBiologicalYearsFloat.CompareTo(b.ageTracker.AgeBiologicalYearsFloat);
        {
            PawnLvComp pawnlvcompa = a.TryGetComp<PawnLvComp>();
            PawnLvComp pawnlvcompb = b.TryGetComp<PawnLvComp>();
            if (pawnlvcompa != null && pawnlvcompb != null)
            {
                return pawnlvcompa.need_exp.CompareTo(pawnlvcompb.need_exp);
            }
            else
            {
                // WHAT?
                Log.Message("error in PawnColumnWorker_Need_EXP Compare. no pawnlvcomp.");
                return 0;
            }
        }

        public override int GetMinWidth(PawnTable table)
        {
            return base.GetMinWidth(table) + 30;
        }
    }
}
