// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   NTriplesErrorElementHighlighting.cs
// </summary>
// ***********************************************************************

using JetBrains.DocumentModel;
//using JetBrains.ReSharper.Daemon;
//using JetBrains.ReSharper.Daemon.Impl;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharper.NTriples.CodeInspections.Highlightings
{
    [StaticSeverityHighlighting(Severity.ERROR, "Syntax errors", OverlapResolve = OverlapResolveKind.ERROR,
        ShowToolTipInStatusBar = false)]
    internal class NTriplesErrorElementHighlighting : IHighlightingWithRange, ICustomAttributeIdHighlighting
    {
        private const string AtributeId = HighlightingAttributeIds.ERROR_ATTRIBUTE;
        private readonly ITreeNode myElement;

        public NTriplesErrorElementHighlighting(ITreeNode element)
        {
            this.myElement = element;
        }

        public string AttributeId
        {
            get
            {
                return AtributeId;
            }
        }

        public string ErrorStripeToolTip
        {
            get
            {
                var errorMessage = this.myElement is IErrorElement
                                       ? string.Format("Syntax error: {0}.", (this.myElement as IErrorElement).ErrorDescription)
                                       : "Syntax error";
                return errorMessage;
            }
        }

        public int NavigationOffsetPatch
        {
            get
            {
                return 0;
            }
        }

        public string ToolTip
        {
            get
            {
                return this.ErrorStripeToolTip;
            }
        }

        public DocumentRange CalculateRange()
        {
            return this.myElement.GetNavigationRange();
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
