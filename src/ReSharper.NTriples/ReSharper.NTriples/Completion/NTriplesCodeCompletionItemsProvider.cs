// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   NTriplesCodeCompletionItemsProvider.cs
// </summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Feature.Services.CodeCompletion;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure.LookupItems;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure.LookupItems.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using ReSharper.NTriples.Impl;

namespace ReSharper.NTriples.Completion
{
    [Language(typeof(NTriplesLanguage))]
    internal class NTriplesCodeCompletionItemsProvider : ItemsProviderOfSpecificContext<NTriplesCodeCompletionContext>
    {
        /*/
        protected override void AddItemsGroups(
            NTriplesCodeCompletionContext context, GroupedItemsCollector collector, IntellisenseManager intellisenseManager)
        //*/
        protected override void AddItemsGroups(
            NTriplesCodeCompletionContext context, GroupedItemsCollector collector, IntellisenseManager intellisenseManager)
        {
            collector.AddRanges(
                new TextLookupRanges(
                    context.BasicContext.SelectedRange.TextRange,
                    context.BasicContext.SelectedRange.TextRange));
            collector.AddFilter(new ReferencesBetterFilter());
        }

        //protected override bool AddLookupItems(NTriplesCodeCompletionContext context, GroupedItemsCollector collector)
        protected override bool AddLookupItems(NTriplesCodeCompletionContext context, IItemsCollector collector)
        {
            IReference reference = context.ReparsedContext.Reference;
            if (reference == null)
            {
                return false;
            }

            reference.GetReferenceSymbolTable(false).ForAllSymbolInfos(
                info =>
                {
                    var item = new DeclaredElementLookupItemImpl(
                        new DeclaredElementInstance(info.GetDeclaredElement(), EmptySubstitution.INSTANCE),
                        context,
                        NTriplesLanguage.Instance,
                        context.BasicContext.LookupItemsOwner);
                    item.InitializeRanges(context.Ranges, context.BasicContext);
                    collector.AddAtDefaultPlace(item);
                });

            return true;
        }

        protected override void DecorateItems(NTriplesCodeCompletionContext context, IEnumerable<ILookupItem> items)
        {
            foreach (var item in items.OfType<DeclaredElementLookupItem>())
            {
                item.DisplayName.Text = item.Text;
                item.DisplayName.Text = item.Text;
                item.OrderingString = item.Text.ToLowerInvariant();
            }
        }

        protected override bool IsAvailable(NTriplesCodeCompletionContext context)
        {
            if (
                !((context.BasicContext.CodeCompletionType == CodeCompletionType.BasicCompletion) ||
                  (context.BasicContext.CodeCompletionType == CodeCompletionType.AutomaticCompletion)))
            {
                return false;
            }

            IReference reference = context.ReparsedContext.Reference;
            if (reference == null)
            {
                return false;
            }

            return true;
        }
    }
}
