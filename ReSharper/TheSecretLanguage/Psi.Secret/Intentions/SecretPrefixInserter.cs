// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   SecretPrefixInserter.cs
// </summary>
// ***********************************************************************

using System.Diagnostics;
using JetBrains.Application;
using JetBrains.ReSharper.Feature.Services.Intentions.DataProviders;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Secret.Parsing;
using JetBrains.ReSharper.Psi.Secret.Tree;

namespace JetBrains.ReSharper.Psi.Secret.Intentions
{
    internal class SecretPrefixInserter
    {
        private readonly ISentence myDeclarationToAdd;
        private readonly ICreationTarget myTarget;

        public SecretPrefixInserter(ISentence declarationToAdd, ICreationTarget target)
        {
            this.myDeclarationToAdd = declarationToAdd;
            this.myTarget = target;
        }

        public ISentence InsertRule()
        {
            using (WriteLockCookie.Create())
            {
                var anchor = this.myTarget.Anchor;
                Debug.Assert(anchor != null, "anchor != null");
                var parent = anchor.Parent;

                var dot = ModificationUtil.AddChildAfter(parent, anchor, SecretTokenType.CreateDot());
                var whiteSpace = ModificationUtil.AddChildAfter(parent, dot, new NewLine("\n"));
                return ModificationUtil.AddChildAfter(parent, whiteSpace, this.myDeclarationToAdd);
            }
        }
    }
}