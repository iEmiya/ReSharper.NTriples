﻿using JetBrains.ReSharper.Psi.CSharp.Impl.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Secret.Parsing;

namespace JetBrains.ReSharper.Psi.Secret.Tree
{
    internal class PrefixIdentifier : SecretTokenBase, ISecretIdentifier
    {
        private readonly string myText;

        public PrefixIdentifier(string text)
        {
            this.myText = text;
        }

        public override PsiLanguageType Language
        {
            get
            {
                return SecretLanguage.Instance;
            }
        }

        public string Name
        {
            get
            {
                return CSharpResolveUtil.ReferenceName(this.myText);
            }
        }

        public override NodeType NodeType
        {
            get
            {
                return SecretTokenType.PREFIX_IDENTIFIER;
            }
        }

        public override string GetText()
        {
            return this.myText;
        }

        public override int GetTextLength()
        {
            return this.myText.Length;
        }
    }
}