﻿// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   Extensions.cs
// </summary>
// ***********************************************************************

using System.IO;
using System.Linq;
using JetBrains.TestFramework;
using ReSharper.NTriples.Impl;

namespace ReSharper.NTriples.Tests
{
    public static class Extensions
    {
        public static string[] GetFilesToTest(this BaseTestNoShell testClass)
        {
            return testClass.TestDataPath2.GetDirectoryEntries("*" + NTriplesProjectFileType.NTriplesExtension, true)
                            .Select(f => Path.GetFileNameWithoutExtension(f.FullPath))
                            .ToArray();
        }
    }
}
