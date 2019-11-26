using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public partial interface IBaseRepositories<T>
         where T : class
    {
        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        T Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 通过Lamda表达式获取实体（异步方式）
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        T GetNoTracking(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 通过Lamda表达式获取实体(去除追踪)
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns>T</returns>
        Task<T> GetNoTrackingAsync(Expression<Func<T, bool>> predicate);


        /// <summary>
        /// 获取list集合
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>List<T></returns>
        List<T> GetList(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 获取list集合
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>List<T></returns>
        Task<List<T>> GeListAsync(Expression<Func<T, bool>> expression);


        /// <summary>
        /// 获取list集合
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>List<T></returns>
        List<T> GeListAsnotracking(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 获取list集合
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>List<T></returns>
        Task<List<T>> GeListAsnotrackingAsync(Expression<Func<T, bool>> expression);


        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        bool Save(T entity);

        /// <summary>
        /// 增加一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        Task<bool> SaveAsync(T entity);

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        bool Update(T entity);

        /// <summary>
        /// 更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        bool SaveList(List<T> list);

        /// <summary>
        /// 增加多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        Task<bool> SaveListAsync(List<T> list);

        /// <summary>
        /// 更新多条记录，同一模型
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        bool UpdateList(List<T> list);

        /// <summary>
        /// 更新多条记录，同一模型
        /// </summary>
        /// <param name="list">实体模型集合</param>
        /// <returns>bool</returns>
        Task<bool> UpdateListAsync(List<T> list);

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>int</returns>
        int Count(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>int</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
    }
}
