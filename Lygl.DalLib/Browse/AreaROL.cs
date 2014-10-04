using Lygl.DalLib.Util;
using System;
using System.Linq;

namespace Lygl.DalLib.Browse
{
    public partial class AreaROL
    {
        public void Add(Guid areaID)
        {
            AreaRO areaRO = AreaROL.GetAreaROLByID(areaID).First();
            try
            {
                IsReadOnly = false;
                Add(areaRO);
            }
            finally
            {
                IsReadOnly = true;
            }
        }

        public bool Remove(Guid areaID)
        {
            AreaRO areaRO = this.FindAreaROByAreaID(areaID);
            try
            {
                IsReadOnly = false;
                return Remove(areaRO);
            }
            finally
            {
                IsReadOnly = true;
            }
        }
        #region OnDeserialized actions

        /*/// <summary>
        /// This method is called on a newly deserialized object
        /// after deserialization is complete.
        /// </summary>
        protected override void OnDeserialized()
        {
            base.OnDeserialized();
            // add your custom OnDeserialized actions here.
        }*/

        #endregion

        #region Pseudo Event Handlers

        //partial void OnFetchPre(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        //partial void OnFetchPost(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        #endregion

    }
}
