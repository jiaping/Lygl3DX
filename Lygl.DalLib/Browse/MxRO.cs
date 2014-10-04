using Lygl.DalLib.Core;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Browse
{
    public partial class MxRO
    {
        string IMx.MxID
        {
            get
            {
                return this.MxID.ToString();
            }
        }
        string IMx.GetImageName()
        {
            if (MxStatusID < 40)
                return string.Format("{0}_{1}", this.MxType, this.MxStyleID);
            else
                return string.Format("{0}_{1}_ÒÑÁ¢±®", this.MxType, this.MxStyleID);
        }

        string IVisualLableEntity.GetLableText()
        {
            return string.Format("{0}\n{1}\n{2}\n{3}", new object[] { MxName, MxStatus, MxType, SzName });

        }
        #region OnDeserialized actions

        /*/// <summary>
        /// This method is called on a newly deserialized object
        /// after deserialization is complete.
        /// </summary>
        /// <param name="context">Serialization context object.</param>
        protected override void OnDeserialized(System.Runtime.Serialization.StreamingContext context)
        {
            base.OnDeserialized(context);
            // add your custom OnDeserialized actions here.
        }*/

        #endregion

        #region Pseudo Event Handlers

        //partial void OnFetchRead(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        #endregion

    }
}
