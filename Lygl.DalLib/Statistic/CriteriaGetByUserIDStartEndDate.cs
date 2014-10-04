using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace Lygl.DalLib.Statistic
{
    /// <summary>
    /// 用于查询统计中的，根据操作员，开始，结束日期，来获取相关数据，
    /// CriteriaGetByUserIDStartEndDate criteria.
    /// </summary>
    [Serializable]
    public class CriteriaGetByUserIDStartEndDate : CriteriaBase<CriteriaGetByUserIDStartEndDate>
    {
        public static readonly PropertyInfo<string> UserIDProperty = RegisterProperty<string>(p => p.UserID);
        public string UserID
        {
            get { return ReadProperty(UserIDProperty); }
            private set { LoadProperty(UserIDProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> StartDateProperty = RegisterProperty<DateTime>(p => p.StartDate);
        public DateTime StartDate
        {
            get { return ReadProperty(StartDateProperty); }
            private set { LoadProperty(StartDateProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> EndDateProperty = RegisterProperty<DateTime>(p => p.EndDate);
        public DateTime EndDate
        {
            get { return ReadProperty(EndDateProperty); }
            private set { LoadProperty(EndDateProperty, value); }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaGetByMxName"/> class.
        /// </summary>
        /// <param name="mxName">The MxName.</param>
        public CriteriaGetByUserIDStartEndDate(DateTime startDate, DateTime endDate, string userID = "")
        {
            UserID = userID;
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CriteriaGetByUserIDStartEndDate)
            {
                var c = (CriteriaGetByUserIDStartEndDate)obj;
                if (StartDate.Equals(c.StartDate) && EndDate.Equals(c.EndDate) && UserID.Equals(c.UserID))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>An hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return string.Concat("CriteriaGetByUserIDStartEndDate", StartDate.ToString(), EndDate.ToString(), UserID).GetHashCode();
        }
    }
}
