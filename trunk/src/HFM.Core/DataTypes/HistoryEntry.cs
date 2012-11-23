﻿/*
 * HFM.NET - History Entry Class
 * Copyright (C) 2009-2012 Ryan Harlamert (harlam357)
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2
 * of the License. See the included file GPLv2.TXT.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Diagnostics;

namespace HFM.Core.DataTypes
{
   [PetaPoco.TableName("WuHistory")]
   [PetaPoco.PrimaryKey("ID")]
   public class HistoryEntry : IEquatable<HistoryEntry>
   {
      public HistoryEntry()
      {
         ProductionView = HistoryProductionView.BonusDownloadTime;
      }

      public long ID { get; set; }
      public int ProjectID { get; set; }
      public int ProjectRun { get; set; }
      public int ProjectClone { get; set; }
      public int ProjectGen { get; set; }
      [PetaPoco.Column("InstanceName")]
      public string Name { get; set; }
      [PetaPoco.Column("InstancePath")]
      public string Path { get; set; }
      public string Username { get; set; }
      public int Team { get; set; }
      public float CoreVersion { get; set; }
      public int FramesCompleted { get; set; }
      [PetaPoco.Ignore]
      public TimeSpan FrameTime
      {
         get { return TimeSpan.FromSeconds(FrameTimeValue); }
      }
      [PetaPoco.Column("FrameTime")]
      public int FrameTimeValue { get; set; }
      [PetaPoco.Ignore]
      public string Result
      {
         get { return ResultValue.ToWorkUnitResultString(); }
      }
      [PetaPoco.Column("Result")]
      public int ResultValue { get; set; }
      public DateTime DownloadDateTime { get; set; }
      public DateTime CompletionDateTime { get; set; }

      private Protein _protein;

      [PetaPoco.Ignore]
      public string WorkUnitName { get { return _protein == null ? String.Empty : _protein.WorkUnitName; } }
      [PetaPoco.Ignore]
      public double KFactor { get { return _protein == null ? 0 : _protein.KFactor; } }
      [PetaPoco.Ignore]
      public string Core { get { return _protein == null ? String.Empty : _protein.Core; } }
      [PetaPoco.Ignore]
      public int Frames { get { return _protein == null ? 0 : _protein.Frames; } }
      [PetaPoco.Ignore]
      public int Atoms { get { return _protein == null ? 0 : _protein.NumberOfAtoms; } }

      [PetaPoco.Ignore]
      public string SlotType { get; private set; }
      
      [PetaPoco.Ignore]
      public HistoryProductionView ProductionView { get; set; }

      [PetaPoco.Ignore]
      public double PPD
      {
         get
         {
            if (_protein == null) return 0;
         
            switch (ProductionView)
            {
               case HistoryProductionView.Standard:
                  return _protein.GetPPD(FrameTime);
               case HistoryProductionView.BonusFrameTime:
                  return _protein.GetPPD(FrameTime, TimeSpan.FromSeconds(FrameTime.TotalSeconds * Frames), true);
               case HistoryProductionView.BonusDownloadTime:
                  return _protein.GetPPD(FrameTime, CompletionDateTime.Subtract(DownloadDateTime), true);
               default:
                  // ReSharper disable HeuristicUnreachableCode
                  Debug.Assert(false);
                  return 0;
                  // ReSharper restore HeuristicUnreachableCode
            }
         }
      }

      [PetaPoco.Ignore]
      public double Credit
      {
         get
         {
            if (_protein == null) return 0;

            switch (ProductionView)
            {
               case HistoryProductionView.Standard:
                  return _protein.Credit;
               case HistoryProductionView.BonusFrameTime:
                  return _protein.GetCredit(TimeSpan.FromSeconds(FrameTime.TotalSeconds * Frames), true);
               case HistoryProductionView.BonusDownloadTime:
                  return _protein.GetCredit(CompletionDateTime.Subtract(DownloadDateTime), true);
               default:
                  // ReSharper disable HeuristicUnreachableCode
                  Debug.Assert(false);
                  return 0;
                  // ReSharper restore HeuristicUnreachableCode
            }
         }
      }
      
      public HistoryEntry SetProtein(Protein protein)
      {
         _protein = protein;
         if (protein != null)
         {
            SlotType = protein.Core.ToSlotType().ToString();
         }

         return this;
      }

      #region IEquatable<HistoryEntry> Members

      public bool Equals(HistoryEntry other)
      {
         if (other == null) return false;

         return (ProjectID == other.ProjectID &&
                 ProjectRun == other.ProjectRun &&
                 ProjectClone == other.ProjectClone &&
                 ProjectGen == other.ProjectGen &&
                 DownloadDateTime == other.DownloadDateTime);
      }

      #endregion
   }
}
