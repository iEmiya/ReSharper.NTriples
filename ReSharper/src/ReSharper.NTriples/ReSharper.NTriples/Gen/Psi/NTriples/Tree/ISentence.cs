//------------------------------------------------------------------------------
// <auto-generated>
//     Generated by IntelliJ parserGen
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#pragma warning disable 0168, 0219, 0108, 0414
// ReSharper disable RedundantNameQualifier
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
namespace ReSharper.NTriples.Tree {
  public partial interface ISentence : ReSharper.NTriples.Tree.INTriplesTreeNode {
    ReSharper.NTriples.Tree.IDirective Directive { get; }
  
    ReSharper.NTriples.Tree.IStatement Statement { get; }
  
    ReSharper.NTriples.Tree.IDirective SetDirective (ReSharper.NTriples.Tree.IDirective param);
  
    ReSharper.NTriples.Tree.IStatement SetStatement (ReSharper.NTriples.Tree.IStatement param);
  
  }
}