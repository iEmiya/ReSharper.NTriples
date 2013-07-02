// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   NTriplesIntentionResultBehavior.cs
// </summary>
// ***********************************************************************

using JetBrains.Application;
using JetBrains.DataFlow;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;
using JetBrains.TextControl;

namespace ReSharper.NTriples.Intentions.CreateFromUsage
{
    [ShellComponent]
    public class NTriplesIntentionResultBehavior
    {
        public NTriplesIntentionResultBehavior(HotspotSessionExecutor hotspotSessionExecutor)
        {
            this.HotspotSessionExecutor = hotspotSessionExecutor;
        }

        private HotspotSessionExecutor HotspotSessionExecutor { get; set; }

        public void OnHotspotSessionExecutionStarted(NTriplesIntentionResult result, ITextControl textControl)
        {
            this.OnHotspotSessionExecutionStartedInternal(result, textControl);
        }

        protected static void SetCaretPosition(ITextControl textControl, NTriplesIntentionResult result)
        {
            if (result.PreferredSelection != DocumentRange.InvalidRange)
            {
                textControl.Selection.SetRange(result.PreferredSelection.TextRange);
            }
        }

        protected virtual void OnHotspotSessionExecutionStartedInternal(NTriplesIntentionResult result, ITextControl textControl)
        {
            var hotspotSessionUi = this.HotspotSessionExecutor.CurrentSession;
            if (hotspotSessionUi == null)
            {
                SetCaretPosition(textControl, result);
            }
            else
            {
                hotspotSessionUi.HotspotSession.Closed.Advise(
                    EternalLifetime.Instance,
                    args =>
                    {
                        if (args.TerminationType != TerminationType.Finished)
                        {
                            return;
                        }

                        SetCaretPosition(textControl, result);
                    });
            }
        }
    }
}
