﻿/*******************************************************************************
 *  Copyright 2008-2014 OBS.com, Inc. or its affiliates. All Rights Reserved.
 *  Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *  this file except in compliance with the License. A copy of the License is located at
 *  or in the "license" file accompanying this file.
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *  specific language governing permissions and limitations under the License.
 * *****************************************************************************
 */

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;

using OBS.Util;
using System.Xml;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace OBS
{
    internal class CustomUrlResovler : XmlUrlResolver
    {
        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            Uri uri = new Uri(baseUri, relativeUri);
            if (IsUnsafeHost(uri.Host))
                return null;

            return base.ResolveUri(baseUri, relativeUri);
        }

        private bool IsUnsafeHost(string host)
        {
            return false;
        }
    }

    /// <summary>
    /// Root OBS config section
    /// </summary>
    internal class OBSSection : ConfigurationSection
    {
        private const string loggingKey = "logging";        
        private const string s3Key = "s3";        
        private const string defaultsKey = "defaults";
        private const string endpointDefinitionKey = "endpointDefinition";
        private const string regionKey = "region";
        private const string proxy = "proxy";
        private const string profileNameKey = "profileName";
        private const string profilesLocationKey = "profilesLocation";

        [ConfigurationProperty(loggingKey)]
        public LoggingSection Logging
        {
            get { return (LoggingSection)this[loggingKey]; }
            set { this[loggingKey] = value; }
        }

        [ConfigurationProperty(s3Key)]
        public V4ClientSection S3
        {
            get { return (V4ClientSection)this[s3Key]; }
            set { this[s3Key] = value; }
        }

        [ConfigurationProperty(endpointDefinitionKey)]
        public string EndpointDefinition
        {
            get { return (string)this[endpointDefinitionKey]; }
            set { this[endpointDefinitionKey] = value; }
        }

        [ConfigurationProperty(regionKey)]
        public string Region
        {
            get { return (string)this[regionKey]; }
            set { this[regionKey] = value; }
        }

        [ConfigurationProperty(profileNameKey)]
        public string ProfileName
        {
            get { return (string)this[profileNameKey]; }
            set { this[profileNameKey] = value; }
        }

        [ConfigurationProperty(profilesLocationKey)]
        public string ProfilesLocation
        {
            get { return (string)this[profilesLocationKey]; }
            set { this[profilesLocationKey] = value; }
        }

        /// <summary>
        /// Gets and sets the proxy settings for the SDK to use.
        /// </summary>
        [ConfigurationProperty(proxy)]
        public ProxySection Proxy
        {
            get { return (ProxySection)this[proxy]; }
            set { this[proxy] = value; }
        }

        internal string Serialize(string name)
        {
            try
            {
                string basicXml = this.SerializeSection(null, name, ConfigurationSaveMode.Full);
                XmlUrlResolver resolver = new XmlUrlResolver();
                resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;
                
                XmlDocument doc = new XmlDocument();
                doc.XmlResolver = new CustomUrlResovler();
                doc.LoadXml(basicXml);

                using (var textWriter = new StringWriter(CultureInfo.InvariantCulture))
                {
                    CleanUpNodes(doc);

                    doc.Save(textWriter);
                    return textWriter.ToString();
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        internal static void CleanUpNodes(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                var toRemove = child.Attributes.Cast<XmlAttribute>().Where(at => string.IsNullOrEmpty(at.Value)).ToList();
                foreach (var rem in toRemove) child.Attributes.Remove(rem);

                CleanUpNodes(child);
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    }

    #region Basic sections

    /// <summary>
    /// V4-enabling section
    /// </summary>
    internal class V4ClientSection : WritableConfigurationElement
    {
        private const string useSignatureVersion4Key = "useSignatureVersion4";

        [ConfigurationProperty(useSignatureVersion4Key)]
        public bool? UseSignatureVersion4
        {
            get { return (bool?)this[useSignatureVersion4Key]; }
            set { this[useSignatureVersion4Key] = value; }
        }
    }

    /// <summary>
    /// Settings for configuring a proxy for the SDK to use.
    /// </summary>
    internal class ProxySection : WritableConfigurationElement
    {
        private const string hostSection = "host";
        private const string portSection = "port";
        private const string usernameSection = "username";
        private const string passwordSection = "password";

        /// <summary>
        /// Gets and sets the host name or IP address of the proxy server.
        /// </summary>
        [ConfigurationProperty(hostSection)]
        public string Host
        {
            get { return (string)this[hostSection]; }
            set { this[hostSection] = value; }
        }

        /// <summary>
        /// Gets and sets the port number of the proxy.
        /// </summary>
        [ConfigurationProperty(portSection)]
        public int? Port
        {
            get { return (int?)this[portSection]; }
            set { this[portSection] = value; }
        }

        /// <summary>
        /// Gets and sets the username to authenticate with the proxy server.
        /// </summary>
        [ConfigurationProperty(usernameSection)]
        public string Username
        {
            get { return (string)this[usernameSection]; }
            set { this[usernameSection] = value; }
        }

        /// <summary>
        /// Gets and sets the password to authenticate with the proxy server.
        /// </summary>
        [ConfigurationProperty(passwordSection)]
        public string Password
        {
            get { return (string)this[passwordSection]; }
            set { this[passwordSection] = value; }
        }

    }

    /// <summary>
    /// Logging section
    /// </summary>
    internal class LoggingSection : WritableConfigurationElement
    {
        private const string logToKey = "logTo";
        private const string logResponsesKey = "logResponses";
        private const string logMetricsKey = "logMetrics";
        private const string logMetricsFormatKey = "logMetricsFormat";
        private const string logMetricsCustomFormatterKey = "logMetricsCustomFormatter";

        [ConfigurationProperty(logToKey)]
        public LoggingOptions LogTo
        {
            get { return (LoggingOptions)this[logToKey]; }
            set { this[logToKey] = value; }
        }

        [ConfigurationProperty(logResponsesKey)]
        public ResponseLoggingOption LogResponses
        {
            get { return (ResponseLoggingOption)this[logResponsesKey]; }
            set { this[logResponsesKey] = value; }
        }

        [ConfigurationProperty(logMetricsKey)]
        public bool? LogMetrics
        {
            get { return (bool?)this[logMetricsKey]; }
            set { this[logMetricsKey] = value; }
        }

        [ConfigurationProperty(logMetricsFormatKey)]
        public LogMetricsFormatOption LogMetricsFormat
        {
            get { return (LogMetricsFormatOption)this[logMetricsFormatKey]; }
            set { this[logMetricsFormatKey] = value; }
        }

        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty(logMetricsCustomFormatterKey)]
        public Type LogMetricsCustomFormatter
        {
            get { return (Type)this[logMetricsCustomFormatterKey]; }
            set { this[logMetricsCustomFormatterKey] = value; }
        }
    }

    #endregion

    #region Abstract helper classes

    /// <summary>
    /// Easy-to-use generic collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class WritableConfigurationElementCollection<T> : ConfigurationElementCollection
    where T : SerializableConfigurationElement, new()
    {
        protected abstract string ItemPropertyName { get; }

        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new T();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return element;
        }
        protected override string ElementName { get { return string.Empty; } }
        protected override bool IsElementName(string elementName)
        {
            return (string.Equals(elementName, ItemPropertyName, StringComparison.Ordinal));
        }
        public override ConfigurationElementCollectionType CollectionType { get { return ConfigurationElementCollectionType.BasicMapAlternate; } }
        public override bool IsReadOnly()
        {
            return false;
        }

        public WritableConfigurationElementCollection() : base() { }

        public void Add(T t)
        {
            this.BaseAdd(t);
        }
        public void Add(T[] ts)
        {
            foreach (var t in ts)
            {
                Add(t);
            }
        }

        protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
        {
            if (writer == null)
                return base.SerializeElement(writer, serializeCollectionKey);

            for (int i = 0; i < this.Count; i++)
            {
                var item = BaseGet(i) as SerializableConfigurationElement;
                if (item == null)
                {
                    return false;
                }
                writer.WriteStartElement(ItemPropertyName);
                item.SerializeElement(writer, serializeCollectionKey);
                writer.WriteEndElement();
            }

            return (Count > 0);
        }

        public List<T> Items { get { return this.Cast<T>().ToList(); } }
    }

    /// <summary>
    /// Configuration element that serializes properly when used with collections
    /// </summary>
    internal abstract class SerializableConfigurationElement : WritableConfigurationElement
    {
        new public bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
        {
            return base.SerializeElement(writer, serializeCollectionKey);
        }
    }

    /// <summary>
    /// ConfigurationElement class which returns false for IsReadOnly
    /// </summary>
    internal abstract class WritableConfigurationElement : ConfigurationElement
    {
        public override bool IsReadOnly()
        {
            return false;
        }
    }

    #endregion
}
