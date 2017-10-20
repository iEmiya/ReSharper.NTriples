﻿// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   CreateNTriplesPrefixFromUsage.cs
// </summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.Intentions;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Intentions.CreateFromUsage;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.Util;
using ReSharper.NTriples.CodeInspections.Highlightings;
using ReSharper.NTriples.Resolve;

namespace ReSharper.NTriples.Intentions.CreateFromUsage
{
    [QuickFix]
    internal class CreateNTriplesPrefixFromUsage
        : CreateFromUsageActionBase<NTriplesPrefixReference>, IQuickFix
    {
        public CreateNTriplesPrefixFromUsage(NTriplesUnresolvedReferenceHighlighting<NTriplesPrefixReference> error)
            : base(error.Reference)
        {
        }

        protected override ICreationTarget GetTarget()
        {
            return new CreateNTriplesPrefixTarget(this.Reference);
        }

        protected override IEnumerable<IBulbAction> CreateBulbActions()
        {
            Debug.Assert(Reference != null, "Reference != null");
            yield return new CreatePsiRuleItem(Lazy.Of(this.GetContext), string.Format("Create prefix {0}", this.Reference.GetName()));
        }

        IEnumerable<IntentionAction> IQuickFix.CreateBulbItems()
        {
            return CreateBulbActions().ToContextActionIntentions();
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            return ((Reference != null) && (Reference.IsValid()));
        }

        private CreateNTriplesPrefixContext GetContext()
        {
            return new CreateNTriplesPrefixContext(this.GetTarget() as CreateNTriplesPrefixTarget);
        }
    }
}
