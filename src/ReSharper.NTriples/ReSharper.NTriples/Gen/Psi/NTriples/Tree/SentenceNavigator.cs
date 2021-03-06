//------------------------------------------------------------------------------
// <auto-generated>
//     Generated by IntelliJ parserGen
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#pragma warning disable 0168, 0219, 0108, 0414, 0114
// ReSharper disable RedundantNameQualifier
using System.Collections;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using ReSharper.NTriples.Impl.Tree;
namespace ReSharper.NTriples.Tree {
  public static partial class SentenceNavigator {
    [JetBrains.Annotations.Pure]
    [JetBrains.Annotations.CanBeNull]
    [JetBrains.Annotations.ContractAnnotation("null <= null")]
    public static ReSharper.NTriples.Tree.ISentence GetByDirective (ReSharper.NTriples.Tree.IDirective param) {
      if (param == null) return null;
      TreeElement current = (TreeElement)param;
      if (current.parent is ReSharper.NTriples.Impl.Tree.Sentence) {
        if (current.parent.GetChildRole (current) != ReSharper.NTriples.Impl.Tree.Sentence.DIRECTIVE) return null;
        current = current.parent;
      } else return null;
      return (ReSharper.NTriples.Tree.ISentence) current;
    }
    [JetBrains.Annotations.Pure]
    [JetBrains.Annotations.CanBeNull]
    [JetBrains.Annotations.ContractAnnotation("null <= null")]
    public static ReSharper.NTriples.Tree.ISentence GetByStatement (ReSharper.NTriples.Tree.IStatement param) {
      if (param == null) return null;
      TreeElement current = (TreeElement)param;
      if (current.parent is ReSharper.NTriples.Impl.Tree.Sentence) {
        if (current.parent.GetChildRole (current) != ReSharper.NTriples.Impl.Tree.Sentence.STATEMENT) return null;
        current = current.parent;
      } else return null;
      return (ReSharper.NTriples.Tree.ISentence) current;
    }
  }
}
