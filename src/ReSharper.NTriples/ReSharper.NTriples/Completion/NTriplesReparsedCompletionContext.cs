﻿// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   NTriplesReparsedCompletionContext.cs
// </summary>
// ***********************************************************************

using System.Linq;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.CodeCompletion;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure;
using JetBrains.ReSharper.Feature.Services.Util;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharper.NTriples.Completion
{
    public class NTriplesReparsedCompletionContext : ReparsedCodeCompletionContext
    {
        public NTriplesReparsedCompletionContext([NotNull] IFile file, TreeTextRange range, string newText)
            : base(file, range, newText)
        {
        }

        protected override IReference FindReference(TreeTextRange referenceRange, ITreeNode treeNode)
        {
            return treeNode.FindReferencesAt(referenceRange).FirstOrDefault();
        }

        protected override IReparseContext GetReparseContext(IFile file, TreeTextRange range)
        {
            return new TrivialReparseContext(file, range);
        }
    }
}
