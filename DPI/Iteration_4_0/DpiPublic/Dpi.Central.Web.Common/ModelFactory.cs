using System;
using System.Configuration;
using System.Reflection;
using DPI.Interfaces;

namespace Dpi.Central.Web
{
    public class ModelFactory
    {
        public static BaseModel Create(string modelTypeKey, Type defaultModelType, IMap imap)
        {
            if (modelTypeKey == null) {
                throw new ArgumentNullException("modelTypeKey");
            }

            if (modelTypeKey.Length == 0) {
                throw new ArgumentException("Model type key can not empty string.", "modelTypeKey");
            }

            if (defaultModelType == null) {
                throw new ArgumentNullException("defaultModelType");
            }

            if (imap == null) {
                throw new ArgumentNullException("imap");
            }

            string modelTypeData = ConfigurationSettings.AppSettings[modelTypeKey];
            if (modelTypeData == null || modelTypeData.Length == 0) {
                return Create(defaultModelType, imap);
            }

            string[] tokens = modelTypeData.Split(',');
            if (tokens.Length != 2) {
                throw new ConfigurationException("Model type key '" + modelTypeKey + "' has invalid value: " + modelTypeData + ".");
            }

            string className = tokens[0].Trim();
            string assemblyPath = tokens[1].Trim();

            return Create(assemblyPath, className, imap);
        }

        private static BaseModel Create(string assemblyPath, string className, IMap imap)
        {
            Assembly assembly = null;

            lock (typeof (ModelFactory)) {
                assembly = Assembly.LoadFrom(assemblyPath);
            }

            Type modelType = assembly.GetType(className, true);

            return Create(modelType, imap);
        }

        private static BaseModel Create(Type modelType, IMap imap)
        {
            ConstructorInfo constructorInfo = modelType.GetConstructor(new Type[] {imap.GetType()});

            if (constructorInfo == null) {
                throw new NotImplementedException("Model type " + modelType.Name + " must have constructor with one parameter of IMap type.");
            }

            object createdObject = constructorInfo.Invoke(new object[] {imap});

            return (BaseModel) createdObject;
        }
    }
}