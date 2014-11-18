using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchedule.Domain
{
    //Base class for all domain objects

    [DataContract]
    public class DomainObject
    {
        #region Import

        /// <summary>
        /// Provides property by property object importing from another objects. Property values 
        /// will be copied if their names and types are equal
        /// </summary>
        /// <typeparam name="T">Type of source object</typeparam>
        /// <param name="sourceObject">Source object</param>
        public void Import<T>(T sourceObject)
        {
            Dictionary<string, PropertyInfo> sourceProperties = new Dictionary<string, PropertyInfo>();

            foreach (PropertyInfo sourcePropertyInfo in typeof(T).GetProperties())
            {
                if (sourcePropertyInfo.CanRead)
                    sourceProperties.Add(sourcePropertyInfo.Name, sourcePropertyInfo);
            }

            foreach (PropertyInfo destinationPropertyInfo in this.GetType().GetProperties())
            {
                if (destinationPropertyInfo.CanWrite
                    && sourceProperties.ContainsKey(destinationPropertyInfo.Name))
                {
                    PropertyInfo sourceProperty = sourceProperties[destinationPropertyInfo.Name];

                    if (sourceProperty.PropertyType == destinationPropertyInfo.PropertyType)
                    {
                        destinationPropertyInfo.SetValue(this,
                                                         sourceProperty.GetValue(sourceObject, null), null);
                    }
                }
            }
        }

        #endregion

        #region Export

        /// <summary>
        /// Provides property by property object exporting to another object. Property values 
        /// will be copied if their names and types are equal
        /// </summary>
        /// <typeparam name="T">Type of output object</typeparam>
        /// <returns>Output object</returns>
        public T Export<T>() where T : new()
        {
            T outputObject = new T();

            Dictionary<string, PropertyInfo> sourceProperties = new Dictionary<string, PropertyInfo>();

            foreach (PropertyInfo sourcePropertyInfo in this.GetType().GetProperties())
            {
                if (sourcePropertyInfo.CanRead)
                    sourceProperties.Add(sourcePropertyInfo.Name, sourcePropertyInfo);
            }

            foreach (PropertyInfo destinationPropertyInfo in typeof(T).GetProperties())
            {
                if (destinationPropertyInfo.CanWrite
                    && sourceProperties.ContainsKey(destinationPropertyInfo.Name))
                {
                    PropertyInfo sourceProperty = sourceProperties[destinationPropertyInfo.Name];

                    if (sourceProperty.PropertyType == destinationPropertyInfo.PropertyType)
                    {
                        destinationPropertyInfo.SetValue(outputObject,
                                                         sourceProperty.GetValue(this, null), null);
                    }
                }
            }

            return outputObject;
        }

        #endregion       
    }
}
