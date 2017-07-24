using NUnit.Framework;
using ChainedLists;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainedListsTest
{

    public class ChainedListManagerTest
    {
        List<Node> familly;
        ChainedListManager manager;

        [SetUp]
        public void Init()
        {
            familly = new List<Node>
            {
                new Node{ Id = 1, Description="GrandFather", Position = 1, ParentId = null },
                new Node{ Id = 2, Description="Father", Position = 1, ParentId = 1 },
                new Node{ Id = 5, Description="GrandSon", Position = 1, ParentId = 3 },
                new Node{ Id = 3, Description="Son", Position = 1, ParentId = 2 },
                new Node{ Id = 4, Description="Daughter", Position = 2, ParentId = 2 }
            };

            manager = new ChainedListManager();
        }

        [Test, Category("GetOrderedList")]
        public void should_throw_exception_when_parents_not_found()
        {
            familly.RemoveAll(x => !x.ParentId.HasValue);
            Assert.Throws<InvalidOperationException>(() => manager.GetOrderedList(familly));
        }      

        [Test, Category("GetOrderedList")]
        public void should_add_levels_when_exists_childs()
        {
            var nodes = manager.GetOrderedList(familly);
            var maxLevel = nodes.Max(x => x.Level);
            Assert.AreEqual(5, nodes.Count);
        }

        [Test, Category("GetOrderedList")]
        public void not_should_change_original_list()
        {
            var before = familly.Count;
            var nodes = manager.GetOrderedList(familly);
            Assert.AreEqual(familly.Count, before);
        }
    }
}
