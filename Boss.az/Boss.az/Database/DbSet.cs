//using Boss.az.ExceptionNS;
//using System;
//using System.Collections;
//using System.Collections.Generic;

//namespace Boss.az.DatabaseNS
//{
//    class DbSet<T> : IEnumerable<T>
//    {
//        List<T> Items = new List<T>();
//        public void Add(T item)
//        {
//            if (item == null)
//            {
//                throw new DbSetException($"{typeof(T).Name} is null!");
//            }
//            Items.Add(item);
//        }
//        public void Delete(T item)
//        {
//            if (item == null)
//                throw new DbSetException($"{typeof(T).Name} is null!");
//            else if (Items.Exists(data => data.Equals(item)))
//                Items.Remove(item);
//            else
//                Console.WriteLine($"Not found {typeof(T).Name.ToLower()}!");
//        }
//        #region Enumerable implement
//        private IEnumerable<T> GetValues()
//        {
//            foreach (var item in Items)
//            {
//                yield return item;
//            }
//        }
//        public IEnumerator<T> GetEnumerator()
//        {
//            return GetValues().GetEnumerator();
//        }
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }

//        #endregion
//    }
//}
