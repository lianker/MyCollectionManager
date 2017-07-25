using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainedLists
{
    public class ChainedListManager
    {
        private List<Node> _orderedList { get; set; }
        private List<Node> _list { get; set; }
        private int _level { get; set; } = 1;

        public ChainedListManager()
        {
            _orderedList = new List<Node>();
            _list = new List<Node>();
        }

        private void GetChildrenRecursive(List<Node> list, List<Node> parents)
        {
            foreach (var node in parents)
            {
                node.Level = _level;
                _orderedList.Add(node);
                list.Remove(node);

                var childs = GetChildren(node, list);

                if (childs != null && childs.Any())
                {
                    _level++;
                    GetChildrenRecursive(list, childs);
                }
            }
        }

        private List<Node> GetChildren(Node node, List<Node> listBase)
        {
            var childs = listBase.Where(n => n.ParentId == node.Id)
                                 .OrderBy(x => x.Position)
                                 .ToList();

            return childs;
        }

        private List<Node> GetParentsOf(List<Node> list)
        {
            var parents = list.Where(x => !x.ParentId.HasValue)
                            .OrderBy(x => x.Position)
                            .ToList();

            if (!parents.Any())
            {
                throw new InvalidOperationException("Parents not found");
            }

            return parents;
        }

        public List<Node> GetOrderedList(List<Node> list)
        {
            _list = list.ToList();
            var parents = GetParentsOf(_list);
            
            GetChildrenRecursive(_list, parents);
            
            return _orderedList;
        }
    }
}
