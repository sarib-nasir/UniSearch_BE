﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UniSearch.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UniSearch.Services.Base
{
    public abstract class Service<T> : IDisposable, IService<T> where T : class
    {
        protected DbSet<T> Table;

        protected readonly ApplicationDbContext Db;

        bool _disposed = false;

        public ApplicationDbContext Context => Db;

        public T Find(long? id) => Table.Find(id);

        public T GetFirst(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;

            IQueryable<T> dbQuery = Table;

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause

            return item;
        }

        public T GetLast(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;

            IQueryable<T> dbQuery = Table;

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause

            return item;
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;

            IQueryable<T> dbQuery = Table;

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .ToList<T>();

            return list;
        }

        public IEnumerable<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;

            IQueryable<T> dbQuery = Table;

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .Where(where)
                .ToList<T>();

            return list;
        }

        public T GetFirst(Func<T, bool> where)
        {
            T item = null;

            item = Table
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause

            return item;
        }


        public T GetLast(Func<T, bool> where)
        {
            T item = null;

            item = Table
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause

            return item;
        }

        public bool GetAny(Func<T, bool> where)
        {
            List<T> list;

            IQueryable<T> dbQuery = Table;

            list = dbQuery
                .AsNoTracking()
                .Where(where)
                .ToList<T>();
            if (list.Count > 0)
            {
                return true;
            }

            return false;
        }
        public IEnumerable<T> GetList(Func<T, bool> where)
        {
            List<T> list;

            IQueryable<T> dbQuery = Table;

            list = dbQuery
                .AsNoTracking()
                .Where(where)
                .ToList<T>();

            return list;
        }

        public T GetFirst() => Table.FirstOrDefault();


        public T GetLast() => Table.LastOrDefault();

        public virtual IEnumerable<T> GetAll() => Table;

        internal IEnumerable<T> GetRange(IQueryable<T> query, int skip, int take) => query.Skip(skip).Take(take);

        public virtual IEnumerable<T> GetRange(int skip, int take) => GetRange(Table, skip, take);

        public bool HasChanges => Db.ChangeTracker.HasChanges();

        public int Count => Table.Count();

        protected Service()
        {
            Db = new ApplicationDbContext();
            Table = Db.Set<T>();
        }

        protected Service(DbContextOptions<ApplicationDbContext> options)
        {
            Db = new ApplicationDbContext(options);
            Table = Db.Set<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                // Free any other managed objects here.
                //
            }
            Db.Dispose();
            _disposed = true;
        }

        public int SaveChanges()
        {
            try
            {
                return Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //A concurrency error occurred
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                //DbResiliency retry limit exceeded
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
            }
        }

        public virtual int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public virtual int Update(T entity, bool persist = true)
        {
            Table.Update(entity);

            return persist ? SaveChanges() : 0;
        }

        public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public virtual int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }
    }
}
