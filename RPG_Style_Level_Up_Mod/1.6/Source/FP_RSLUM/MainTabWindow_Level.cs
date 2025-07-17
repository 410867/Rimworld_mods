using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using RimWorld.Planet;

namespace FP_RSLUM
{
    class MainTabWindow_Level : MainTabWindow_PawnTable
    {
        public const float buttonWidth = 110f;
        public const float buttonHeight = 35f;
        public const float buttonGap = 4f;
        public const float extraTopSpace = 83f;

        // margin = 6f

        protected override PawnTableDef PawnTableDef => FP_RSLUM.PawnTableDefOf.FP_RSLUM_MainTable;

        public IEnumerable<Pawn> getPawns()
        {
            return Pawns;
        }

        protected override IEnumerable<Pawn> Pawns
        {
            get
            {
                switch (FP_RSLUM_mod.TabViewPawnCategory)
                {
                    case 0: // colonist
                        return from p in Find.CurrentMap.mapPawns.PawnsInFaction(Faction.OfPlayer)
                               where p.IsColonist
                               select p;
                    case 1: // animal
                        return from p in Find.CurrentMap.mapPawns.PawnsInFaction(Faction.OfPlayer)
                               where p.RaceProps.Animal
                               select p;
                    case 2: // mech
                        return from p in Find.CurrentMap.mapPawns.PawnsInFaction(Faction.OfPlayer)
                               where p.RaceProps.IsMechanoid
                               select p;
                    case 3: // zombie
                        return from p in Find.CurrentMap.mapPawns.PawnsInFaction(Faction.OfPlayer)
                               where p.IsGhoul
                               select p;
                    default:
                        return from p in Find.CurrentMap.mapPawns.PawnsInFaction(Faction.OfPlayer)
                               where p.RaceProps.Animal
                               select p;
                }
            }
        }

        // public override void PostOpen()
        // {
        //     if (this.table == null)
        //     {
        //         this.table = this.CreateTable();
        //     }
        //     this.SetDirty();
        //     Find.World.renderer.wantedMode = WorldRenderMode.None;
        // }
        public override void PostOpen()
        {
            base.PostOpen();
            Find.World.renderer.wantedMode = WorldRenderMode.None;
        }

        public override void DoWindowContents(Rect rect) => base.DoWindowContents(rect);

        // public override void DoWindowContents(Rect rect)
        // {
        //     this.SetInitialSizeAndPosition();
        //     this.DoListShiftButton(new Rect(rect.x, rect.yMin, 200f, 30f));
        //     //this.DoLVUPButton(new Rect(rect.x + 200f, rect.yMin, 200f, 30f));
        //     if (FP_RSLUM_setting.MaxLevel == 0)
        //     {
        //         Widgets.Label(new Rect(rect.x + 200f, rect.yMin, 200f, 30f), "FP_RSLUM_setting_MaxLevel".Translate() + " : " + "Inf");
        //     }
        //     else
        //     {
        //         Widgets.Label(new Rect(rect.x + 200f, rect.yMin, 200f, 30f), "FP_RSLUM_setting_MaxLevel".Translate() + " : " + FP_RSLUM_setting.MaxLevel);
        //     }


        //     this.table.PawnTableOnGUI(new Vector2(rect.x, rect.y + this.ExtraTopSpace + 40f));
        // }

        // public void DoListShiftButton(Rect rect)
        // {
        //     TooltipHandler.TipRegion(rect, Translator.Translate("LvTab_Next"));
        //     if (Widgets.ButtonText(rect, Translator.Translate("LvTab_Next")))
        //     {
        //         this.pawncategory += 1;
        //         if ((this.pawncategory == 2) && !ModsConfig.BiotechActive) // no biotech, no mech level.
        //         {
        //             this.pawncategory = 3;
        //         }
        //         if ((this.pawncategory == 3) && !ModsConfig.AnomalyActive) // no anomaly, no ghoul level.
        //         {
        //             this.pawncategory = 0;
        //         }
        //         if (pawncategory > 3)
        //             pawncategory = 0;
        //         Notify_ResolutionChanged();
        //     }
        // }

        // private PawnTable CreateTable()
        // {
        //     return (PawnTable)Activator.CreateInstance(this.PawnTableDef.workerClass, new object[]
        //     {
        //         this.PawnTableDef,
        //         new Func<IEnumerable<Pawn>>(getPawns),
        //         UI.screenWidth - (int)(this.Margin * 2f),
        //         (int)((float)(UI.screenHeight - 35) - this.ExtraBottomSpace - this.ExtraTopSpace - this.Margin * 2f)
        //     });
        // }

        // public override Vector2 RequestedTabSize
        // {
        //     get
        //     {
        //         if (this.table == null)
        //         {
        //             return Vector2.zero;
        //         }
        //         return new Vector2(this.table.Size.x + this.Margin * 2f, this.table.Size.y + this.ExtraBottomSpace + this.ExtraTopSpace + this.Margin * 2f + 40f);
        //     }
        // }
        // new public void Notify_PawnsChanged()
        // {
        //     base.Notify_PawnsChanged();
        //     this.SetDirty();
        // }
        // public override void Notify_ResolutionChanged()
        // {
        //     this.table = this.CreateTable();
        //     base.Notify_ResolutionChanged();
        // }

        // new protected void SetDirty()
        // {
        //     this.table.SetDirty();
        //     this.SetInitialSizeAndPosition();
        // }
    }
}
