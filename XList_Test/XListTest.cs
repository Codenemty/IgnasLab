using IgnasLab;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XList_Test
{
    [TestClass]
    public class XListTest
    {
        [TestMethod]
        public void XList_Add_HoldsGivenObjectsInside_True()
        {
            XList<Player> list = new XList<Player>();
            Player a = new Player() { Name = "Karen" };
            Assert.IsFalse(list.Contains(a));   //Case empty lsit
            list.Add(a);
            Assert.IsTrue(list.Contains(a));    //Should contain added object
        }

        [TestMethod]
        public void XList_Count_ReturnsObjCount()
        {
            XList<Player> list = new XList<Player>();
            Assert.AreEqual(list.Count(), 0);
            Player pHandle = new Player() { Name = "D" };
            list.Add(new Player() { Name = "A" });
            Assert.AreEqual(list.Count(), 1);
            list.Add(new Player() { Name = "B" });
            Assert.AreEqual(list.Count(), 2);
            list.Add(new Player() { Name = "C" });
            Assert.AreEqual(list.Count(), 3);
            list.Add(pHandle);
            Assert.AreEqual(list.Count(), 4);

            list.Remove(pHandle);
            Assert.AreEqual(list.Count(), 3);

            list.Dispose();
            Assert.AreEqual(list.Count(), 0);
        }


        [TestMethod]
        public void XList_Contains_ReturnsIfGivenObjectIsInXList()
        {
            XList<Player> list = new XList<Player>();
            Player a = new Player() { Name = "Karen" };
            Player b = new Player() { Name = "Bob" };

            Assert.IsFalse(list.Contains(b));   //Case empty list
            list.Add(a);
            Assert.IsFalse(list.Contains(b));   //Case wrong obj inside
            list.Add(b);
            Assert.IsTrue(list.Contains(b));   //Case right obj inside
        }

        [TestMethod]
        public void XList_Dispose_RemovesAllReferencesFromXList()
        {
            XList<Player> list = new XList<Player>();

            list.Add(new Player() { Name = "A" });
            list.Add(new Player() { Name = "B" });
            list.Add(new Player() { Name = "C" });
            list.Add(new Player() { Name = "D" });
            list.Add(new Player() { Name = "E" });

            Assert.AreNotEqual(list.Count(), 0);        //Object count in list is above 0 (5)
            list.Dispose();                             //Removes all references from list
            Assert.AreEqual(list.Count(), 0);           //Object count should be 0
        }

        [TestMethod]
        public void XList_IndexOf_ReturnsIndexOfGivenObj()
        {
            XList<Player> list = new XList<Player>();

            Player unused = new Player() { Name = "ZZZZZZ" };
            Player a = new Player() { Name = "A" };
            Player b = new Player() { Name = "B" };
            Player c = new Player() { Name = "C" };

            Assert.AreEqual(list.IndexOf(unused), -1);
            Assert.AreEqual(list.IndexOf(a), -1);

            list.Add(a);
            list.Add(b);
            list.Add(c);

            Assert.AreEqual(list.IndexOf(a), 0);
            Assert.AreEqual(list.IndexOf(b), 1);
            Assert.AreEqual(list.IndexOf(c), 2);

        }


        [TestMethod]
        public void XList_Remove_RemovesGivenObjFromXList()
        {
            XList<Player> list = new XList<Player>();

            Player a = new Player() { Name = "A" };
            Player b = new Player() { Name = "B" };
            Player c = new Player() { Name = "C" };

            list.Add(a);
            list.Add(b);
            list.Add(c);

            Assert.IsTrue(list.Contains(a));
            Assert.IsTrue(list.Contains(b));
            Assert.IsTrue(list.Contains(c));
            list.Remove(b);
            Assert.IsTrue(list.Contains(a));
            Assert.IsFalse(list.Contains(b));
            Assert.IsTrue(list.Contains(c));
            list.Remove(a);
            Assert.IsFalse(list.Contains(a));
            Assert.IsFalse(list.Contains(b));
            Assert.IsTrue(list.Contains(c));
            list.Remove(c);
            Assert.IsFalse(list.Contains(a));
            Assert.IsFalse(list.Contains(b));
            Assert.IsFalse(list.Contains(c));

        }

        [TestMethod]
        public void XList_Sort_SortsObjects()
        {
            XList<Player> list = new XList<Player>();

            // Sorting by goalCount, order should be   (c b a d)
            Player a = new Player() { GoalCount = 5 };
            Player b = new Player() { GoalCount = 7 };
            Player c = new Player() { GoalCount = 8 };
            Player d = new Player() { GoalCount = 4 };

            list.Add(a);
            list.Add(b);
            list.Add(c);
            list.Add(d);

            //"Is it sorted?" Block
            int priorGoalCount = 0;
            bool sorted = true;
            foreach(Player p in list)
            {
                if(p.GoalCount > priorGoalCount) //b > a so sorted will be false
                {
                    sorted = false;
                    break;
                }
                priorGoalCount = p.GoalCount;
            }
            Assert.IsFalse(sorted);


            list.Sort();  // Should sort

            priorGoalCount = int.MaxValue;
            sorted = true;
            foreach (Player p in list)       
            {
                if (p.GoalCount > priorGoalCount)   //Should be sorted so sorted will never become false
                {
                    sorted = false;
                    break;
                }
                priorGoalCount = p.GoalCount;
            }

            Assert.IsTrue(sorted);
        }

    }
}
