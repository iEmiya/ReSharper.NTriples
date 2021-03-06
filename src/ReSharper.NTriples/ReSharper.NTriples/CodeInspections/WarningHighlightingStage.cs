﻿// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   WarningHighlightingStage.cs
// </summary>
// ***********************************************************************

using System.Collections.Generic;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.UsageChecking;
using JetBrains.Util;

namespace ReSharper.NTriples.CodeInspections
{
    [DaemonStage(StagesBefore = new[] { typeof(ContextErrorHighlightingStage) },
        StagesAfter = new[] { typeof(CollectUsagesStage) })]
    public class WarningHighlightingStage : NTriplesDaemonStageBase
    {
        public override IEnumerable<IDaemonStageProcess> CreateProcess(
            IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind)
        {
            if (!this.IsSupported(process.SourceFile))
            {
                return EmptyList<IDaemonStageProcess>.InstanceList;
            }

            return new List<IDaemonStageProcess>
                {
                    new WarningHighlighterProcess(process, settings)
                };
        }
    }
}
