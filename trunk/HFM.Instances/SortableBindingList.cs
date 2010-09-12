/*
 * HFM.NET - Sortable Binding List Class
 * Copyright (C) 2009-2010 Ryan Harlamert (harlam357)
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

/* 
 * Implementation by Joe Stegman - Microsoft Corporation
 * http://social.msdn.microsoft.com/forums/en-US/winformsdatacontrols/thread/12eb59d3-e687-4e36-93ab-bf6741954d39/
 */ 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using HFM.Framework;

namespace HFM.Instances
{
   [Serializable]
   [CoverageExclude]
   public class SortableBindingList<T> : BindingList<T>, ITypedList
   {
      #region Fields

      protected bool IsSorted { get; set; }
      private ListSortDirection _dir = ListSortDirection.Ascending;
      private bool _sortColumns;

      [NonSerialized]
      private PropertyDescriptorCollection _shape;

      [NonSerialized]
      private PropertyDescriptor _sort;

      #endregion
      
      #region Constructor

      public SortableBindingList()
      {
         /* Default to non-sorted columns */
         _sortColumns = false;

         /* Get shape (only get public properties marked browsable true) */
         _shape = GetShape();
      }

      public SortableBindingList(IList<T> list)
         : base(list)
      {
         /* Default to non-sorted columns */
         _sortColumns = false;

         /* Get shape (only get public properties marked browsable true) */
         _shape = GetShape();
      }

      #endregion

      #region SortedBindingList<T> Column Sorting API

      public bool SortColumns
      {
         get { return _sortColumns; }
         set
         {
            if (value != _sortColumns)
            {
               /* Set Column Sorting */
               _sortColumns = value;

               /* Set shape */
               _shape = GetShape();

               /* Fire MetaDataChanged */
               OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, -1));
            }
         }
      }

      #endregion

      #region BindingList<T> Public Sorting API

      public void Sort()
      {
         ApplySortCore(_sort, _dir);
      }

      public void Sort(string property)
      {
         /* Get the PD */
         _sort = FindPropertyDescriptor(property);

         /* Sort */
         ApplySortCore(_sort, _dir);
      }

      public void Sort(string property, ListSortDirection direction)
      {
         /* Get the sort property */
         _sort = FindPropertyDescriptor(property);
         _dir = direction;

         /* Sort */
         ApplySortCore(_sort, _dir);
      }
      #endregion

      #region BindingList<T> Sorting Overrides

      protected override bool SupportsSortingCore
      {
         get { return true; }
      }

      protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
      {
         var items = Items as List<T>;

         if ((null != items) && (null != property))
         {
            var pc = new PropertyComparer<T>(property, direction);
            items.Sort(pc);

            /* Set sorted */
            IsSorted = true;
         }
         else
         {
            /* Set sorted */
            IsSorted = false;
         }
      }

      protected override bool IsSortedCore
      {
         get { return IsSorted; }
      }

      protected override void RemoveSortCore()
      {
         IsSorted = false;
      }

      #endregion

      #region SortedBindingList<T> Private Sorting API

      protected PropertyDescriptor FindPropertyDescriptor(string property)
      {
         PropertyDescriptor prop = null;

         if (null != _shape)
         {
            prop = _shape.Find(property, true);
         }

         return prop;
      }

      private PropertyDescriptorCollection GetShape()
      {
         /* Get shape (only get public properties marked browsable true) */
         PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(typeof(T), new Attribute[] { new BrowsableAttribute(true) });

         /* Sort if required */
         if (_sortColumns)
         {
            pdc = pdc.Sort();
         }

         return pdc;
      }

      #endregion

      #region PropertyComparer<T>

      [CoverageExclude]
      internal class PropertyComparer<TKey> : IComparer<TKey>
      {
         /*
         * The following code contains code implemented by Rockford Lhotka:
         * msdn.microsoft.com/library/default.asp?url=/library/en-us/dnadvnet/html/vbnet01272004.asp" 
         */

         private readonly PropertyDescriptor _property;
         private readonly ListSortDirection _direction;

         public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
         {
            _property = property;
            _direction = direction;
         }

         public int Compare(TKey xVal, TKey yVal)
         {
            /* Get property values */
            object xValue = GetPropertyValue(xVal, _property);
            object yValue = GetPropertyValue(yVal, _property);

            /* Determine sort order */
            if (_direction == ListSortDirection.Ascending)
            {
               return CompareAscending(xValue, yValue);
            }
            
            return CompareDescending(xValue, yValue);
         }

         //public bool Equals(TKey xVal, TKey yVal)
         //{
         //   return xVal.Equals(yVal);
         //}

         //public int GetHashCode(TKey obj)
         //{
         //   return obj.GetHashCode();
         //}

         /* Compare two property values of any type */
         private static int CompareAscending(object xValue, object yValue)
         {
            int result;

            if (xValue is IComparable)
            {
               /* If values implement IComparer */
               result = ((IComparable)xValue).CompareTo(yValue);
            }
            else if (xValue.Equals(yValue))
            {
               /* If values don't implement IComparer but are equivalent */
               result = 0;
            }
            else
            {
               /* Values don't implement IComparer and are not equivalent, so compare as string values */
               result = xValue.ToString().CompareTo(yValue.ToString());
            }

            /* Return result */
            return result;
         }

         private static int CompareDescending(object xValue, object yValue)
         {
            /* Return result adjusted for ascending or descending sort order ie
               multiplied by 1 for ascending or -1 for descending */
            return CompareAscending(xValue, yValue) * -1;
         }

         private static object GetPropertyValue(TKey value, PropertyDescriptor property)
         {
            /* Get property */
            return property.GetValue(value);
         }
      }

      #endregion

      #region ITypedList Implementation

      public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
      {
         PropertyDescriptorCollection pdc;

         if (null == listAccessors)
         {
            /* Return properties in sort order */
            pdc = _shape;
         }
         else
         {
            /* Return child list shape */
            pdc = ListBindingHelper.GetListItemProperties(listAccessors[0].PropertyType);
         }

         return pdc;
      }

      public string GetListName(PropertyDescriptor[] listAccessors)
      {
         /* Not really used anywhere other than DT and the old DataGrid */
         return typeof(T).Name;
      }

      #endregion
   }
}
