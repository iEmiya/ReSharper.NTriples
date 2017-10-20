// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   StatementSimplificationQuickFixAvailabilityTest.cs
// </summary>
// ***********************************************************************

using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;
using ReSharper.NTriples.CodeInspections.Highlightings;
using ReSharper.NTriples.Impl;
using ReSharper.NTriples.Tree;

namespace ReSharper.NTriples.Tests
{
    [TestFileExtension(NTriplesProjectFileType.NTriplesExtension)]
    [TestFixture]
    public class StatementSimplificationQuickFixAvailabilityTest : QuickFixAvailabilityTestBase
    {
        private readonly string[] files;

        public StatementSimplificationQuickFixAvailabilityTest()
        {
            this.files = this.GetFilesToTest();
        }

        protected override string RelativeTestDataPath
        {
            get
            {
                return @"Intentions\QuickFixes";
            }
        }

        [Test]
        [Ignore]
        [TestCaseSource("files")]
        public void TestQuickFixAvailability(string file)
        {
            this.DoOneTest(file);
        }

        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile psiSourceFile)
        {
            return highlighting is SuggestionRangeHighlighting<ISentence>;
        }
    }
}
