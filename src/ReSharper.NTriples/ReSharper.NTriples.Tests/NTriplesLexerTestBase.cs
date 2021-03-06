// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   NTriplesLexerTestBase.cs
// </summary>
// ***********************************************************************

using System;
using System.IO;
using System.Text;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.TestFramework;
using JetBrains.Text;
using JetBrains.Util;
using JetBrains.Util.Text;
using NUnit.Framework;
using ReSharper.NTriples.Impl;
using ReSharper.NTriples.Parsing;

namespace ReSharper.NTriples.Tests
{
    [Category("Lexer")]
    [TestFileExtension(NTriplesProjectFileType.NTriplesExtension)]
    public abstract class NTriplesLexerTestBase : BaseTestWithSingleProject
    {
        private Encoding myEncoding;
        private string myFileName;

        protected override String RelativeTestDataPath
        {
            get
            {
                return @"Lexing";
            }
        }

        protected ILexer CreateLexer(StreamReader sr)
        {
            return new NTriplesLexer(new StringBuffer(sr.ReadToEnd()));
        }

        protected ILexer CreateLexer(IProjectFile projectFile, StreamReader sr)
        {
            var lexer = this.CreateLexer(sr);
            if (lexer != null)
            {
                return lexer;
            }
            var buffer = new StringBuffer(sr.ReadToEnd());
            var sourceFile = projectFile.ToSourceFile();
            Assert.IsNotNull(sourceFile, "sourceFile == null");
            var lexerFactory = PsiProjectFileTypeCoordinator.Instance.CreateLexerFactory(this.Solution, sourceFile, buffer);
            Assert.IsNotNull(lexerFactory, "lexerFactory == null");
            return lexerFactory.CreateLexer(buffer);
        }

        protected override void DoOneTest(string testName)
        {
            this.DoTestFile(testName);
        }

        protected override void DoTest(IProject testProject)
        {
            this.CheckOutput(this.GetProjectFile(this.myFileName), this.myEncoding);
        }

        protected void DoTestFile(string filename, Encoding encoding = null)
        {
            this.myFileName = filename + this.Extension;
            this.myEncoding = encoding ?? EncodingUtil.CP1251;
            this.DoTestSolution(this.myFileName);
        }

        protected virtual void WriteToken(TextWriter writer, ILexer lexer)
        {
            writer.WriteLine(lexer.TokenType);
        }

        private void CheckOutput(IProjectFile projectFile, Encoding encoding)
        {
            Assert.IsNotNull(projectFile, "projectFile == null");
            this.ExecuteWithGold(
                projectFile.Location.Name,
                sw =>
                {
                    using (var sr = new StreamReader(projectFile.Location.OpenFileForReadingExclusive(), encoding, true))
                    {
                        var lexer = this.CreateLexer(projectFile, sr);
                        int position = 0;
                        lexer.Start();
                        while (lexer.TokenType != null)
                        {
                            Assert.AreEqual(
                                lexer.TokenStart,
                                position,
                                "Token start error. Expected: {0}, actual: {1}",
                                position,
                                lexer.TokenStart);
                            position = lexer.TokenEnd;
                            this.WriteToken(sw, lexer);
                            lexer.Advance();
                        }
                        Assert.AreEqual(lexer.Buffer.Length, position, "position == lexer.Buffer.Length");
                    }
                });
        }
    }
}
