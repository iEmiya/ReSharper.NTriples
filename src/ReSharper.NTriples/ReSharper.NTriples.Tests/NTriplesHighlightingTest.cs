﻿// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   NTriplesHighlightingTest.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using JetBrains.Metadata.Reader.API;
using JetBrains.ProjectModel.Properties;
using JetBrains.ProjectModel.Properties.Common;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;
using ReSharper.NTriples.Impl;
using PlatformID = JetBrains.Application.platforms.PlatformID;

namespace ReSharper.NTriples.Tests
{
    [TestFixture]
    [Category("Highlighting")]
    [TestNetFramework4]
    [TestFileExtension(NTriplesProjectFileType.NTriplesExtension)]
    public class NTriplesHighlightingTest : HighlightingTestBase
    {
        private readonly string[] files;

        public NTriplesHighlightingTest()
        {
            this.files = this.GetFilesToTest();
        }

        protected override PsiLanguageType CompilerIdsLanguage
        {
            get
            {
                return NTriplesLanguage.Instance;
            }
        }

        protected override string RelativeTestDataPath
        {
            get
            {
                return @"Highlighting";
            }
        }

        public override IProjectProperties GetProjectProperties(PlatformID platformId, IReadOnlyCollection<TargetFrameworkId> targetFrameworkIds, ICollection<Guid> flavours)
        {
            return UnknownProjectPropertiesFactory.CreateUnknownProjectProperties(platformId);
        }

        [Test]
        [TestCaseSource("files")]
        public void TestHighlighting(string file)
        {
            this.DoOneTest(file);
        }
    }
}
