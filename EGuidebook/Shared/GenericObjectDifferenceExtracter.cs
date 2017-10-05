using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace EGuidebook.Shared
{
    public class GenericObjectDifferenceExtracter
    {
        public class ObjectDifference
        {
            public enum EnumObjectDifferenceType
            {
                EQULAS = 0,
                CHANGED = 1
            }

            public string OldValue { get; private set; }
            public string NewValue { get; private set; }
            public EnumObjectDifferenceType Type { get; private set; }

            public ObjectDifference(string strOldValue, string strNewValue, EnumObjectDifferenceType eType)
            {
                this.OldValue = strOldValue;
                this.NewValue = strNewValue;
                this.Type = eType;
            }
        }

        public List<ObjectDifference> getChanges<T>(T objOld, T objNew)
        {
            Type objType = objOld.GetType();
            PropertyInfo[] arrPropertyInfo = objType.GetProperties();

            List<ObjectDifference> listObjectDifference = new List<ObjectDifference>();

            foreach (PropertyInfo objPropertyInfo in arrPropertyInfo)
            {
                object objOldValue = objPropertyInfo.GetValue(objOld);
                object objNewValue = objPropertyInfo.GetValue(objNew);

                ObjectDifference.EnumObjectDifferenceType eObjectDifferenceType = ObjectDifference.EnumObjectDifferenceType.EQULAS;

                if(objOldValue != null && objNewValue != null)
                {
                    eObjectDifferenceType = objOldValue.Equals(objNewValue) ? ObjectDifference.EnumObjectDifferenceType.EQULAS : ObjectDifference.EnumObjectDifferenceType.CHANGED;
                    listObjectDifference.Add(new ObjectDifference(objOldValue.ToString(), objNewValue.ToString(), eObjectDifferenceType));
                }
                else if(objOldValue != null && objNewValue == null)
                {
                    listObjectDifference.Add(new ObjectDifference(objOldValue.ToString(), "", ObjectDifference.EnumObjectDifferenceType.CHANGED));
                }
                else if (objOldValue == null && objNewValue != null)
                {
                    listObjectDifference.Add(new ObjectDifference("", objNewValue.ToString(), ObjectDifference.EnumObjectDifferenceType.CHANGED));
                }
                else
                {
                    listObjectDifference.Add(new ObjectDifference("", "", ObjectDifference.EnumObjectDifferenceType.EQULAS));
                }
            }

            return listObjectDifference;
        }
    }
}