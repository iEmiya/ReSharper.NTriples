using JetBrains.DocumentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.Impl;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.Tree;
using ReSharper.NTriples.Impl;

[assembly: RegisterConfigurableSeverity("WarningRangeHighlighting", null, HighlightingGroupIds.CompilerWarnings,
    "Warning highlighting", @"Warning highlighting", Severity.WARNING, false, Internal = true)]

namespace ReSharper.NTriples.CodeInspections.Highlightings
{
    [ConfigurableSeverityHighlighting("WarningRangeHighlighting", NTriplesLanguage.LanguageName, OverlapResolve = OverlapResolveKind.WARNING)]
    public class WarningRangeHighlighting<TTreeElement>
        : IHighlightingWithRange, ICustomAttributeIdHighlighting where TTreeElement : ITreeNode
    {
        private readonly string myMessage;

        public WarningRangeHighlighting(TTreeElement startElement, TTreeElement endElement, string myMessage)
        {
            this.StartElement = startElement;
            this.EndElement = endElement;
            this.myMessage = myMessage;
        }

        public string AttributeId
        {
            get
            {
                return HighlightingAttributeIds.WARNING_ATTRIBUTE;
            }
        }

        public TTreeElement EndElement { get; private set; }

        public string ErrorStripeToolTip
        {
            get
            {
                return this.myMessage;
            }
        }

        public int NavigationOffsetPatch
        {
            get
            {
                return 0;
            }
        }

        public TTreeElement StartElement { get; private set; }

        public string ToolTip
        {
            get
            {
                return this.myMessage;
            }
        }

        public DocumentRange CalculateRange()
        {
            var sourceFile = this.StartElement.GetSourceFile();
            if (sourceFile != null)
            {
                var range = this.StartElement.GetNavigationRange().JoinRight(this.EndElement.GetNavigationRange());
                return range;
            }

            return DocumentRange.InvalidRange;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}