using MesDatas.Utility.SugarDB;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace  MesDatas.Utility.SugarDB
{
    /// <summary>
    /// 创建仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : SimpleClient<T> where T : class, new()
    {

        public Repository()
        {
            //固定数据库用法
            // base.Context = SqlSugarHelper.Db.GetConnectionScopeWithAttr<T>();

            // var configId = typeof(T).GetCustomAttribute<TenantAttribute>()?.configId;
            base.Context = DBConnSugClie.GetDBConnection();
            //建库：如果不存在创建数据库存在不会重复创建 createdb
            //Context.DbMaintenance.CreateDatabase();// 注意 ：Oracle和个别国产库需不支持该方法，需要手动建库 

            base.Context.CodeFirst.InitTables<T>();

        }

        #region 分表操作

        public async Task<bool> SplitTableInsertAsync(T input)
        {
            return await base.AsInsertable(input).SplitTable().ExecuteCommandAsync() > 0;
        }

        public async Task<bool> SplitTableInsertAsync(List<T> input)
        {
            return await base.AsInsertable(input).SplitTable().ExecuteCommandAsync() > 0;
        }

        public async Task<bool> SplitTableUpdateAsync(T input)
        {
            return await base.AsUpdateable(input).SplitTable().ExecuteCommandAsync() > 0;
        }

        public async Task<bool> SplitTableUpdateAsync(List<T> input)
        {
            return await base.AsUpdateable(input).SplitTable().ExecuteCommandAsync() > 0;
        }

        public async Task<bool> SplitTableDeleteableAsync(T input)
        {
            return await base.Context.Deleteable(input).SplitTable().ExecuteCommandAsync() > 0;
        }

        public async Task<bool> SplitTableDeleteableAsync(List<T> input)
        {
            return await base.Context.Deleteable(input).SplitTable().ExecuteCommandAsync() > 0;
        }

        public Task<T> SplitTableGetFirstAsync(Expression<Func<T, bool>> whereExpression)
        {
            return base.AsQueryable().SplitTable().FirstAsync(whereExpression);
        }

        public Task<bool> SplitTableIsAnyAsync(Expression<Func<T, bool>> whereExpression)
        {
            return base.Context.Queryable<T>().Where(whereExpression).SplitTable().AnyAsync();
        }

        public Task<List<T>> SplitTableGetListAsync()
        {
            return Context.Queryable<T>().SplitTable().ToListAsync();
        }

        public Task<List<T>> SplitTableGetListAsync(Expression<Func<T, bool>> whereExpression)
        {
            return Context.Queryable<T>().Where(whereExpression).SplitTable().ToListAsync();
        }
        public Task<List<T>> SplitTableGetListAsync(Expression<Func<T, bool>> whereExpression, DateTime beginTime, DateTime endTime)
        {
            return Context.Queryable<T>().Where(whereExpression).SplitTable(beginTime, endTime).ToListAsync();
        }

        public Task<List<T>> SplitTableGetListAsync(Expression<Func<T, bool>> whereExpression, string[] tableNames)
        {
            return Context.Queryable<T>().Where(whereExpression).SplitTable(t => t.InTableNames(tableNames)).ToListAsync();
        }

        #endregion 分表操作
    }

}
