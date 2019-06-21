namespace Tests
{
    using MSUnitTest = Microsoft.VisualStudio.TestTools.UnitTesting;
    using NUnit.Framework;
    using TasksLibrary;

    [MSUnitTest.TestClass]
    [TestFixture]
    class BubbleSortTests
    {
        TaskWorker tWorker = new TaskWorker();

        [Test]
        public void CheckBubbleSortBySum()
        {
            var testData = new BubbleSortTestData[]
            {
                new BubbleSortTestData() // Descending test
                {
                    ActualArray = new int[,]
                    {
                        {1,2 },
                        {3,4 },
                        {0,5 }
                    },
                    SortMethod = new SortByRowSum(),
                    Direction = Direction.Descending,
                    ExpectedArray = new int[,]
                    {
                        {3,4 },
                        {0,5 },
                        {1,2 }
                    }
                },
                new BubbleSortTestData() // Ascending test
                {
                    ActualArray = new int[,]
                    {
                        {1,2 },
                        {3,4 },
                        {0,5 }
                    },
                    SortMethod = new SortByRowSum(),
                    Direction = Direction.Ascending,
                    ExpectedArray = new int[,]
                    {
                        {1,2 },
                        {0,5 },
                        {3,4 }
                    }
                },
                new BubbleSortTestData()  // Boundary values
                {
                    ActualArray = new int[,]
                    {
                        {int.MaxValue,int.MinValue },
                        {int.MinValue,int.MaxValue }
                    },
                    SortMethod = new SortByRowSum(),
                    Direction = Direction.Descending,
                    ExpectedArray = new int[,]
                    {
                        {int.MaxValue,int.MinValue },
                        {int.MinValue,int.MaxValue }
                    }
                }
            };

            MakeTests(testData);
        }

        [Test]
        public void CheckBubbleSortByMaxElement()
        {
            var testData = new BubbleSortTestData[]
            {
                new BubbleSortTestData() // Common test
                {
                    ActualArray = new int[,]
                    {
                        {1,2,30 },
                        {3,4,50 },
                        {0,5,49 }
                    },
                    SortMethod = new SortByRowMaxElement(),
                    Direction = Direction.Descending,
                    ExpectedArray = new int[,]
                    {
                        {3,4,50 },
                        {0,5,49 },
                        {1,2,30 }
                    }
                },
                new BubbleSortTestData() // First order maximums are equal
                {
                    ActualArray = new int[,]
                    {
                        {-1,2,1,4 },
                        {-3,4,3,4 },
                        {0,2,-1,4 }
                    },
                    SortMethod = new SortByRowMaxElement(),
                    Direction = Direction.Descending,
                    ExpectedArray = new int[,]
                    {
                        {-3,4,3,4 },
                        {-1,2,1,4 },
                        {0,2,-1,4 }
                    }
                },
                new BubbleSortTestData() // Only the maximums of the last order are not equal
                {
                    ActualArray = new int[,]
                    {
                        {4,3,2,1,0 },
                        {4,3,2,1,-1 },
                        {4,3,2,1,-2 }
                    },
                    SortMethod = new SortByRowMaxElement(),
                    Direction = Direction.Ascending,
                    ExpectedArray = new int[,]
                    {
                        {4,3,2,1,-2 },
                        {4,3,2,1,-1 },
                        {4,3,2,1,0 }
                    }
                }
            };

            MakeTests(testData);
        }

        [Test]
        public void CheckBubbleSortByMinElement()
        {
            var testData = new BubbleSortTestData[]
            {
                new BubbleSortTestData() // Negative number
                {
                    ActualArray = new int[,]
                    {
                        {1,2 },
                        {10,20 },
                        {-50,1 },
                        {32,32 },
                        {0,1 },
                        {1,3 }
                    },
                    SortMethod = new SortByRowMinElement(),
                    Direction = Direction.Descending,
                    ExpectedArray = new int[,]
                    {
                        {32,32 },
                        {10,20 },
                        {1,3 },
                        {1,2 },
                        {0,1 },
                        {-50,1 }
                    }
                },
                new BubbleSortTestData() // 1 element in row
                {
                    ActualArray = new int[,]
                    {
                        {1},
                        {3},
                        {0}
                    },
                    SortMethod = new SortByRowMinElement(),
                    Direction = Direction.Ascending,
                    ExpectedArray = new int[,]
                    {
                        {0},
                        {1},
                        {3}
                    }
                },
                new BubbleSortTestData() // 1 element matrix
                {
                    ActualArray = new int[,]
                    {
                        {1}
                    },
                    SortMethod = new SortByRowMinElement(),
                    Direction = Direction.Ascending,
                    ExpectedArray = new int[,]
                    {
                        {1}
                    }
                }
            };

            MakeTests(testData);
        }

        private void MakeTests(BubbleSortTestData[] testData)
        {
            foreach (var t in testData)
            {
                var array = t.ActualArray;
                tWorker.BubbleSort(array, t.SortMethod, t.Direction);
                MSUnitTest.CollectionAssert.AreEqual(t.ExpectedArray, array);
            }
        }
    }

    class BubbleSortTestData
    {
        public int[,] ActualArray { get; set; }
        public int[,] ExpectedArray { get; set; }
        public ISortable SortMethod { get; set; }
        public Direction Direction { get; set; }
    }
}
