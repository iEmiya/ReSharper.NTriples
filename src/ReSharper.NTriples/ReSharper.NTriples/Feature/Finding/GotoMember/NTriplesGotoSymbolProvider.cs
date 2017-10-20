// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   NTriplesGotoSymbolProvider.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Application;
using JetBrains.Application.UI.Utils;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.CodeCompletion;
using JetBrains.ReSharper.Feature.Services.Navigation.Goto.Misc;
using JetBrains.ReSharper.Feature.Services.Navigation.Goto.ProvidersAPI;
using JetBrains.ReSharper.Feature.Services.Occurrences;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.Text;
using JetBrains.Util;
using ReSharper.NTriples.Cache;
using ReSharper.NTriples.Impl;
using ReSharper.NTriples.Impl.Tree;
using ReSharper.NTriples.Resolve;

namespace ReSharper.NTriples.Feature.Finding.GotoMember
{
    [ShellFeaturePart]
    public class NTriplesGotoSymbolProvider
        : //CachedGotoSymbolBase<NTriplesCache>,
            IGotoSymbolProvider,
            IOccurrenceNavigationProvider,
            //IChainedSymbolProvider,
            //IChainedSearchProvider,
            IApplicableGotoProvider
    {
        /*public IEnumerable<ChainedNavigationItemData> GetNextChainedScopes(
            GotoContext gotoContext, IdentifierMatcher matcher, INavigationScope containingScope, CheckForInterrupt checkCancelled)
        {
            var solution = containingScope.GetSolution();
            var cache = this.GetCache(containingScope, solution, gotoContext);

            return ChainedScopesUtil.GetNextCodeModelScope(matcher, containingScope, checkCancelled, cache, true);
        }
*/

        public IEnumerable<MatchingInfo> FindMatchingInfos(
            IIdentifierMatcher matcher, INavigationScope scope, GotoContext gotoContext, Func<bool> checkCancelled)
        {
            var primaryMembersData = this.GetPrimaryMembers(scope.GetSolution());

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
            MatchingInfo navigationInfo, INavigationScope scope, GotoContext gotoContext, Func<bool> checkCancelled)
        {
            var fileMembersMap = gotoContext.GetData(NTriplesFileMembersMap.NTriplesFileMembersMapKey);
            if (fileMembersMap == null)
            {
                yield break;
            }

            var membersData = fileMembersMap[navigationInfo.Identifier];
            foreach (var clrFileMemberData in membersData)
            {
                var occurrence = this.CreateOccurrence(clrFileMemberData);
                if (occurrence != null)
                {
                    yield return occurrence;
                }
            }
            /*

            ISolution solution = scope.GetSolution();
            var cache = this.GetCache(scope, solution, gotoContext);
            var namespaceNavigationScope = scope as NamespaceNavigationScope;
            if (namespaceNavigationScope != null)
            {
                INamespace namespaceScope = namespaceNavigationScope.DeclaredElement as INamespace;
                if (namespaceScope != null)
                {
                    return (IEnumerable<IOccurrence>)EmptyList<IOccurrence>.InstanceList;
                }
                //return Enumerable.Select<IClrDeclaredElement, IOccurrence>(Enumerable.Where<IClrDeclaredElement>(Enumerable.Where<IClrDeclaredElement>(ChainedScopesUtil.GetAllSubElements(namespaceScope, cache, true), (Func<IClrDeclaredElement, bool>)(element => element.ShortName == navigationInfo.Identifier)), new Func<IClrDeclaredElement, bool>(this.IsDeclaredElementVisible)), (Func<IClrDeclaredElement, IOccurrence>)(x => (IOccurrence)new ChainedCodeModelOccurrence((IDeclaredElement)x, navigationInfo, OccurrencePresentationOptions.DefaultOptions)));
            }
            if (!(scope is SolutionNavigationScope))
            {
                return (IEnumerable<IOccurrence>)EmptyList<IOccurrence>.InstanceList;
            }
            List<IClrDeclaredElement> list1 = new List<IClrDeclaredElement>();
            foreach (ITypeMember typeMember in cache.GetSourceMembers(navigationInfo.Identifier))
            {
                if (!typeMember.IsSynthetic() && !(typeMember is IAccessor))
                {
                    list1.Add((IClrDeclaredElement)typeMember);
                }
            }
            List<IClrDeclaredElement> list2 = new List<IClrDeclaredElement>();
            if (scope.ExtendedSearchFlag == LibrariesFlag.SolutionAndLibraries)
            {
                foreach (ITypeMember typeMember in cache.GetCompiledMembers(navigationInfo.Identifier))
                {
                    list2.Add((IClrDeclaredElement)typeMember);
                }
            }
            IClrDeclaredElement[] elementsByShortName = cache.GetElementsByShortName(navigationInfo.Identifier);
            return
                Enumerable.Select<IClrDeclaredElement, IOccurrence>(
                    Enumerable.Where<IClrDeclaredElement>(
                        Enumerable.Concat<IClrDeclaredElement>(
                            CollectionUtil.Concat<IClrDeclaredElement>(
                                (IEnumerable<IClrDeclaredElement>)list1, elementsByShortName),
                            (IEnumerable<IClrDeclaredElement>)list2),
                        new Func<IClrDeclaredElement, bool>(this.IsDeclaredElementVisible)),
                    (Func<IClrDeclaredElement, IOccurrence>)
                    (x => (IOccurrence)new DeclaredElementOccurrence((IDeclaredElement)x, OccurrenceType.Occurrence)));*/
        }

        public bool IsApplicable(INavigationScope scope, GotoContext gotoContext, IIdentifierMatcher matcher)
        {
            if (!(scope is SolutionNavigationScope))
            {
                return scope is NamespaceNavigationScope;
            }

            return true;
        }

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
                        LocationStyle = GlobalLocationStyle.None
                    });

            if (localName != null)
            {
                localName.ScopeToMainFile = false;
            }

            return declaredElementOccurrence;
        }

        protected NTriplesCache GetCache(ISolution solution)
        {
            // always perform unscoped search
            return solution.GetComponent<NTriplesCache>();
        }

        protected virtual bool IsDeclaredElementVisible(IClrDeclaredElement element)
        {
            return true;
        }

        protected virtual bool IsSourceFileAvailable(IPsiSourceFile sourceFile)
        {
            return sourceFile.IsValid();
        }

        private IEnumerable<NTriplesFileMemberData> GetPrimaryMembers(ISolution solution)
        {
            var cache = solution.GetComponent<NTriplesCache>();
            //var subjects = cache.GetImportantSubjects().ToArray();

            var symbolsByFile = cache.GetAllUriIdentifierSymbolsByFile();
            var services = solution.GetPsiServices();
            foreach (var symbols in symbolsByFile)
            {
                var file = symbols.Key;
                var sourceFile = file.GetPsiFile(NTriplesLanguage.Instance, new DocumentRange(file.Document, 0));
                foreach (var symbol in symbols.Value)
                {
                    var uriIdentifier = new UriIdentifierDeclaredElement(
                        sourceFile, symbol.Namespace, symbol.LocalName, symbol.Info, services, true);
                    yield return new NTriplesFileMemberData(uriIdentifier, ContainerDisplayStyle.NoContainer);
                }
            }
        }

        private IEnumerable<JetTuple<string, bool>> GetQuickSearchTexts(IDeclaredElement declaredElement)
        {
            return new[] { JetTuple.Of(declaredElement.ShortName, true) };
        }
    }
}
