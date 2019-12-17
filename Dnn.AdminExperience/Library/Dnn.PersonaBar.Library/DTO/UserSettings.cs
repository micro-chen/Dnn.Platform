﻿// 
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using DotNetNuke.Common.Utilities;

namespace Dnn.PersonaBar.Library.DTO
{
    /// <summary>
    /// Persona Bar Settings For User.
    /// </summary>
    [DataContract]
    public class UserSettings : Dictionary<string, object>, IXmlSerializable
    {
        [IgnoreDataMember]
        public bool ExpandPersonaBar
        {
            get
            {
                return Convert.ToBoolean(this["expandPersonaBar"]);
            }
            set
            {
                this["expandPersonaBar"] = value;
            } 
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            switch (reader.Name)
            {
                case "data":
                    ReadSettings(reader);
                    break;
                default:
                    ReadLegacySettings(reader);
                    break;

            }
        }

        public void WriteXml(XmlWriter writer)
        {            
            writer.WriteStartElement("data");
            writer.WriteCData(Json.Serialize(this));
            writer.WriteEndElement();
        }

        private void ReadSettings(XmlReader reader)
        {
            reader.ReadStartElement("data");

            var settingsData = reader.ReadContentAsString();
            var settings = Json.Deserialize<IDictionary<string, object>>(settingsData);
            foreach (var key in settings.Keys)
            {
                this[key] = settings[key];
            }

            reader.ReadEndElement();
        }

        [Obsolete("The method add for backward compatible, should remove this in future release.")]
        private void ReadLegacySettings(XmlReader reader)
        {
            while(!reader.EOF && reader.NodeType != XmlNodeType.EndElement)
            {
                if (reader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                var settingName = reader.Name;
                reader.ReadStartElement();

                var settingValue = string.Empty;
                if (reader.NodeType == XmlNodeType.Text)
                {
                    settingValue = reader.ReadContentAsString();
                }

                if (!string.IsNullOrEmpty(settingValue))
                {
                    switch (settingName)
                    {
                        case "ExpandPersonaBar":
                            this["expandPersonaBar"] = Convert.ToBoolean(settingValue);
                            break;
                        case "ActiveIdentifier":
                            this["activeIdentifier"] = settingValue;
                            break;
                        case "ActivePath":
                            this["activePath"] = settingValue;
                            break;
                        case "ExpandTasksPane":
                            this["expandTasksPane"] = Convert.ToBoolean(settingValue);
                            break;
                        case "ComparativeTerm":
                            this["comparativeTerm"] = settingValue;
                            break;
                        case "EndDate":
                            this["endDate"] = Convert.ToDateTime(settingValue);
                            break;
                        case "Legends":
                            this["legends"] = settingValue.Split(',');
                            break;
                        case "Period":
                            this["period"] = settingValue;
                            break;
                        case "StartDate":
                            this["startDate"] = Convert.ToDateTime(settingValue);
                            break;
                    }
                }

                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    reader.ReadEndElement();
                }
            }
        }
    }
}