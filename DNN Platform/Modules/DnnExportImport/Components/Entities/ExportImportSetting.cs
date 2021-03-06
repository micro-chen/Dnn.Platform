﻿// 
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 
using System;

namespace Dnn.ExportImport.Components.Entities
{
    [Serializable]
    public class ExportImportSetting
    {
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public bool SettingIsSecure { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
