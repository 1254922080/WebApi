using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class BaseRepositories<T> : IBaseRepositories<T> where T : class
    {
        private readonly TestDbContext db;
        public BaseRepositories(TestDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>int</returns>

        public int Count(Expression<Func<T, bool>> expression)
        {
            return db.Set<T>().Where(expression).Count();
        }

        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>Task<int></returns>
        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await db.Set<T>().Where(expression).CountAsync();
        }

        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        public T GetNoTracking(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().AsNoTracking().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 通过Lamda表达式获取实体(去除追踪)
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        public async Task<T> GetNoTrackingAsync(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 获取list集合
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>List<T></returns>
        public List<T> GetList(Expression<Func<T, bool>> expression)
        {
            return db.Set<T>().ToList();
        }

        /// <summary>
        /// 获取list集合
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>List<T></returns>
        public async Task<List<T>> GeListAsync(Expression<Func<T, bool>> expression)
        {
            return await db.Set<T>().ToListAsync();
        }

        /// <summary>
        /// 获取list集合(去除追踪)
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>List<T></returns>
        public List<T> GeListAsnotracking(Expression<Func<T, bool>> expression)
        {
            return db.Set<T>().AsNoTracking().Where(expression).ToList();
        }

        /// <summary>
        /// 获取list集合(去除追踪)
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>List<T></returns>
        public async Task<List<T>> GeListAsnotrackingAsync(Expression<Func<T, bool>> expression)
        {
            return await db.Set<T>().AsNoTracking().Where(expression).ToListAsync();
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public bool Save(T entity)
        {
            db.Set<T>().Add(entity);
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public async Task<bool> SaveAsync(T entity)
        {
            db.Set<T>().Add(entity);
            return await db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        public bool SaveList(List<T> list)
        {
            bool result = false;
            if (list == null || list.Count == 0)
            {
                return result;
            }

            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    list.ForEach(item =>
                    {
                        db.Set<T>().Add(item);
                    });
                    db.SaveChanges();
                    tran.Commit();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                    tran.Rollback();
                }
            }
            return result;
        }

        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        public async Task<bool> SaveListAsync(List<T> list)
        {
            bool result = false;
            if (list == null || list.Count == 0)
            {
                return result;
            }

            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    list.ForEach(item =>
                    {
                        db.Set<T>().Add(item);
                    });
                    await db.SaveChangesAsync();
                    tran.Commit();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                    tran.Rollback();
                }
            }
            return result;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public bool Update(T entity)
        {
            try
            {
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                return await db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public bool UpdateList(List<T> list)
        {
            bool result = false;
            if (list == null || list.Count == 0)
            {
                return result;
            }

            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    list.ForEach(item =>
                    {
                        db.Set<T>().Update(item);
                    });
                    result = db.SaveChanges() > 0 ? true : false;
                    tran.Commit();
                }
                catch (Exception)
                {
                    result = false;
                    tran.Rollback();
                }
            }
            return result;
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateListAsync(List<T> list)
        {
            bool result = false;
            if (list == null || list.Count == 0)
            {
                return result;
            }

            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    list.ForEach(item =>
                    {
                        db.Set<T>().Update(item);
                    });
                    result = await db.SaveChangesAsync() > 0 ? true : false;
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    result = false;
                }
            }
            return result;
        }
    }

}
