﻿// ***********************************************************************
// <author>Stephan B</author>
// <copyright company="Comindware">
//   Copyright (c) Comindware 2010-2013. All rights reserved.
// </copyright>
// <summary>
//   KeywordsBetterFilter.cs
// </summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Util;
using JetBrains.ReSharper.Feature.Services.Lookup;

namespace JetBrains.ReSharper.Psi.Secret.Completion
{
    internal class KeywordsBetterFilter : ILookupItemsPreference
    {
        public int Order
        {
            get
            {
                return 10;
            }
        }

        public IEnumerable<ILookupItem> FilterItems(ICollection<ILookupItem> items)
        {
            return items.Where(x => x is SecretKeywordLookupItem || x is TemplateLookupItem);
        }
    }
}