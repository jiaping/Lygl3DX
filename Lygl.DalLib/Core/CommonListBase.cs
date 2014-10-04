using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;


namespace Lygl.DalLib.Core
{
    /// <summary>
    /// 实现对象列表类，对Csla的ReadOnlyListBase继承
    /// 实现AddItem，RevoeItem来添加移除列表项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="C"></typeparam>
    public class CommonListBase<T, C> : ReadOnlyListBase<T, C> where T : ReadOnlyListBase<T, C>
    {
         public void AddItem(C item)
        {
            try
            {
                IsReadOnly = false;
                base.Add(item);
            }
            finally
            {
                IsReadOnly = true;
            }
        }

         public bool RemoveItem(C item)
        {
            try
            {
                IsReadOnly = false;
                return Remove(item);
            }
            finally
            {
                IsReadOnly = true;
            }
        }
    }
}
