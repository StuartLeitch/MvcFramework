using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EntityExtensions
{
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        private readonly List<T> _list = new List<T>();

        public FakeDbSet()
        {
            this._list = new List<T>();
        }

        public FakeDbSet(IEnumerable<T> contents)
        {
            this._list = contents.ToList();
        }

        public FakeDbSet(T singleItem)
        {
            this._list.Add(singleItem);
        }

        public Type ElementType
        {
            get
            {
                return this._list.AsQueryable().ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return this._list.AsQueryable().Expression;
            }
        }

        public ObservableCollection<T> Local
        {
            get
            {
                return new ObservableCollection<T>(this._list);
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return this._list.AsQueryable().Provider;
            }
        }

        public T Add(T entity)
        {
            this._list.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            this._list.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Remove(T entity)
        {
            this._list.Remove(entity);
            return entity;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }
    }
}