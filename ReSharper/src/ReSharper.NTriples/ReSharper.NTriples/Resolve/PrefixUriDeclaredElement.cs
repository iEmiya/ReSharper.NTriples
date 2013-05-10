﻿using System.Collections.Generic;
using System.Xml;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using JetBrains.Util.DataStructures;
using ReSharper.NTriples.Impl;

namespace ReSharper.NTriples.Resolve
{
    internal class PrefixUriDeclaredElement : IDeclaredElement
    {
        private readonly IFile myFile;
        private readonly IPsiServices myServices;
        private string myName;
        private string myNewName;

        public PrefixUriDeclaredElement(IFile file, string name, IPsiServices services)
        {
            this.myFile = file;
            this.myName = name;
            this.myNewName = name;
            this.myServices = services;
        }

        public bool CaseSensistiveName
        {
            get
            {
                return true;
            }
        }

        public bool ChangeName { get; set; }

        public IFile File
        {
            get
            {
                return this.myFile;
            }
        }

        public string NewName
        {
            get
            {
                return this.myNewName;
            }
            set
            {
                this.ChangeName = true;
                this.myNewName = value;
            }
        }

        public PsiLanguageType PresentationLanguage
        {
            get
            {
                return NTriplesLanguage.Instance;
            }
        }

        public string ShortName
        {
            get
            {
                return this.myName;
            }
        }

        public IList<IDeclaration> GetDeclarations()
        {
            return EmptyList<IDeclaration>.InstanceList;
        }

        public IList<IDeclaration> GetDeclarationsIn(IPsiSourceFile sourceFile)
        {
            return EmptyList<IDeclaration>.InstanceList;
        }

        public DeclaredElementType GetElementType()
        {
            return NTriplesDeclaredElementType.PrefixUri;
        }

        public IPsiServices GetPsiServices()
        {
            return this.myServices;
        }

        public HybridCollection<IPsiSourceFile> GetSourceFiles()
        {
            return new HybridCollection<IPsiSourceFile>
                {
                    this.myFile.GetSourceFile()
                };
        }

        public XmlNode GetXMLDescriptionSummary(bool inherit)
        {
            return null;
        }

        public XmlNode GetXMLDoc(bool inherit)
        {
            return null;
        }

        public bool HasDeclarationsIn(IPsiSourceFile sourceFile)
        {
            return sourceFile == this.myFile.GetSourceFile();
        }

        public bool IsSynthetic()
        {
            return false;
        }

        public bool IsValid()
        {
            return true;
        }

        public void SetName(string name)
        {
            this.myName = name;
        }
    }
}