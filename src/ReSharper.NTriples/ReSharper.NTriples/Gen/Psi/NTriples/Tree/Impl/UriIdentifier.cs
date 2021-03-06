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
  internal partial class UriIdentifier : NTriplesCompositeElement, ReSharper.NTriples.Tree.IUriIdentifier {
    public const short PREFIX= ChildRole.LAST + 1;
    public const short LOCALNAME= ChildRole.LAST + 2;
    public const short URISTRING= ChildRole.LAST + 3;
    internal UriIdentifier() : base() {
    }
    public override JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeType NodeType {
      get { return ReSharper.NTriples.Impl.Tree.ElementType.URI_IDENTIFIER; }
    }
    public override void Accept(ReSharper.NTriples.Tree.TreeNodeVisitor visitor) {
      visitor.VisitUriIdentifier(this);
    }
    public override void Accept<TContext>(ReSharper.NTriples.Tree.TreeNodeVisitor<TContext> visitor, TContext context) {
      visitor.VisitUriIdentifier(this, context);
    }
    public override TReturn Accept<TContext, TReturn>(ReSharper.NTriples.Tree.TreeNodeVisitor<TContext, TReturn> visitor, TContext context) {
      return visitor.VisitUriIdentifier(this, context);
    }
    private static readonly JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeTypeDictionary<short> CHILD_ROLES = new JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeTypeDictionary<short>(
      new System.Collections.Generic.KeyValuePair<JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeType, short>[]
      {
        new System.Collections.Generic.KeyValuePair<JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeType, short>(ReSharper.NTriples.Impl.Tree.ElementType.PREFIX, PREFIX),
        new System.Collections.Generic.KeyValuePair<JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeType, short>(ReSharper.NTriples.Impl.Tree.ElementType.LOCAL_NAME, LOCALNAME),
        new System.Collections.Generic.KeyValuePair<JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeType, short>(ReSharper.NTriples.Impl.Tree.ElementType.URI_STRING, URISTRING),
      }
    );
    public override short GetChildRole(JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.TreeElement child) {
      return CHILD_ROLES[child.NodeType];
    }
    public virtual ReSharper.NTriples.Tree.ILocalName LocalName {
      get { return (ReSharper.NTriples.Tree.ILocalName) FindChildByRole(LOCALNAME); }
    }
    public virtual ReSharper.NTriples.Tree.IPrefix Prefix {
      get { return (ReSharper.NTriples.Tree.IPrefix) FindChildByRole(PREFIX); }
    }
    public virtual ReSharper.NTriples.Tree.IUriString UriStringElement {
      get { return (ReSharper.NTriples.Tree.IUriString) FindChildByRole(URISTRING); }
    }
    public virtual  JetBrains.ReSharper.Psi.Tree.ITokenNode UriString {
      get
      {
        CompositeElement current = this;  
    
        JetBrains.ReSharper.Psi.Tree.ITokenNode result = null;
        CompositeElement current0 = (CompositeElement)current.FindChildByRole (ReSharper.NTriples.Impl.Tree.UriIdentifier.URISTRING);
        if (current0 != null) {
          TreeElement current1 = current0.FindChildByRole (ReSharper.NTriples.Impl.Tree.UriString.URISTRING);
          if (current1 != null) {
            result = (JetBrains.ReSharper.Psi.Tree.ITokenNode) current1;
          }
        }
        return result;
      }
    }
    public virtual ReSharper.NTriples.Tree.ILocalName SetLocalName (ReSharper.NTriples.Tree.ILocalName param)
    {
      using (JetBrains.Application.WriteLockCookie.Create(this.IsPhysical()))
      {
        TreeElement current = null, next = GetNextFilteredChild(current), result = null;
        next = GetNextFilteredChild(current);
        if (next.NodeType == ReSharper.NTriples.Impl.Tree.ElementType.PREFIX) {
          next = GetNextFilteredChild (current);
          if (next == null) {
            return (ReSharper.NTriples.Tree.ILocalName)result;
          } else {
            if (next.NodeType == ReSharper.NTriples.Impl.Tree.ElementType.PREFIX) {
              current = next;
            } else {
              return (ReSharper.NTriples.Tree.ILocalName) result;
            }
          }
          next = GetNextFilteredChild (current);
          if (next == null) {
            if (param == null) return null;
            result = current = (TreeElement) JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.AddChildAfter(this, current, (JetBrains.ReSharper.Psi.Tree.ITreeNode) param);
          } else {
            if (next.NodeType == ReSharper.NTriples.Impl.Tree.ElementType.LOCAL_NAME) {
              if (param != null) {
                result = current = (TreeElement) JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.ReplaceChild(next, (JetBrains.ReSharper.Psi.Tree.ITreeNode) param);
              } else {
                current = GetNextFilteredChild(next);
                JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.DeleteChild(next);
              }
            } else {
              if (param == null) return null;
              result = (TreeElement) JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.AddChildBefore(next, (JetBrains.ReSharper.Psi.Tree.ITreeNode) param);
              current = next;
            }
          }
        }
        else if (next.NodeType == ReSharper.NTriples.Impl.Tree.TokenType.URI_BEGIN) {
        }
        else return null;
        return (ReSharper.NTriples.Tree.ILocalName)result;
      }
    }
    public virtual ReSharper.NTriples.Tree.IPrefix SetPrefix (ReSharper.NTriples.Tree.IPrefix param)
    {
      using (JetBrains.Application.WriteLockCookie.Create(this.IsPhysical()))
      {
        TreeElement current = null, next = GetNextFilteredChild(current), result = null;
        next = GetNextFilteredChild(current);
        if (next.NodeType == ReSharper.NTriples.Impl.Tree.ElementType.PREFIX) {
          next = GetNextFilteredChild (current);
          if (next == null) {
            if (param == null) return null;
            result = current = (TreeElement) JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.AddChildAfter(this, current, (JetBrains.ReSharper.Psi.Tree.ITreeNode) param);
          } else {
            if (next.NodeType == ReSharper.NTriples.Impl.Tree.ElementType.PREFIX) {
              if (param != null) {
                result = current = (TreeElement) JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.ReplaceChild(next, (JetBrains.ReSharper.Psi.Tree.ITreeNode) param);
              } else {
                current = GetNextFilteredChild(next);
                JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.DeleteChild(next);
              }
            } else {
              if (param == null) return null;
              result = (TreeElement) JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.AddChildBefore(next, (JetBrains.ReSharper.Psi.Tree.ITreeNode) param);
              current = next;
            }
          }
        }
        else if (next.NodeType == ReSharper.NTriples.Impl.Tree.TokenType.URI_BEGIN) {
        }
        else return null;
        return (ReSharper.NTriples.Tree.IPrefix)result;
      }
    }
    public virtual ReSharper.NTriples.Tree.IUriString SetUriStringElement (ReSharper.NTriples.Tree.IUriString param)
    {
      using (JetBrains.Application.WriteLockCookie.Create(this.IsPhysical()))
      {
        TreeElement current = null, next = GetNextFilteredChild(current), result = null;
        next = GetNextFilteredChild(current);
        if (next.NodeType == ReSharper.NTriples.Impl.Tree.ElementType.PREFIX) {
          next = GetNextFilteredChild (current);
          if (next == null) {
            return (ReSharper.NTriples.Tree.IUriString)result;
          } else {
            if (next.NodeType == ReSharper.NTriples.Impl.Tree.ElementType.PREFIX) {
              current = next;
            } else {
              return (ReSharper.NTriples.Tree.IUriString) result;
            }
          }
          next = GetNextFilteredChild (current);
          if (next == null) {
            return (ReSharper.NTriples.Tree.IUriString)result;
          } else {
            if (next.NodeType == ReSharper.NTriples.Impl.Tree.ElementType.LOCAL_NAME) {
              current = next;
            } else {
              return (ReSharper.NTriples.Tree.IUriString) result;
            }
          }
        }
        else if (next.NodeType == ReSharper.NTriples.Impl.Tree.TokenType.URI_BEGIN) {
          next = GetNextFilteredChild (current);
          if (next == null) {
            return (ReSharper.NTriples.Tree.IUriString)result;
          } else {
            if (next.NodeType == ReSharper.NTriples.Impl.Tree.TokenType.URI_BEGIN) {
              current = next;
            } else {
              return (ReSharper.NTriples.Tree.IUriString) result;
            }
          }
          next = GetNextFilteredChild (current);
          if (next == null) {
            if (param == null) return null;
            result = current = (TreeElement) JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.AddChildAfter(this, current, (JetBrains.ReSharper.Psi.Tree.ITreeNode) param);
          } else {
            if (next.NodeType == ReSharper.NTriples.Impl.Tree.ElementType.URI_STRING) {
              if (param != null) {
                result = current = (TreeElement) JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.ReplaceChild(next, (JetBrains.ReSharper.Psi.Tree.ITreeNode) param);
              } else {
                current = GetNextFilteredChild(next);
                JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.DeleteChild(next);
              }
            } else {
              if (param == null) return null;
              result = (TreeElement) JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.ModificationUtil.AddChildBefore(next, (JetBrains.ReSharper.Psi.Tree.ITreeNode) param);
              current = next;
            }
          }
        }
        else return null;
        return (ReSharper.NTriples.Tree.IUriString)result;
      }
    }
    public override string ToString() {
      return "IUriIdentifier";
    }
  }
}
