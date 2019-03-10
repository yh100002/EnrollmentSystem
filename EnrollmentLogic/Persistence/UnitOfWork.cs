﻿using System.Data;
using System.Linq;
using EnrollmentApi.Logic.Interfaces;
using NHibernate;

namespace EnrollmentApi.Logic.Persistence
{
    public sealed class UnitOfWork
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;
        private bool _isAlive = true;

        public UnitOfWork(IDBSessionFactory sessionFactory)
        {
            _session = sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public UnitOfWork(IDBQuerySessionFactory sessionFactory)
        {
            _session = sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Commit()
        {
            if (!_isAlive)
                return;

            try
            {
                _transaction.Commit();
            }
            finally
            {
                _isAlive = false;
                _transaction.Dispose();
                _session.Dispose();
            }
        }

        internal T Get<T>(long id)
            where T : class
        {
            return _session.Get<T>(id);
        }

        internal void SaveOrUpdate<T>(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        internal void Delete<T>(T entity)
        {
            _session.Delete(entity);
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }
    }
}
