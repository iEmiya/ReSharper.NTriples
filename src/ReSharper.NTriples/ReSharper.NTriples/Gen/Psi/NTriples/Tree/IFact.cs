//------------------------------------------------------------------------------
// <auto-generated>
//     Generated by IntelliJ parserGen
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#pragma warning disable 0168, 0219, 0108, 0414, 0114
// ReSharper disable RedundantNameQualifier
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
namespace ReSharper.NTriples.Tree {
  public partial interface IFact : ReSharper.NTriples.Tree.INTriplesTreeNode {
    ReSharper.NTriples.Tree.IPredicate Predicate { get; }
  
    JetBrains.ReSharper.Psi.Tree.TreeNodeCollection<ReSharper.NTriples.Tree.IExpression> Objects { get; }
    JetBrains.ReSharper.Psi.Tree.TreeNodeEnumerable<ReSharper.NTriples.Tree.IExpression> ObjectsEnumerable { get; }
    JetBrains.ReSharper.Psi.Tree.TreeNodeCollection<ReSharper.NTriples.Tree.IIdentifier> PredicateIdentifiers { get; }
    JetBrains.ReSharper.Psi.Tree.TreeNodeEnumerable<ReSharper.NTriples.Tree.IIdentifier> PredicateIdentifiersEnumerable { get; }
    ReSharper.NTriples.Tree.IPredicate SetPredicate (ReSharper.NTriples.Tree.IPredicate param);
  
  }
}
