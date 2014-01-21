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
  public static partial class UriIdentifierNavigator {
    [JetBrains.Annotations.Pure]
    [JetBrains.Annotations.CanBeNull]
    [JetBrains.Annotations.ContractAnnotation("null <= null")]
    public static ReSharper.NTriples.Tree.IUriIdentifier GetByLocalName (ReSharper.NTriples.Tree.ILocalName param) {
      if (param == null) return null;
      TreeElement current = (TreeElement)param;
      if (current.parent is ReSharper.NTriples.Impl.Tree.UriIdentifier) {
        if (current.parent.GetChildRole (current) != ReSharper.NTriples.Impl.Tree.UriIdentifier.LOCALNAME) return null;
        current = current.parent;
      } else return null;
      return (ReSharper.NTriples.Tree.IUriIdentifier) current;
    }
    [JetBrains.Annotations.Pure]
    [JetBrains.Annotations.CanBeNull]
    [JetBrains.Annotations.ContractAnnotation("null <= null")]
    public static ReSharper.NTriples.Tree.IUriIdentifier GetByPrefix (ReSharper.NTriples.Tree.IPrefix param) {
      if (param == null) return null;
      TreeElement current = (TreeElement)param;
      if (current.parent is ReSharper.NTriples.Impl.Tree.UriIdentifier) {
        if (current.parent.GetChildRole (current) != ReSharper.NTriples.Impl.Tree.UriIdentifier.PREFIX) return null;
        current = current.parent;
      } else return null;
      return (ReSharper.NTriples.Tree.IUriIdentifier) current;
    }
    [JetBrains.Annotations.Pure]
    [JetBrains.Annotations.CanBeNull]
    [JetBrains.Annotations.ContractAnnotation("null <= null")]
    public static ReSharper.NTriples.Tree.IUriIdentifier GetByUriStringElement (ReSharper.NTriples.Tree.IUriString param) {
      if (param == null) return null;
      TreeElement current = (TreeElement)param;
      if (current.parent is ReSharper.NTriples.Impl.Tree.UriIdentifier) {
        if (current.parent.GetChildRole (current) != ReSharper.NTriples.Impl.Tree.UriIdentifier.URISTRING) return null;
        current = current.parent;
      } else return null;
      return (ReSharper.NTriples.Tree.IUriIdentifier) current;
    }
  }
}
