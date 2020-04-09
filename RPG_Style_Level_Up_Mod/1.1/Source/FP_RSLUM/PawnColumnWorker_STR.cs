﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace FP_RSLUM
{
    class PawnColumnWorker_STR : PawnColumnWorker
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
            PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
            if (pawnlvcomp != null)
            {
                if (pawnlvcomp.StatPoint > 0)
                {
                    this.DoStatUpButton(new Rect(rect.x, rect.yMin, 30f, 30f), pawn);
                    Rect rect2 = new Rect(rect.x + 50, rect.y, rect.width - 50, Mathf.Min(rect.height, 30f));
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
                else
                {
                    Rect rect2 = new Rect(rect.x + 35, rect.y, rect.width - 50, Mathf.Min(rect.height, 30f));
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
            }
        }

        protected string GetTip(Pawn pawn)
        {
            return  "PawnColumnWorker_STR_Tip_Desc".Translate();
        }



        //protected override string GetTextFor(Pawn pawn)
        //    => Math.Round(pawn.ageTracker.AgeBiologicalYearsFloat, 2).ToString("0.00");

        protected string GetTextFor(Pawn pawn)
        {
            PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
            if (pawnlvcomp != null)
            {
                return pawnlvcomp.STR.ToString();
            }
            else
            {
                // WHAT?
                Log.Message("error in PawnColumnWorker_Level GetTextFor. no pawnlvcomp.");
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
                return pawnlvcompa.STR.CompareTo(pawnlvcompb.STR);
            }
            else
            {
                // WHAT?
                Log.Message("error in PawnColumnWorker_Level Compare. no pawnlvcomp.");
                return 0;
            }
        }

        public override int GetMinWidth(PawnTable table)
        {
            return base.GetMinWidth(table) + 30;
        }

        public void DoStatUpButton(Rect rect, Pawn pawn)
        {
            TooltipHandler.TipRegion(rect, Translator.Translate("LvTab_Distribute"));
            if (Widgets.ButtonImage(rect, harmony_patches.DistributeIMG, Color.white, GenUI.SubtleMouseoverColor))
            {
                PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                if (pawnlvcomp != null)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (pawnlvcomp.StatPoint > 0)
                            {
                                pawnlvcomp.StatPoint -= 1;
                                pawnlvcomp.STR += 1;
                            }
                        }
                    }
                    else if (Input.GetKey(KeyCode.LeftControl))
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (pawnlvcomp.StatPoint > 0)
                            {
                                pawnlvcomp.StatPoint -= 1;
                                pawnlvcomp.STR += 1;
                            }
                        }
                    }
                    else if (Input.GetKey(KeyCode.LeftAlt))
                    {
                        if (pawnlvcomp.STR > FP_RSLUM_setting.Startingstat_min)
                        {
                            pawnlvcomp.StatPoint += 1;
                            pawnlvcomp.STR -= 1;
                        }
                    }
                    else
                    {
                        if (pawnlvcomp.StatPoint > 0)
                        {
                            pawnlvcomp.StatPoint -= 1;
                            pawnlvcomp.STR += 1;
                        }
                    }
                }
            }
        }
    }
}
