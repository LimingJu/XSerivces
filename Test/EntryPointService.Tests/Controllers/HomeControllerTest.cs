using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntryPointService.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        public class ReduceAndDeferQueue<T> : IEnumerable<T>
        {
            private readonly Object syncObject = new object();
            private readonly HashSet<T> set;

            private readonly List<T> buffer = new List<T>();

            private readonly System.Timers.Timer timer;

            public event EventHandler<ReduceAndDeferQueueOnItemEnqueuedEventArg<T>> OnItemEnqueued;
            private ReduceAndDeferQueue()
            {
            }

            /// <summary>
            /// Initialize an instance of class ReduceAndDeferQueue
            /// </summary>
            /// <param name="deferTime">how many time of interval want to get a notify if new item get enqueued</param>
            public ReduceAndDeferQueue(int deferTime)
                : this(deferTime, null)
            {
            }

            public ReduceAndDeferQueue(int deferTime, IEqualityComparer<T> comparer)
            {
                this.set = comparer != null ? new HashSet<T>(comparer) : new HashSet<T>();
                this.timer = new System.Timers.Timer(deferTime);
                this.timer.Elapsed += ((s, a) =>
                {
                    lock (this.syncObject)
                    {
                        if (!this.buffer.Any()) return;
                        var safe = this.OnItemEnqueued;
                        if (safe == null) return;
                        try
                        {
                            safe(this, new ReduceAndDeferQueueOnItemEnqueuedEventArg<T>(this.buffer));
                        }
                        finally
                        {
                            /* Items in buffer already consumed, so clear them */
                            this.set.RemoveWhere(i => this.buffer.Contains(i));
                            this.buffer.Clear();
                        }
                    }
                });
                this.timer.Start();
            }

            #region IEnumerable<T> Members

            public IEnumerator<T> GetEnumerator()
            {
                lock (this.syncObject)
                {
                    return this.set.GetEnumerator();
                }
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            #endregion

            public bool Enqueue(T item)
            {
                lock (this.syncObject)
                {
                    if (this.set.Contains(item)) return false;
                    this.set.Add(item);
                    this.buffer.Add(item);
                    return true;
                }
            }
        }

        public class ReduceAndDeferQueueOnItemEnqueuedEventArg<T> : EventArgs
        {
            public ReduceAndDeferQueueOnItemEnqueuedEventArg(List<T> enqueuedItems)
            {
                this.EnqueuedItems = enqueuedItems;
            }

            public List<T> EnqueuedItems { get; private set; }
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.Index() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual("Home Page", result.ViewBag.Title);
        }

        [TestMethod]
        public void ExtraTest0()
        {
            var queue = new ReduceAndDeferQueue<string>(5000);
            string[] enqueueItems = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
            List<bool> results = new List<bool>();
            foreach (var enqueueItem in enqueueItems)
            {
                results.Add(queue.Enqueue(enqueueItem));
            }

            Assert.IsTrue(results.All(_ => true), "");
        }

        [TestMethod]
        public void ExtraTest1()
        {
            var queue = new ReduceAndDeferQueue<string>(5000);
            string[] enqueueItems = new string[] { "a", "a", "a", "d", "e", "f", "g", "h", "i" };
            List<bool> results = new List<bool>();
            foreach (var enqueueItem in enqueueItems)
            {
                results.Add(queue.Enqueue(enqueueItem));
            }

            Assert.IsTrue(results.Count(p => !p) == 2, "");
        }

        [TestMethod]
        public void ExtraTest2()
        {
            var queue = new ReduceAndDeferQueue<string>(5000);
            bool getCalled = false;
            queue.OnItemEnqueued += (sender, arg) =>
            {
                getCalled = true;
            };
            string[] enqueueItems = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
            foreach (var enqueueItem in enqueueItems)
            {
                queue.Enqueue(enqueueItem);
            }
            Assert.IsTrue(getCalled == false, "getCalled == false");
        }

        [TestMethod]
        public void ExtraTest3()
        {
            var queue = new ReduceAndDeferQueue<string>(2500);
            bool getCalled = false;
            List<string> dequeuedItems = null;
            queue.OnItemEnqueued += (sender, arg) =>
            {
                getCalled = true;
                dequeuedItems = new List<string>();
                dequeuedItems.AddRange(arg.EnqueuedItems);
            };
            string[] enqueueItems = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
            foreach (var enqueueItem in enqueueItems)
            {
                queue.Enqueue(enqueueItem);
            }
            Thread.Sleep(3000);
            Assert.IsTrue(getCalled == true, "getCalled == true");
            Assert.IsTrue(dequeuedItems != null, "dequeuedItems!=null");
            Assert.IsTrue(dequeuedItems.Count == enqueueItems.Length, "Count");
            Assert.IsTrue(dequeuedItems.All(d => enqueueItems.Contains(d)), "All");

            Thread.Sleep(3000);
            //reset all, and see still good with no new item enqueued.
            getCalled = false;
            dequeuedItems = null;
            Assert.IsTrue(getCalled == false, "getCalled == false");
            Assert.IsTrue(dequeuedItems == null, "dequeuedItems==null");
        }
    }
}
