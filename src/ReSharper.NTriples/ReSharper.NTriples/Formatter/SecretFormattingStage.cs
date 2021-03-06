﻿// ***********************************************************************
// <author>Stephan B</author>
// <copyright company="Comindware">
//   Copyright (c) Comindware 2010-2013. All rights reserved.
// </copyright>
// <summary>
//   SecretFormattingStage.cs
// </summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using JetBrains.Application;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Impl.CodeStyle;
using JetBrains.ReSharper.Psi.Secret.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using ReSharper.NTriples.Tree;

namespace JetBrains.ReSharper.Psi.Secret.Formatter
{
    public class SecretFormattingStage
    {
        private readonly SecretFormattingVisitor myFmtVisitor;

        private SecretFormattingStage(CodeFormattingContext context)
        {
            this.myFmtVisitor = CreateFormattingVisitor(context);
        }

        public static void DoFormat(CodeFormattingContext context, IProgressIndicator pi)
        {
            if (context.FirstNode == context.LastNode)
            {
                return;
            }

            var stage = new SecretFormattingStage(context);

            IEnumerable<FormattingRange> nodePairs = GetNodePairs(context);

            IEnumerable<FormatResult<IEnumerable<string>>> spaces = nodePairs.Select(
                range => new FormatResult<IEnumerable<string>>(range, stage.CalcSpaces(new FormattingStageContext(range))));

            FormatterImplHelper.ForeachResult(spaces, pi, res => stage.MakeFormat(res.Range, res.ResultValue));
        }

        private static SecretFormattingVisitor CreateFormattingVisitor(CodeFormattingContext context)
        {
            IPsiSourceFile sourceFile = context.FirstNode.GetSourceFile();
            if (sourceFile != null)
            {
                var projectFileTypeServices = Shell.Instance.GetComponent<IProjectFileTypeServices>();
                var factory = projectFileTypeServices.TryGetService<ISecretCodeFormatterFactory>(sourceFile.LanguageType);
                if (factory != null)
                {
                    return factory.CreateFormattingVisitor(context);
                }
            }

            return new SecretFormattingVisitor(context);
        }

        private static IEnumerable<FormattingRange> GetNodePairs(CodeFormattingContext context)
        {
            ITreeNode firstNode = context.FirstNode;
            ITreeNode lastNode = context.LastNode;
            IList<FormattingRange> list = new List<FormattingRange>();
            GetNodePairs(firstNode, lastNode, list);
            return list;
        }

        private static void GetNodePairs(ITreeNode firstNode, ITreeNode lastNode, IList<FormattingRange> list)
        {
            var firstChild = firstNode;
            var lastChild = lastNode;
            var commonParent = firstNode.FindCommonParent(lastNode);
            while (firstChild != null && firstChild.Parent != commonParent)
            {
                firstChild = firstChild.Parent;
            }
            while (lastChild != null && lastChild.Parent != commonParent)
            {
                lastChild = lastChild.Parent;
            }
            Assertion.Assert(firstChild != null, "firstChild != null");
            Assertion.Assert(lastChild != null, "lastChild != null");
            var node = firstChild;
            while (node != null && node != lastChild.NextSibling)
            {
                if (!node.IsWhitespaceToken())
                {
                    GetNodePairs(node, list, commonParent);
                }
                node = node.NextSibling;
            }
        }

        private static void GetNodePairs(ITreeNode treeNode, IList<FormattingRange> list, ITreeNode parent)
        {
            ITreeNode node = treeNode;
            if (node.FirstChild == null)
            {
                if (! node.IsWhitespaceToken())
                {
                    ITreeNode nextNode = null;
                    while ((node != null) && (nextNode == null))
                    {
                        var sibling = node.NextSibling;
                        while (sibling != null && sibling.IsWhitespaceToken())
                        {
                            sibling = sibling.NextSibling;
                        }
                        if (sibling == null)
                        {
                            node = node.Parent;
                            if (node == parent)
                            {
                                break;
                            }
                        }
                        else
                        {
                            nextNode = sibling;
                        }
                    }
                    if (nextNode != null)
                    {
                        list.Add(new FormattingRange(node, nextNode));
                    }
                }
            }
            else
            {
                ITreeNode child = node.FirstChild;
                {
                    while (child != null)
                    {
                        if (! child.IsWhitespaceToken())
                        {
                            GetNodePairs(child, list, parent);
                        }
                        child = child.NextSibling;
                    }
                }
            }
        }

        private IEnumerable<string> CalcSpaces(FormattingStageContext context)
        {
            var psiTreeNode = context.Parent as INTriplesTreeNode;
            /*if(context.RightChild is IQuantifier)// TODO
      {
        return new  List<string> {""};
      } */
            return psiTreeNode != null
                       ? psiTreeNode.Accept(this.myFmtVisitor, context)
                       : null;
        }

        private void MakeFormat(FormattingRange range, IEnumerable<string> space)
        {
            SecretFormatterHelper.ReplaceSpaces(range.First, range.Last, space);
        }
    }
}
