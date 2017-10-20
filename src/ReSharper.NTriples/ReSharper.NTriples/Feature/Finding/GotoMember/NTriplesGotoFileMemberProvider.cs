// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   NTriplesGotoFileMemberProvider.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
//using System.Text;
using JetBrains.Annotations;
using JetBrains.Application;
using JetBrains.Application.UI.Utils;
//using JetBrains.CommonControls;
//using JetBrains.IDE;
//using JetBrains.ProjectModel;
//using JetBrains.ProjectModel.Model2.Assemblies.Interfaces;
using JetBrains.ReSharper.Feature.Services.CodeCompletion;
//using JetBrains.ReSharper.Feature.Services.Goto;
//using JetBrains.ReSharper.Feature.Services.Navigation;
using JetBrains.ReSharper.Feature.Services.Navigation.Goto.Misc;
using JetBrains.ReSharper.Feature.Services.Navigation.Goto.ProvidersAPI;
//using JetBrains.ReSharper.Feature.Services.Navigation.Search;
//using JetBrains.ReSharper.Feature.Services.Occurences;
using JetBrains.ReSharper.Feature.Services.Occurrences;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Files;
//using JetBrains.ReSharper.Psi.Search;
using JetBrains.Text;
//using JetBrains.UI.PopupWindowManager;
//using JetBrains.UI.RichText;
using JetBrains.Util;
using ReSharper.NTriples.Cache;
//using ReSharper.NTriples.Impl;
using ReSharper.NTriples.Impl.Tree;
using ReSharper.NTriples.Resolve;

namespace ReSharper.NTriples.Feature.Finding.GotoMember
{
    [ShellFeaturePart]
    public class NTriplesGotoFileMemberProvider : IGotoFileMemberProvider
    {
        [CanBeNull]
        protected IOccurrence CreateOccurrence(NTriplesFileMemberData fileMemberData)
        {
            var localName = fileMemberData.Element as LocalName;
            if (localName != null)
            {
                localName.ScopeToMainFile = true;
            }

            var declaredElementOccurrence = new DeclaredElementOccurrence(
                fileMemberData.Element,
                new OccurrencePresentationOptions
                    {
                        ContainerStyle = !(fileMemberData.Element is ITypeElement)
                                             ? fileMemberData.ContainerDisplayStyle
                                             : ContainerDisplayStyle.NoContainer,
                        LocationStyle = GlobalLocationStyle.None,
                        TextDisplayStyle = TextDisplayStyle.IdentifierAndContext
                    });

            if (localName != null)
            {
                localName.ScopeToMainFile = false;
            }

            return declaredElementOccurrence;
        }

        protected virtual bool IsSourceFileAvailable(IPsiSourceFile sourceFile)
        {
            return sourceFile.IsValid();
        }

        private IEnumerable<NTriplesFileMemberData> GetPrimaryMembers(FileMemberNavigationScope fileMemberScope)
        {
            var primarySourceFile = fileMemberScope.GetPrimarySourceFile();
            if (!this.IsSourceFileAvailable(primarySourceFile))
            {
                return EmptyList<NTriplesFileMemberData>.InstanceList;
            }

            var file = primarySourceFile.GetPrimaryPsiFile() as NTriplesFile;
            if (file == null)
            {
                return EmptyList<NTriplesFileMemberData>.InstanceList;
            }

            var primaryMembers = new LinkedList<NTriplesFileMemberData>();
            foreach (var declaredElement in file.GetAllPrefixDeclaredElements())
            {
                primaryMembers.AddFirst(new NTriplesFileMemberData(declaredElement, ContainerDisplayStyle.Namespace));
            }

            var subjects = file.GetAllUriIdentifierDeclaredElements().Where(e => ((IUriIdentifierDeclaredElement)e).GetKind() == IdentifierKind.Subject);
            foreach (var declaredElement in subjects)
            {
                primaryMembers.AddFirst(new NTriplesFileMemberData(declaredElement, ContainerDisplayStyle.Namespace));
            }

            return primaryMembers;
        }

        private IEnumerable<JetTuple<string, bool>> GetQuickSearchTexts(IDeclaredElement declaredElement)
        {
            return new[] { JetTuple.Of(declaredElement.ShortName, true) };
        }

        public bool IsApplicable(INavigationScope scope, GotoContext gotoContext, IIdentifierMatcher matcher)
        {
            return true;
        }

        public IEnumerable<MatchingInfo> FindMatchingInfos(
            IIdentifierMatcher matcher,
            INavigationScope scope,
            GotoContext gotoContext,
            Func<bool> checkCancelled)
        {
            var fileMemberScope = scope as FileMemberNavigationScope;
            if (fileMemberScope == null)
            {
                return EmptyList<MatchingInfo>.InstanceList;
            }

            var primaryMembersData = this.GetPrimaryMembers(fileMemberScope);

            var fileMembersMap = new NTriplesFileMembersMap();

            var result = new Collection<MatchingInfo>();
            foreach (var data in primaryMembersData)
            {
                var quickSearchTexts = this.GetQuickSearchTexts(data.Element);
                var matchedText = quickSearchTexts.FirstOrDefault(tuple => matcher.Matches(tuple.A));
                if (matchedText == null)
                {
                    continue;
                }

                fileMembersMap.Add(matchedText.A, data);

                var matchingIndicies = matchedText.B
                    ? matcher.MatchingIndicies(matchedText.A)
                    : EmptyArray<CombinedLookupItem.IdentifierMatch>.Instance;
                result.Add(
                    new MatchingInfo(
                        matchedText.A,
                        matcher.Filter == "*"
                            ? EmptyList<CombinedLookupItem.IdentifierMatch>.InstanceList
                            : matchingIndicies,
                        matchedText.B));
            }

            gotoContext.PutData(NTriplesFileMembersMap.NTriplesFileMembersMapKey, fileMembersMap);
            return result;
        }

        public IEnumerable<IOccurrence> GetOccurrencesByMatchingInfo(
            MatchingInfo navigationInfo,
            INavigationScope scope,
            GotoContext gotoContext,
            Func<bool> checkForInterrupt)
        {
            var fileMembersMap = gotoContext.GetData(NTriplesFileMembersMap.NTriplesFileMembersMapKey);
            if (fileMembersMap == null)
            {
                yield break;
            }

            var membersData = fileMembersMap[navigationInfo.Identifier];
            foreach (var clrFileMemberData in membersData)
            {
                var occurence = this.CreateOccurrence(clrFileMemberData);
                if (occurence != null)
                {
                    yield return occurence;
                }
            }
        }
    }
}
