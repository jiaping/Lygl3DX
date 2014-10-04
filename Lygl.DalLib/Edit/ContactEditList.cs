using Csla;
using Csla.Data;
using Lygl.DalLib.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Lygl.DalLib.Edit
{
    public partial class ContactEditList
    {
        /// <summary>
        /// CriteriaGetByMxInvoicetypeBusinessID criteria.
        /// </summary>
        [Serializable]
        public class CriteriaGetContactByPhone : CriteriaBase<CriteriaGetContactByPhone>
        {
            public static readonly PropertyInfo<int> PhoneTypeProperty = RegisterProperty<int>(p => p.PhoneType);
            public int PhoneType
            {
                get { return ReadProperty(PhoneTypeProperty); }
                set { LoadProperty(PhoneTypeProperty, value); }
            }

            public static readonly PropertyInfo<string> PhoneNumProperty = RegisterProperty<string>(p => p.PhoneNum);
            public string PhoneNum
            {
                get { return ReadProperty(PhoneNumProperty); }
                set { LoadProperty(PhoneNumProperty, value); }
            }


            public CriteriaGetContactByPhone(string phoneNum, int phoneType)
            {
                PhoneNum = phoneNum;
                PhoneType = phoneType;
            }

            /// <summary>
            /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
            /// </summary>
            /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
            public override bool Equals(object obj)
            {
                if (obj is CriteriaGetContactByPhone)
                {
                    var c = (CriteriaGetContactByPhone)obj;
                    if (!PhoneNum.Equals(c.PhoneNum))
                        return false;
                    if (!PhoneType.Equals(c.PhoneType))
                        return false;
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
                return string.Concat("CriteriaGetContactByPhone", PhoneNum, PhoneType.ToString()).GetHashCode();
            }
        }
   
            /// <summary>
            /// Factory method. Loads a <see cref="ContactEditList"/> object, based on given parameters.
            /// </summary>
            /// <param name="name">The Name parameter of the ContactEditList to fetch.</param>
            /// <returns>A reference to the fetched <see cref="ContactEditList"/> object.</returns>
            public static ContactEditList GetContactEditListByPhoneNum(string phoneNum, int phoneType)
            {
                return DataPortal.Fetch<ContactEditList>(new CriteriaGetContactByPhone(phoneNum, phoneType));
            }
            /// <summary>
            /// Loads a <see cref="ContactEditList"/> collection from the database, based on given criteria.
            /// </summary>
            /// <param name="name">The Name.</param>
            protected void DataPortal_Fetch(CriteriaGetContactByPhone crit)
            {
                using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
                {
                    using (var cmd = new SqlCommand("dbo.GetContactEditListByPhoneNum", ctx.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PhoneNum", crit.PhoneNum).DbType = DbType.String;
                        var args = new DataPortalHookArgs(cmd, crit.PhoneNum);
                        OnFetchPre(args);
                        LoadCollection(cmd);
                        OnFetchPost(args);
                    }
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

