//------------------------------------------------------------------------------
// <auto-generated>
//     Generated by IntelliJ parserGen
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#pragma warning disable 0168, 0219, 0108, 0414, 0114
// ReSharper disable RedundantNameQualifier
using System;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using ReSharper.NTriples.Tree;
using ReSharper.NTriples.Parsing;
namespace ReSharper.NTriples.Impl.Tree {
  internal partial class Meta : NTriplesCompositeElement, ReSharper.NTriples.Tree.IMeta {
    internal Meta() : base() {
    }
    public override JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeType NodeType {
      get { return ReSharper.NTriples.Impl.Tree.ElementType.META; }
    }
    public override void Accept(ReSharper.NTriples.Tree.TreeNodeVisitor visitor) {
      visitor.VisitMeta(this);
    }
    public override void Accept<TContext>(ReSharper.NTriples.Tree.TreeNodeVisitor<TContext> visitor, TContext context) {
      visitor.VisitMeta(this, context);
    }
    public override TReturn Accept<TContext, TReturn>(ReSharper.NTriples.Tree.TreeNodeVisitor<TContext, TReturn> visitor, TContext context) {
      return visitor.VisitMeta(this, context);
    }
    public override string ToString() {
      return "IMeta";
    }
  }
}
